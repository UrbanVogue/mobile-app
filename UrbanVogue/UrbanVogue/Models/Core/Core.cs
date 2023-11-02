using Polly;
using Polly.Retry;
using System.Net;
using UrbanVogue.Enums;
using UrbanVogue.Models.ObservableModels;
using UrbanVogue.Models.Request;
using UrbanVogue.Models.Response;

namespace UrbanVogue.Models.Core
{
    public class Core
    {
        private readonly AppSettings _appSettings;

        private AsyncRetryPolicy _asyncPolicy;

        public Core()
        {
            _appSettings = new AppSettings();
        }

        public async Task<List<CatalogProduct>> GetProducts()
        {
            var res = new List<CatalogProduct>();

            var response = await RestApi.GetAsync<List<CatalogProductResponse>>(GetUri(EnumMethod.Catalogue), null, 10);

            foreach (var item in response)
            {
                res.Add(new CatalogProduct
                {
                    Name = item.Name,
                    BasePrice = item.BasePrice,
                    Discount = item.DiscountPrice,
                    Rating = item.Rating,
                    Image = item.Image.Data is not null ? $"data:image/png;base64,{Convert.ToBase64String(item.Image.Data)}" : null
                });
            }


            return res;
        }



        public async Task<object> AuthenticateAsync(object body)
        {
            return Task.FromResult<object>(null);
        }

        private async Task<TResult> GetResult<TResult>(Uri uri, object body, Header header, int attemptsLeft = 3, int seconds = AppSettings.StandardRequestTime) where TResult : BaseResponseResult
        {
            _asyncPolicy = Policy
                .Handle<HttpRequestException>(e => e.StatusCode == HttpStatusCode.Unauthorized)
                .Or<HttpRequestException>()
                .Or<TaskCanceledException>()
                .WaitAndRetryAsync(attemptsLeft,
                                    index => TimeSpan.FromSeconds(0),
                                    OnRetry);

            return await _asyncPolicy.ExecuteAsync(async () => await RestApi.PostAsync<TResult>(uri, body, header, seconds));

        }

        public Uri GetUri(EnumMethod method)
        {
            string path = Path.Combine(AppSettings.API_URI, CommandToName(method));
            var uri = new Uri(path);
            return uri;
        }


        public static string CommandToName(EnumMethod method) => method switch
        {
            EnumMethod.Authenticate => "Authenticate",
            EnumMethod.Catalogue => "catalogue",
            _ => throw new NotImplementedException(),
        };

        private async Task OnRetry(Exception e, TimeSpan ts)
        {
            switch (e)
            {
                case HttpRequestException hre when hre.StatusCode == HttpStatusCode.Unauthorized:
                    // await AuthorizationDoesntThrowsException(_authenticateBody.CardCode);
                    break;
                    /*
                     case HttpRequestException:
                         throw new NoInternetException();
                     case TaskCanceledException:
                         throw new NoInternetException();
                     case WebException:
                         throw new NoInternetException();
                     default:
                         throw new NoInternetException();
                    */

            }

        }
    }
}
