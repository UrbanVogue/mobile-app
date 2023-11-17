namespace UrbanVogue.Models.Response;

public class CatalogProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal BasePrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public float Rating { get; set; }
    public ImageResponse Image { get; set; }
}
