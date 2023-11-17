namespace UrbanVogue.Models.ObservableModels
{
    public class DetailedProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public float Rating { get; set; }
        public List<string> Images { get; set; }

    }
}
