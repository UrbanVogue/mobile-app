namespace UrbanVogue.Models.ObservableModels
{
    public class CartProduct
    {
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Discount { get; set; }
        public int Count { get; set; }
    }
}
