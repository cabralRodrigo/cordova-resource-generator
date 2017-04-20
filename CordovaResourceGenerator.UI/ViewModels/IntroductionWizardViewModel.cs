namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// The view model for the introduction wizard page.
    /// </summary>
    public class IntroductionWizardViewModel : BaseViewModel, IWizardPageViewModel
    {
        /// <summary>
        /// The type of the page.
        /// </summary>
        public WizardPages PageType => WizardPages.Introduction;

        /// <summary>
        /// The parent view model.
        /// </summary>
        public WizardViewModel Parent { get; set; }

        /// <summary>
        /// Method called before the view navigates.
        /// </summary>
        /// <param name="navigationType">The type of the navigation.</param>
        /// <returns>True if the view should navigate, False if not.</returns>
        public bool BeforeNavigate(NavigationType navigationType) => true;

        /// <summary>
        /// Determines if the page can navigate.
        /// </summary>
        /// <param name="navigationType">The type of the navigation.</param>
        /// <returns></returns>
        public bool CanNavigate(NavigationType navigationType)
        {
            return navigationType == NavigationType.Next || navigationType == NavigationType.Cancel;
        }
    }
}