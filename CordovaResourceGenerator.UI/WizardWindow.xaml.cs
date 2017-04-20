using System.Windows;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Code behind for the wizard window.
    /// </summary>
    public partial class WizardWindow : Window
    {
        /// <summary>
        /// Creates a new instance of the <see cref="WizardWindow"/> class.
        /// </summary>
        /// <param name="viewModel">The wizard window view model.</param>
        public WizardWindow(WizardViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}