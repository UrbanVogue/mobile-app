namespace UrbanVogue;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
		Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
		Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
	}

    private void OnLogoutButtonClicked(object sender, EventArgs e)
    {

    }
}
