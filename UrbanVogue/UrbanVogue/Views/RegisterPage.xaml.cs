namespace UrbanVogue.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
