using Newtonsoft.Json;
using System.Text;

namespace UrbanVogue.Models.Core
{
    public class RestApi
    {
        /// <summary>
        /// MediaType
        /// </summary>
        public const string ApplicationJson = "application/json";
        
        public static async Task<TResult> PostAsync<TResult>(
            Uri uri,
            object value,
            Header header = null,
            int seconds = AppSettings.StandardRequestTime
            )
        {
            using HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(seconds),
            };

            if (header is not null && header.Count() != 0)
            {
                foreach (KeyValuePair<string,string> item in header)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            HttpResponseMessage message = await client.PostAsync(uri,
                value is not null ? new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, ApplicationJson)
                : new StringContent(string.Empty, Encoding.UTF8, ApplicationJson)
                );

            message.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<TResult>(await message.Content.ReadAsStringAsync());
        }

        public static async Task<TResult> GetAsync<TResult>(
            Uri uri,
            Header header = null,
            int seconds = AppSettings.StandardRequestTime
            )
        {
            using HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(seconds),
            };

            if (header is not null && header.Count() != 0)
            {
                foreach (KeyValuePair<string, string> item in header)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            HttpResponseMessage message = await client.GetAsync(uri);

            message.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<TResult>(await message.Content.ReadAsStringAsync());
        }
    }
}
