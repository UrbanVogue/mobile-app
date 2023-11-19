using UrbanVogue.Models.Core;
using UrbanVogue.Models.ObservableModels;
using UrbanVogue.Models.Request;

namespace UrbanVogue.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    private readonly Core _core;

    [ObservableProperty]
    private bool _isLogin;

    [ObservableProperty]
    private AuthenticationOM _authenticationModel;

    public RegisterViewModel(Core core)
    {
        _core = core;
    }

    public override Task InitAsync()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task Apply()
    {
        bool res = false;

        res = await _core
           .RegisterAsync(new RegisterRequest
           {
               Email = AuthenticationModel.Email,
               FirstName = AuthenticationModel.FirstName,
               LastName = AuthenticationModel.LastName,
               Password = AuthenticationModel.Password,
               ConfirmPassword = AuthenticationModel.ConfirmPassword
           });

        if (res)
        {
            await Shell.Current.GoToAsync("//CatalogPage");
        }
    }
}
