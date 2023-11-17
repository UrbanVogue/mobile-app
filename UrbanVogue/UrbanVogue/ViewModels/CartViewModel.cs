using UrbanVogue.Models.Core;
using UrbanVogue.Models.ObservableModels;

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
        await RefreshProducts();
    }

    [RelayCommand]
    public async Task RefreshProducts()
    {
        try
        {
            IsBusy = true;
            var cartProducts = await _core.GetCartProducts();

            CartProducts.Clear();
            foreach (var product in cartProducts)
            {
                CartProducts.Add(new CartProductOM
                {
                    Name = product.Name,
                    BasePrice = product.BasePrice,
                    Count = product.Count,
                    Discount = product.Discount,
                    TotalPrice = product.BasePrice * product.Count - product.Discount
            });
            }

            IsBusy = false;
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
    public async Task IncCount(CartProductOM product)
    {
        product.Count++;
        product.TotalPrice = product.BasePrice * product.Count - product.Discount;
    }

    [RelayCommand]
    public async Task DecrCount(CartProductOM product)
    {
        product.Count--;
        product.TotalPrice = product.BasePrice * product.Count - product.Discount;
    }
}
