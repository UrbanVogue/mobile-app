using Newtonsoft.Json;
using UrbanVogue.Models.ObservableModels;

namespace UrbanVogue.Models.Request
{
    public class CreateCartRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("items")]
        public List<CartProduct> Items { get; set; }
    }
}
