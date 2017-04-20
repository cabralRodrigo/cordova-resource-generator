using CordovaResourceGenerator.UI.Commands;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// The view model for the finished wizard page.
    /// </summary>
    public class FinishedWizardViewModel : BaseViewModel, IWizardPageViewModel
    {
        /// <summary>
        /// The type of the page.
        /// </summary>
        public WizardPages PageType => WizardPages.Finished;

        /// <summary>
        /// The parent view model.
        /// </summary>
        public WizardViewModel Parent { get; set; }

        /// <summary>
        /// Command that open the output folder.
        /// </summary>
        public ICommand OpenOutputFolderCommand { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="FinishedWizardViewModel"/> class.
        /// </summary>
        public FinishedWizardViewModel()
        {
            this.OpenOutputFolderCommand = new RelayCommand(_ => this.OpenOutputFolder());
        }

        /// <summary>
        /// Determines if the page can navigate.
        /// </summary>
        /// <param name="navigationType">The type of the navigation.</param>
        /// <returns></returns>
        public bool CanNavigate(NavigationType navigationType) => false;

        /// <summary>
        /// Method called before the view navigates.
        /// </summary>
        /// <param name="navigationType">The type of the navigation.</param>
        /// <returns>True if the view should navigate, False if not.</returns>
        public bool BeforeNavigate(NavigationType navigationType) => true;

        /// <summary>
        /// Opens the output folder.
        /// </summary>
        private void OpenOutputFolder()
        {
            if (Directory.Exists(this.Parent.OutputPath))
                Process.Start("explorer.exe", this.Parent.OutputPath);
        }
    }
}