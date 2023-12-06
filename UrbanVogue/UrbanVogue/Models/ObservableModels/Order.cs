using Newtonsoft.Json;

namespace UrbanVogue.Models.ObservableModels
{
    public class Order
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("totalPrice")]
        public decimal TotalPrice { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }
        [JsonProperty("addressLine")]
        public string AddressLine { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }
        [JsonProperty("cartNumber")]
        public string CardNumber { get; set; }
        [JsonProperty("cardName")]
        public string CardName { get; set; }
        [JsonProperty("expiration")]
        public string Expiration { get; set; }
        [JsonProperty("cvv")]
        public string CVV { get; set; }
        [JsonProperty("paymentMethod")]
        public bool PaymentMethod { get; set; }
        [JsonProperty("items")]
        public List<CartProduct> Items { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
