namespace UrbanVogue.Views;

public partial class CatalogPage : ContentPage
{
    private CatalogViewModel _vm;

    public CatalogPage(CatalogViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_vm = viewModel;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await _vm.InitAsync();
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {

    }
}
