
using Newtonsoft.Json;

namespace UrbanVogue.Models.Response
{
    public class ClaimsResponse
    {
        [JsonProperty("sub")]
        public string Sub { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("preferred_username")]
        public string PrefferedUsername { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
