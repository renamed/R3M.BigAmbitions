using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;
using System.Text.Json;
using System.Text;
using BigAmbitions.Repository.Contexts;

namespace BigAmbitions.WebApi.IntegrationTests.Fixtures;

public class IntegrationTestsBase : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly CustomWebApplicationFactory<Program> _applicationFactory;
    private readonly IServiceScope _scope;
    protected BigAmbitionContext Context { get; init; }

    private readonly HttpClient _httpClient;

    public IntegrationTestsBase(CustomWebApplicationFactory<Program> factory)
    {
        _applicationFactory = factory;
        _httpClient = _applicationFactory.CreateClient();

        _scope = _applicationFactory.Services.CreateScope();
        Context = _scope.ServiceProvider.GetRequiredService<BigAmbitionContext>();

        EnsureDatabaseCreated(Context);
        PopulateDb();
    }

    protected StringContent StringContent(object obj)
        => new(JsonSerializer.Serialize(obj), Encoding.UTF8, MediaTypeNames.Application.Json);

    protected async Task<HttpResponseHelper<TResponse>> PostAsync<TResponse, KRequest>(string url, KRequest body)
    {
        var response = await _httpClient.PostAsync(url, StringContent(body));
        return await BuildResponse<TResponse>(response);
    }

    protected Task<HttpResponseHelper<TResponse>> GetAsync<TResponse>(string url)
    {
        return GetAsync<TResponse>(url, null);
    }

    protected async Task<HttpResponseHelper> DeleteAsync(string url, params string[] pathParams)
    {
        var response = await _httpClient.DeleteAsync(GetUrl(url, pathParams));
        return await BuildResponse(response);
    }

    protected async Task<HttpResponseHelper<TResponse>> GetAsync<TResponse>(string url, params string[] pathParams)
    {
        var response = await _httpClient.GetAsync(GetUrl(url, pathParams));
        return await BuildResponse<TResponse>(response);
    }

    protected async Task<HttpResponseHelper<TResponse>> PutAsync<TResponse, KRequest>(string url, KRequest body, params string[] pathParams)
    {
        var response = await _httpClient.PutAsync(GetUrl(url, pathParams), StringContent(body));
        return await BuildResponse<TResponse>(response);
    }

    private async Task<HttpResponseHelper<TResponse>> BuildResponse<TResponse>(HttpResponseMessage response)
    {
        var body = await response.Content.ReadAsStringAsync();
        return new HttpResponseHelper<TResponse>
        {
            Body = !string.IsNullOrWhiteSpace(body) ? JsonSerializer.Deserialize<TResponse>(body, _serializerOptions) : default,
            Response = response
        };
    }

    private async Task<HttpResponseHelper> BuildResponse(HttpResponseMessage response)
    {
        return await BuildResponse<object>(response);
    }

    private static string GetUrl(string url, params string[] pathParams)
    {
        return pathParams == null ? url : string.Format(url, pathParams);
    }


    private static void EnsureDatabaseCreated(BigAmbitionContext context)
    {
        context.Database.EnsureCreated();
    }

    private void PopulateDb()
    {
        Context.Games.Add(new Domain.Game
        {
            Id = 1,
            Name = "Test game name 1"
        });

        Context.Businesses.Add(new Domain.Business
        {
            Id = 1,
            GameId = 1,
            Name = "Test business name 1"
        });
        //_context.Organizations.AddRange(ReadDataFile<Organization>("Organizations.json"));
        //_context.Categories.AddRange(ReadDataFile<Category>("Categories.json"));
        //_context.Periods.AddRange(ReadDataFile<Period>("Periods.json"));
        //_context.Transactions.AddRange(ReadDataFile<Transaction>("Transactions.json"));
        //_context.FinancialGoals.AddRange(ReadDataFile<FinancialGoal>("FinancialGoals.json"));

        Context.SaveChanges();
    }

    private static IEnumerable<T> ReadDataFile<T>(string filename)
        => JsonSerializer.Deserialize<IEnumerable<T>>(File.ReadAllText(Path.Combine(".", "Data", filename)));

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        Context.Database.EnsureDeleted();
        Context.Dispose();
        _scope.Dispose();
    }

    protected class HttpResponseHelper<T> : HttpResponseHelper
    {
        public T Body { get; set; }
    }

    protected class HttpResponseHelper
    {
        public HttpResponseMessage Response { get; set; }
    }
}
