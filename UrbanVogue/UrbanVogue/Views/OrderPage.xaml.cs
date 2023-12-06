namespace UrbanVogue.Views;

public partial class OrderPage : ContentPage
{
    private OrderViewModel _vm;
    public OrderPage(OrderViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _vm = viewModel;
    }
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await _vm.InitAsync();
    }
}
