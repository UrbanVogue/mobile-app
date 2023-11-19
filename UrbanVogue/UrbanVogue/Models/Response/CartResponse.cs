using Newtonsoft.Json;
using UrbanVogue.Models.ObservableModels;

namespace UrbanVogue.Models.Response
{
    public class CartResponse
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Username { get; set; }
        public List<CartProduct> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
