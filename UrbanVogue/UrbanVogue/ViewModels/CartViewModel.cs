using UrbanVogue.Models.Core;
using UrbanVogue.Models.ObservableModels;
using UrbanVogue.Models.Request;
using UrbanVogue.Models.Response;

namespace UrbanVogue.ViewModels;

public partial class CartViewModel : BaseViewModel
{
    private readonly Core _core;

    [ObservableProperty]
    private bool _isGridView;

    [ObservableProperty]
    private bool _isListView;

    [ObservableProperty]
    private ObservableCollection<CartProductOM> _cartProducts;

    public CartViewModel(Core core)
    {
        _cartProducts = new ObservableCollection<CartProductOM>();
        _core = core;
    }

    public override async Task InitAsync()
    {
        await Refresh();
    }

    //public static DetailedProductResponse detail = new DetailedProductResponse
    //{
    //    Id = 1,
    //    Name = "Test",
    //    BasePrice = 10000,
    //    DiscountPrice = 0,
    //    Rating = 10,
    //    Description = "",
    //    Images = new List<ImageResponse>(),
    //    ProductItems = new List<ProductItem>
    //    {
    //        new ProductItem
    //        {
    //            Id = 1,
    //            Size = "Large",
    //            Color = "Black"
    //        },
    //        new ProductItem
    //        {
    //            Id = 2,
    //            Size = "Large",
    //            Color = "White"
    //        },
    //        new ProductItem
    //        {
    //            Id = 3,
    //            Size = "Small",
    //            Color = "Black"
    //        }
    //    }
    //};

    [RelayCommand]
    public async Task Refresh()
    {
        try
        {
            IsBusy = true;

            var cart = await _core.GetCartProductsAsync("test");
            if (cart != null)
            {
                CartProducts.Clear();
                foreach (var product in cart.Items)
                {
                    var detail = await _core.GetProductDetailsAsync(product.ProductId);

                    var sizes = detail.ProductItems.Select(x => x.Size).Distinct().ToList();
                    var colors = detail.ProductItems.Where(x => x.Size == product.Size).Select(x => x.Color).Distinct().ToList();

                    CartProducts.Add(new CartProductOM
                    {
                        Name = product.ProductName,
                        BasePrice = product.Price,
                        Count = product.Quantity,
                        Discount = 0,
                        TotalPrice = product.Price * product.Quantity - 0,
                        Color = product.Color,
                        Size = product.Size,
                        Colors = colors,
                        Sizes = sizes
                    });
                }
            }
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async void IncCount(CartProductOM product)
    {
        var cart = await _core.GetCartProductsAsync("test");

        if (cart != null)
        {
            var item = cart.Items.FirstOrDefault(x => x.ProductName == product.Name);
            if (item != null)
            {
                item.Quantity++;
                item.Price += item.Price / (item.Quantity - 1);
                cart.TotalPrice += item.Price / (item.Quantity + 1);
            }
        }

        var response = await _core.AddToCart(new CreateCartRequest
        {
            Username = "test",
            Items = cart.Items
        });

        if (response != null)
        {
            await Refresh();
        }
    }

    [RelayCommand]
    private async void DecrCount(CartProductOM product)
    {
        var cart = await _core.GetCartProductsAsync("test");

        if (cart != null)
        {
            var item = cart.Items.FirstOrDefault(x => x.ProductName == product.Name);
            if (item != null)
            {
                item.Quantity--;
                item.Price -= item.Price / (item.Quantity + 1);
                cart.TotalPrice -= item.Price / (item.Quantity + 1);
            }
        }

        var response = await _core.AddToCart(new CreateCartRequest
        {
            Username = "test",
            Items = cart.Items
        });

        if (response != null)
        {
            await Refresh();
        }
    }

    //[RelayCommand]
    //private async Task OpenProduct(CartProductOM product)
    //{
    //    await Shell.Current.GoToAsync("/ProductPage", new Dictionary<string, object>
    //    {
    //        { "id", product.Id }
    //    });
    //}

    [RelayCommand]
    private async Task DeleteFromCart(CartProductOM product)
    {
        var cart = await _core.GetCartProductsAsync("test");

        if (cart != null)
        {
            var item = cart.Items.FirstOrDefault(x => x.ProductName == product.Name);
            if (item != null)
            {
                cart.Items.Remove(item);
            }
        }

        var response = await _core.AddToCart(new CreateCartRequest
        {
            Username = "test", 
            Items = cart.Items
        });

        if (response != null)
        {
            await Refresh();
        }
    }

    [RelayCommand]
    private async Task ClearCart()
    {
        var cart = await _core.GetCartProductsAsync("test");

        if (cart != null)
        {
            cart.Items.Clear();
        }

        var response = await _core.AddToCart(new CreateCartRequest
        {
            Username = "test",
            Items = cart.Items
        });

        if (response != null)
        {
            await Refresh();
        }
    }

    [RelayCommand]
    public async void ChangeSize(CartProductOM product)
    {
        var cart = await _core.GetCartProductsAsync("test");

        if (cart != null)
        {
            var item = cart.Items.FirstOrDefault(x => x.ProductName == product.Name);
            if (item != null)
            {
                item.Size = product.Size;
            }
        }

        //var response = await _core.AddToCart(new CreateCartRequest
        //{
        //    Username = "test",
        //    Items = cart.Items
        //});

        if (true)
        {
            await Refresh();
        }
    }

    [RelayCommand]
    public async void ChangeColor(CartProductOM product)
    {
        var cart = await _core.GetCartProductsAsync("test");

        if (cart != null)
        {
            var item = cart.Items.FirstOrDefault(x => x.ProductName == product.Name);
            if (item != null)
            {
                item.Color = product.Color;
            }
        }

        //var response = await _core.AddToCart(new CreateCartRequest
        //{
        //    Username = "test",
        //    Items = cart.Items
        //});

        if (true)
        {
            await Refresh();
        }
    }

}
