﻿using CommunityToolkit.Maui.Alerts;
using UrbanVogue.Models.Core;
using UrbanVogue.Models.ObservableModels;
using UrbanVogue.Models.Request;
using UrbanVogue.Models.Response;
using ProductItem = UrbanVogue.Models.ObservableModels.ProductItem;

namespace UrbanVogue.ViewModels
{
    public partial class ProductPageVM : BaseViewModel, IQueryAttributable
    {
        private readonly Core _core;

        [ObservableProperty]
        private DetailedProduct _product;

        private int? _productId;

        public ProductPageVM(Core core)
        {
            _core = core;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("id"))
            {
                var id = query["id"];
                _productId = Convert.ToInt32(id);
            }
        }

        public override async Task InitAsync()
        {
            Product = null;

            if (_productId == null)
            {
                await Shell.Current.GoToAsync("..");
                return;
            }

            var product = await _core.GetProductDetailsAsync(_productId.Value);

            Product = new DetailedProduct
            {
                Id = product.Id,
                Name = product.Name,
                BasePrice = product.BasePrice,
                DiscountPrice = product.DiscountPrice,
                Images = product.Images.ConvertAll(x => $"data:image/png;base64,{x.Data}"),
                Rating = product.Rating,
                ProductItems = product.ProductItems.ConvertAll(x => new ProductItem
                {
                    Id = x.Id,
                    Color = x.Color,
                    ColorHash = Color.FromArgb(x.ColorCode),
                    Size = x.Size
                })
            };
        }



        [RelayCommand]
        private async Task AddToCart()
        {
            try
            {
                if (_core.AppSettings.AuthResponse is null)
                {
                    await Shell.Current.GoToAsync("//LoginPage");
                    return;
                }

                var cart = await _core.GetCartProductsAsync("test");

                CartResponse response = null;

                if (cart != null)
                {
                    if (cart.Items.Any(x => x.ProductId == Product.Id))
                    {
                        cart.Items.First(x => x.ProductId == Product.Id).Quantity++;
                    }
                    else
                    {
                        cart.Items.Add(new CartProduct
                        {
                            ProductId = Product.Id,
                            ProductName = Product.Name,
                            Quantity = 1,
                            Price = Product.BasePrice,
                            Color = Product.ProductItems.First().Color,
                            Size = Product.ProductItems.First().Size
                        });
                    }

                    response = await _core.AddToCart(new CreateCartRequest
                    {
                        Username = "test", //_core.AppSettings.ClaimsResponse.PrefferedUsername,
                        Items = cart.Items
                    });

                }
                else
                {

                    response = await _core.AddToCart(new CreateCartRequest
                    {
                        Username = "test", //_core.AppSettings.ClaimsResponse.PrefferedUsername,
                        Items = new List<CartProduct>
                        {
                            new CartProduct
                            {
                                ProductId = Product.Id,
                                ProductName = Product.Name,
                                Quantity = 1,
                                Price = Product.BasePrice,
                                Color = Product.ProductItems.First().Color,
                                Size = Product.ProductItems.First().Size
                            }
                        }

                    });
                }

                if (response == null)
                {

                    await App.Current.MainPage.DisplayAlert("Info", "Product was not added to cart", "Ok");

                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        [RelayCommand]
        private async Task OpenCart()
        {
            await Shell.Current.GoToAsync("/CartPage");
        }
    }
}
