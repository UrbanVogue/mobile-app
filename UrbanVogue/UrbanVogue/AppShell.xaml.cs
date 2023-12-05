using UrbanVogue.Models.Core;

namespace UrbanVogue;

public partial class AppShell : Shell
{

    public AppShell( )
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
		Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
		Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));        
    }

    private void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var core = Application.Current.Handler.MauiContext.Services.GetService<Core>();
            core.Logout();

            FlyoutIsPresented = false;

        }
        catch (Exception ex)
        {
            App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
    }
}
