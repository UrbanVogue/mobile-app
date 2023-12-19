namespace UrbanVogue.Models.Response
{
    public class DetailedProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }
        public List<ImageResponse> Images { get; set; }
        public List<ProductItem> ProductItems { get; set; }
    }

    public class ProductItem
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string ColorCode { get; set; }

    }
}
