namespace UrbanVogue.Views;

public partial class CartPage : ContentPage
{
    private CartViewModel _vm;

    public CartPage(CartViewModel viewModel)
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
