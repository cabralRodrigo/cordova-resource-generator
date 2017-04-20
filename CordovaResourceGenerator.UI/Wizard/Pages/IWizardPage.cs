namespace CordovaResourceGenerator.UI
{
    public interface IWizardPage
    {
        string Title { get; }
        IWizardPageViewModel ViewModel { get; }
    }
}