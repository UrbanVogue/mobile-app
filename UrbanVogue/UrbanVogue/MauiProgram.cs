namespace UrbanVogue;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.RegisterPages();


        

		return builder.Build();
	}

	private static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
	{
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton<CatalogViewModel>();
        builder.Services.AddSingleton<CatalogPage>();

        builder.Services.AddSingleton<OrderViewModel>();
        builder.Services.AddSingleton<OrderPage>();

        builder.Services.AddSingleton<ProfileViewModel>();
        builder.Services.AddSingleton<ProfilePage>();

        builder.Services.AddSingleton<CartViewModel>();
        builder.Services.AddSingleton<CartPage>();

        builder.Services.AddSingleton<AboutViewModel>();
        builder.Services.AddSingleton<AboutPage>();

        builder.Services.AddSingleton<AuthViewModel>();
        builder.Services.AddSingleton<AuthPage>();


        return builder;
	}
}
