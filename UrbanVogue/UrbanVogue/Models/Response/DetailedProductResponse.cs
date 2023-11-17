using System.Text.Json.Serialization;

namespace UrbanVogue.Models.Response
{
    public class DetailedProductResponse
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice{ get; set; } 
        public decimal DiscountPrice { get; set; }
        public float Rating { get; set; }
        public List<ImageResponse> Images { get; set; }
    }
}
