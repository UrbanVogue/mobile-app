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

            var tes = JsonConvert.SerializeObject(value);

            Console.Out.WriteLine(tes);

            var requestString = value is not null ? new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, ApplicationJson)
                : new StringContent(string.Empty, Encoding.UTF8, ApplicationJson);


            HttpResponseMessage message = await client.PostAsync(uri, requestString);

            message.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<TResult>(await message.Content.ReadAsStringAsync());
        }

        public static async Task<TResult> GetAsync<TResult>(
            Uri uri,
            string queryParams = null,
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

            if(queryParams is not null)
            {
                uri = new Uri($"{uri}/{queryParams}");
            }

            HttpResponseMessage message = await client.GetAsync(uri);

            message.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<TResult>(await message.Content.ReadAsStringAsync());
        }



        public static async Task<TResult> PostAuthAsync<TResult>(
            Header header = null,
            ContentBody content = null,
            int seconds = AppSettings.StandardRequestTime)
        {
            try
            {
                var uri = new Uri("http://172.21.224.1:8010/connect/token");

                var request = new HttpRequestMessage(HttpMethod.Post, uri);
                using var client = new HttpClient()
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

                if (content != null && content.Dictionary.Count != 0)
                {
                    request.Content = new FormUrlEncodedContent(content.Dictionary);
                }

                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<TResult>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }
}
