namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Interface that defines a wizard.
    /// </summary>
    public interface IWizard
    {
        /// <summary>
        /// Navigate the wizard page.
        /// </summary>
        /// <param name="navigationType">The type of navigation.</param>
        void Navigate(NavigationType navigationType);
    }
}