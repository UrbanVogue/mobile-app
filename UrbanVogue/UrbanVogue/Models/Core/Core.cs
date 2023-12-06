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

        private AsyncRetryPolicy _asyncPolicy;

        public AppSettings AppSettings { get; set; }

        public Core()
        {
            AppSettings = new AppSettings();
        }

        public static List<CartProduct> response = new List<CartProduct>
            {
                new CartProduct
                {
                    ProductId = 1,
                    ProductName = "Кепка",
                    Price = 50000,
                    Color = "Black",
                    Size = "Large",
                    Quantity = 2
                },
                new CartProduct
                {
                    ProductId = 2,
                    ProductName = "Джинси",
                    Price = 230000,
                    Color = "Black",
                    Size = "Small",
                    Quantity = 1
                }
            };

        public async Task<List<CatalogProduct>> GetProducts()
        {
            var res = new List<CatalogProduct>();

            var response = await RestApi.GetAsync<List<CatalogProductResponse>>(GetUri(EnumMethod.Catalogue), null, null, 10);

            foreach (var item in response)
            {
                res.Add(new CatalogProduct
                {
                    Id = item.Id,
                    Name = item.Name,
                    BasePrice = item.BasePrice,
                    Discount = item.DiscountPrice,
                    Rating = item.Rating,
                    Image = item.Image.Data is not null ? $"data:image/png;base64,{item.Image.Data}" : null
                });
            }

            return res;
        }

        public async Task<DetailedProductResponse> GetProductDetailsAsync(int id)
        {
            var res = await RestApi.GetAsync<DetailedProductResponse>(GetUri(EnumMethod.Catalogue), queryParams: id.ToString(), null, 10);

            return res;
        }

        public async Task<CartResponse> GetCartProductsAsync(string username)
        {
            var res = await RestApi.GetAsync<CartResponse>(new Uri($"http://172.21.224.1:7777/api/v1/basket"), username);

            //var res = new CartResponse
            //{
            //    Id = 1,
            //    Username = "Pena",
            //    Items = response,
            //    TotalPrice = 330000,
            //};

            return res;
        }

        public async Task<List<Order>> GetOrderHistoryAsync(string username)
        {
            //var res = await RestApi.GetAsync<Order>(new Uri($""), username);

            var res = new List<Order>
            {
                new Order
                {
                    Id = 534,
                    UserName = "Pena",
                    TotalPrice = 330000,
                    FirstName = "Penadate",
                    LastName = "1234",
                    EmailAddress = "email@gmail.com",
                    AddressLine = "Some address",
                    Country = "UA",
                    State = "Оплачено",
                    ZipCode = "123124123",
                    CardName = "P24",
                    CardNumber = "1234567891233471",
                    Expiration = "14/25",
                    CVV = "123",
                    PaymentMethod = false,
                    Date = new DateTime(2023, 11, 28),
                    Items = response
                },
                new Order
                {
                    Id = 535,
                    UserName = "Pena",
                    TotalPrice = 330000,
                    FirstName = "Penadate",
                    LastName = "1234",
                    EmailAddress = "email@gmail.com",
                    AddressLine = "Some address",
                    Country = "UA",
                    State = "Оплачено",
                    ZipCode = "123124123",
                    CardName = "P24",
                    CardNumber = "1234567891233471",
                    Expiration = "14/25",
                    CVV = "123",
                    PaymentMethod = false,
                    Date = new DateTime(2023, 12, 4),
                    Items = response
                }
            };

            return res;
        }

        public async Task<CartResponse> AddToCart(CreateCartRequest request)
        {
            try
            {
                var res = await RestApi.PostAsync<CartResponse>(new Uri("http://172.21.224.1:7777/api/v1/basket"), request);

                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<object> AuthenticateAsync()
        {
            return await Task.FromResult<object>(null);
        }

        public async Task<bool> RegisterAsync(RegisterRequest registerRequest)
        {

            //GetResult<AuthenticateResponse>(GetUri(EnumMethod.Authenticate),
            //    new AuthenticateBody {
            //    Email = email,
            //    Password = password
            //}, null);

            return true;

        }

        public async Task<bool> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var authResponse = await RestApi.PostAuthAsync<LoginResponse>(header: null, GetAuthBody(loginRequest));

                if (authResponse != null)
                {
                    AppSettings.AuthResponse = authResponse;

                    var claimsResponse = await RestApi.GetAsync<ClaimsResponse>(
                        new Uri("http://172.21.224.1:8010/connect/userinfo"),
                        null,
                        GetAuthHeader());

                    if (claimsResponse != null)
                    {
                        AppSettings.ClaimsResponse = claimsResponse;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Header GetAuthHeader()
        {
            var header = new Header
            {
                { "Authorization", AppSettings.AuthResponse.TokenType + " " + AppSettings.AuthResponse.AccessToken }
            };

            return header;
        }

        private ContentBody GetAuthBody(LoginRequest loginRequest)
        {
            var body = new ContentBody
            {
                { "grant_type", "password" },
                { "scope", "CatalogAPI.read CatalogAPI.write offline_access profile openid" },
                { "client_id", "Maui-Client" },
                { "client_secret", "ClientSecret1" },
                { "username", loginRequest.Email },
                { "password", loginRequest.Password }
            };

            return body;
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
