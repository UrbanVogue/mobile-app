namespace UrbanVogue.Models.Response;

public class CatalogProductResponse
{
    public string Name { get; set; }
    public decimal BasePrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public float Rating { get; set; }
    public CatalogImage Image { get; set; }
}

public class CatalogImage
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public byte[] Data { get; set; }
    public bool ForCatalogue { get; set; }
}
