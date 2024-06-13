using System.Text.Json.Serialization;

namespace BigAmbitions.Domain.Config;
public class ProductConfig
{
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("qti_per_box")]
    public int QtiPerBox { get; set; }
}
