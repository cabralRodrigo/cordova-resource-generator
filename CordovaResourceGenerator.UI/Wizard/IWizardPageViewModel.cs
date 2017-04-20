namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Interface that defines the basic members of a wizard page view model.
    /// </summary>
    public interface IWizardPageViewModel
    {
        /// <summary>
        /// The page type.
        /// </summary>
        WizardPages PageType { get; }

        /// <summary>
        /// The parent view model.
        /// </summary>
        WizardViewModel Parent { get; set; }

        /// <summary>
        /// Determines if the page can navigate.
        /// </summary>
        /// <param name="navigationType">The type of the navigation.</param>
        /// <returns></returns>
        bool CanNavigate(NavigationType navigationType);

        /// <summary>
        /// Method called before the view navigates.
        /// </summary>
        /// <param name="navigationType">The type of the navigation.</param>
        /// <returns>True if the view should navigate, False if not.</returns>
        bool BeforeNavigate(NavigationType navigationType);
    }
}