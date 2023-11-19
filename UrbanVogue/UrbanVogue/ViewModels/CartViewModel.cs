using UrbanVogue.Models.Core;
using UrbanVogue.Models.ObservableModels;
using UrbanVogue.Models.Request;

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
                    CartProducts.Add(new CartProductOM
                    {
                        Name = product.ProductName,
                        BasePrice = product.Price,
                        Count = product.Quantity,
                        Discount = 0,
                        TotalPrice = product.Price * product.Quantity - 0,
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
    private void IncCount(CartProductOM product)
    {
        product.Count++;
        product.TotalPrice = product.BasePrice * product.Count - product.Discount;
    }


    [RelayCommand]
    private void DecrCount(CartProductOM product)
    {
        product.Count--;
        product.TotalPrice = product.BasePrice * product.Count - product.Discount;
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
}
