using System.Windows.Controls;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Code behind for the select config wizard page.
    /// </summary>
    public partial class SelectConfigWizardPage : Page, IWizardPage
    {
        /// <summary>
        /// The view model.
        /// </summary>
        public IWizardPageViewModel ViewModel => (SelectConfigWizardViewModel)this.DataContext;

        /// <summary>
        /// Creates a new instance of the <see cref="SelectConfigWizardPage"/> class.
        /// </summary>
        /// <param name="viewModel">The view model of the page.</param>
        public SelectConfigWizardPage(SelectConfigWizardViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}