namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// The factory that creates the wizard's pages.
    /// </summary>
    public interface IPageFactory
    {
        /// <summary>
        /// Create a new instance of a wizard page.
        /// </summary>
        /// <param name="parent">The view model of the parent wizard.</param>
        /// <param name="pageType">The type of the page.</param>
        /// <returns>New instance of a wizard page.</returns>
        IWizardPage CreatePage(WizardViewModel parent, WizardPages pageType);
    }
}