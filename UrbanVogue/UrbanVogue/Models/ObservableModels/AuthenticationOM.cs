namespace UrbanVogue.Models.ObservableModels;

public partial class AuthenticationOM : ObservableObject
{
    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _firstName;

    [ObservableProperty]
    private string _lastName;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string _confirmPassword;


}
