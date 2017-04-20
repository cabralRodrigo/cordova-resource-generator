using CordovaResourceGenerator.Domain.Service;
using CordovaResourceGenerator.UI.Commands;
using CordovaResourceGenerator.UI.Properties;
using System.IO;
using System.Windows.Input;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// The view model for the select config wizard page.
    /// </summary>
    public class SelectConfigWizardViewModel : BaseViewModel, IWizardPageViewModel
    {
        /// <summary>
        /// The dialog service.
        /// </summary>
        private readonly IDialogService dialogService;

        /// <summary>
        /// The type of the page.
        /// </summary>
        public WizardPages PageType => WizardPages.SelectConfig;

        /// <summary>
        /// The parent view model.
        /// </summary>
        public WizardViewModel Parent { get; set; }

        /// <summary>
        /// Command that opens an open file dialog to select the cordova project xml configuration file.
        /// </summary>
        public ICommand OpenFileDialogCordovaProjectCommand { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="SelectConfigWizardViewModel"/> class.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        public SelectConfigWizardViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.OpenFileDialogCordovaProjectCommand = new RelayCommand(_ => this.OpenFileDialogConfigXml());
        }

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
            if (navigationType == NavigationType.Next)
                return File.Exists(this.Parent.ConfigXmlPath);
            else
                return true;
        }

        /// <summary>
        /// Opens an open file dialog to select the cordova project xml configuration file.
        /// </summary>
        private void OpenFileDialogConfigXml()
        {
            var file = this.dialogService.OpenFileDialog(Resources.SelectConfigWizardPage_Description, $"{Resources.SelectConfigWizardPage_FileDialog_Filter}|config.xml");

            if (!string.IsNullOrEmpty(file))
                this.Parent.ConfigXmlPath = file;
        }
    }
}