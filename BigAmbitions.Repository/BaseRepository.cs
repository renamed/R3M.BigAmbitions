using System.Text.Json;

namespace BigAmbitions.Repository;
public abstract class BaseRepository<T> : IDisposable, IAsyncDisposable
{
    protected BaseRepository()
    {
        //Refresh(GetFileName());
    }

    public ValueTask AddAsync(T entity)
    {
        EntityList.Add(entity);
        return ValueTask.CompletedTask;
    }

    public ValueTask<T?> GetAsync(Func<T, bool> predicate)
    {
        return ValueTask.FromResult(EntityList.FirstOrDefault(predicate));
    }

    public ValueTask<IReadOnlyList<T>> ListAsync()
    {
        IReadOnlyList<T> readOnlyList = new List<T>(EntityList);
        return ValueTask.FromResult(readOnlyList);
    }

    protected void Refresh(string fileName)
    {
        EnsureDataDirectoryExists();

        var fullFilePath = GetFullFilePath(fileName);
        if (!File.Exists(fullFilePath)) 
        {
            return;
        }
        EntityList = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(fullFilePath)) ?? [];
    }

    protected void Save(string fileName, List<T> contents)
    {
        EnsureDataDirectoryExists();
        var filePath = GetFullFilePath(fileName);
        File.WriteAllText(filePath, JsonSerializer.Serialize(contents, new JsonSerializerOptions { WriteIndented = true }));
    }

    protected ValueTask SaveAsync(string fileName, List<T> contents)
    {
        EnsureDataDirectoryExists();
        var filePath = GetFullFilePath(fileName);
        return new ValueTask(File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(contents, new JsonSerializerOptions { WriteIndented = true })));
    }

    protected void EnsureDataDirectoryExists()
    {
        string dataFolderPath = GetDataFolderPath();

        if (!Directory.Exists(dataFolderPath))
        {
            Directory.CreateDirectory(dataFolderPath);
        }
    }

    protected string GetDataFolderPath()
    {
        return Environment.CurrentDirectory;
    }

    protected string GetFullFilePath(string fileName)
    {
        return Path.Combine(GetDataFolderPath(), fileName);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        if (EntityList.Count == 0)
        {
            return;
        }

        EnsureDataDirectoryExists();
        Save(GetFileName(), EntityList);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        if (EntityList.Count == 0)
        {
            return ValueTask.CompletedTask;
        }

        EnsureDataDirectoryExists();
        return SaveAsync(GetFileName(), EntityList);
    }

    protected abstract string GetFileName();

    protected List<T> EntityList { get; set; } = [];
}
