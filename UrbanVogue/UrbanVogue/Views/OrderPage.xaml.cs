namespace UrbanVogue.Views;

public partial class OrderPage : ContentPage
{
	public OrderPage(OrderViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
