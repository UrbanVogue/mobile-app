using UraniumUI;
using UrbanVogue.Models.Core;

namespace UrbanVogue;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseUraniumUI()
            .UseUraniumUIMaterial()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("Jost-Regular.ttf", "Jost");
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

        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LoginPage>();

		builder.Services.AddSingleton<RegisterViewModel>();
		builder.Services.AddSingleton<RegisterPage>();

		builder.Services.AddSingleton<ProductPageVM>();
        builder.Services.AddSingleton<ProductPage>();

		builder.Services.AddSingleton<Core>();

        return builder;
	}
}
