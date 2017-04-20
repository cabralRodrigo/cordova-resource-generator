using System.Windows.Controls;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Code behind for the select color and ouput wizard page.
    /// </summary>
    public partial class SelectColorAndOutputWizardPage : Page, IWizardPage
    {
        /// <summary>
        /// The view model.
        /// </summary>
        public IWizardPageViewModel ViewModel => (SelectColorAndOutputWizardViewModel)this.DataContext;

        /// <summary>
        /// Creates a new instance of the <see cref="SelectColorAndOutputWizardPage"/> class.
        /// </summary>
        /// <param name="viewModel">The view model of the page.</param>
        public SelectColorAndOutputWizardPage(SelectColorAndOutputWizardViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}