namespace UrbanVogue.Models.ObservableModels
{
    public class CartProductOM : ObservableObject
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal BasePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public int Count { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }        
    }
}
