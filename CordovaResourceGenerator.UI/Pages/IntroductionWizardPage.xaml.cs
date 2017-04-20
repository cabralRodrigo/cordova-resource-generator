using System.Windows.Controls;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Code behind for the introduction wizard page.
    /// </summary>
    public partial class IntroductionWizardPage : Page, IWizardPage
    {
        /// <summary>
        /// The view model.
        /// </summary>
        public IWizardPageViewModel ViewModel => (IntroductionWizardViewModel)this.DataContext;

        /// <summary>
        /// Creates a new instance of the <see cref="IntroductionWizardPage"/> class.
        /// </summary>
        /// <param name="viewModel">The view model of the page.</param>
        public IntroductionWizardPage(IntroductionWizardViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}