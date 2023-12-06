using UrbanVogue.Models.Core;
using UrbanVogue.Models.ObservableModels;

namespace UrbanVogue.ViewModels;

public partial class OrderViewModel : BaseViewModel
{
    private readonly Core _core;

    [ObservableProperty]
    private bool _isGridView;

    [ObservableProperty]
    private bool _isListView;

    [ObservableProperty]
    private ObservableCollection<Order> _orders;
    public OrderViewModel(Core core)
    {
        _orders = new ObservableCollection<Order>();
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

            var orders = await _core.GetOrderHistoryAsync("test");

            if (orders != null)
            {
                Orders.Clear();
                foreach (var order in orders)
                {
                    Orders.Add(order);
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
}
