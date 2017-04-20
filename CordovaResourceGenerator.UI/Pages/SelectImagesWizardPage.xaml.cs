using System.Windows.Controls;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Code behind for the select images wizard page.
    /// </summary>
    public partial class SelectImagesWizardPage : Page, IWizardPage
    {
        /// <summary>
        /// The view model.
        /// </summary>
        public IWizardPageViewModel ViewModel => (SelectImagesWizardViewModel)this.DataContext;

        /// <summary>
        /// Creates a new instance of the <see cref="SelectImagesWizardPage"/> class.
        /// </summary>
        /// <param name="viewModel">The view model of the page.</param>
        public SelectImagesWizardPage(SelectImagesWizardViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}