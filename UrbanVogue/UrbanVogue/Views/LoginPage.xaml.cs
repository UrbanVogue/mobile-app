namespace UrbanVogue.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _viewModel;

    public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        Task.Run(async () => await _viewModel.InitAsync());
    }
}
