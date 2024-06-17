namespace BigAmbitions.Application.Extensions;
public static class IAsyncEnumerableExtensions
{
    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> enumerable)
    {
        List<T> list = [];

        await foreach (var item in enumerable)
        {
            list.Add(item);
        }
        return list;
    }
}
