namespace UrbanVogue.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    public bool _isBusy;

    //public abstract void Init();

    public virtual Task InitAsync()
    {
        return Task.CompletedTask;
    }
}
