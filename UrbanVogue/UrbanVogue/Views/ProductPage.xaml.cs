namespace UrbanVogue.Views;

public partial class ProductPage : ContentPage
{
    private readonly ProductPageVM _vm;

    public ProductPage(ProductPageVM vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    private void ImagesCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        indicatorView.MoveTo(e.CenterItemIndex - 1);
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await _vm.InitAsync();

        //base.OnNavigatedTo(args);
    }

    public void ApplyQueryAttributes(IDictionary<string, string> query)
    {
        if (query.ContainsKey("id"))
        {
            var id = query["id"];
            _vm.Product.Id = Convert.ToInt32(id);
        }
    }
}