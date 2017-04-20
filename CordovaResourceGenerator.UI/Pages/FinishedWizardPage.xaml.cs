using System.Windows.Controls;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Code behind for the finished wizard page.
    /// </summary>
    public partial class FinishedWizardPage : Page, IWizardPage
    {
        /// <summary>
        /// The view model.
        /// </summary>
        public IWizardPageViewModel ViewModel => (FinishedWizardViewModel)this.DataContext;

        /// <summary>
        /// Creates a new instance of the <see cref="FinishedWizardPage"/> class.
        /// </summary>
        /// <param name="viewModel">The view model of the page.</param>
        public FinishedWizardPage(FinishedWizardViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}