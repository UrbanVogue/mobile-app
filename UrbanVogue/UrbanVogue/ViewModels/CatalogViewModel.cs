using UrbanVogue.Models.Core;
using UrbanVogue.Models.ObservableModels;

namespace UrbanVogue.ViewModels;

public partial class CatalogViewModel : BaseViewModel
{
    private readonly Core _core;
    [ObservableProperty]
    private bool _isGridView;

    [ObservableProperty]
    private bool _isListView;

    [ObservableProperty]
    private ObservableCollection<CatalogProduct> _products;


    public CatalogViewModel(Core core)
    {
        _products = new ObservableCollection<CatalogProduct>();
        _core = core;
    }


    public override async Task InitAsync()
    {
        await RefreshProducts();
    }

    [RelayCommand]
    private async Task OpenProduct(CatalogProduct product)
    {
        await Shell.Current.GoToAsync("/ProductPage", new Dictionary<string, object>
        {
            { "id", 1 }
        });
    }

    [RelayCommand]
    public async Task RefreshProducts()
    {
        try
        {
            IsBusy = true;
            var products = await _core.GetProducts();

            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
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


}
