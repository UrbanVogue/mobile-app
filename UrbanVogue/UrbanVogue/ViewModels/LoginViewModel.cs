using UrbanVogue.Models.Core;
using UrbanVogue.Models.ObservableModels;
using UrbanVogue.Models.Request;

namespace UrbanVogue.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly Core _core;

    [ObservableProperty]
    private bool _isLogin;

    [ObservableProperty]
    private AuthenticationOM _authenticationModel;

    public LoginViewModel(Core core)
    {
        _core = core;
    }

    public override Task InitAsync()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task Register()
    {
        await Shell.Current.GoToAsync("/RegisterPage");
    }

    [RelayCommand]
    private async Task Apply()
    {
        //bool res= await _core
        //        .LoginAsync(new LoginRequest
        //        {
        //            Email = AuthenticationModel.Email,
        //            Password = AuthenticationModel.Password
        //        });      

        //if (res)
        //{
        //    await Shell.Current.GoToAsync("//CatalogPage");
        //}

        await Shell.Current.GoToAsync("//CatalogPage");
    }
}
