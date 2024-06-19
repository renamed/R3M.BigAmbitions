namespace BigAmbitions.Domain;
public class Response<T>
{
    public T? Data { get; set; }

    public bool HasError => Data != null;

    public List<string> Message { get; set; } = [];
}

public class Response
{
    public object? Data { get; set; }

    public bool HasError => Data != null;

    public string? Message { get; set; }
}