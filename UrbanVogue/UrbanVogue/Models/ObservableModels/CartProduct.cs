using Newtonsoft.Json;

namespace UrbanVogue.Models.ObservableModels
{
    public class CartProduct
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }
        [JsonProperty("productName")]
        public string ProductName { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }       
        
    }
}
