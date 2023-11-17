namespace UrbanVogue;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
	}

    private void OnLogoutButtonClicked(object sender, EventArgs e)
    {

    }
}
