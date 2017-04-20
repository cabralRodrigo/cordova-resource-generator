using CordovaResourceGenerator.Domain.Service;
using CordovaResourceGenerator.UI.Commands;
using CordovaResourceGenerator.UI.Properties;
using System.IO;
using System.Windows.Input;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// The view model for the select color and output wizard page.
    /// </summary>
    public class SelectColorAndOutputWizardViewModel : BaseViewModel, IWizardPageViewModel
    {
        /// <summary>
        /// The dialog service.
        /// </summary>
        private readonly IDialogService dialogService;

        /// <summary>
        /// The type of the page.
        /// </summary>
        public WizardPages PageType => WizardPages.SelectColorAndOutput;

        /// <summary>
        /// The parent view model.
        /// </summary>
        public WizardViewModel Parent { get; set; }

        /// <summary>
        /// Command that open an open folder dialog to select the output folder.
        /// </summary>
        public ICommand OpenFolderOutputCommand { get; }

        /// <summary>
        /// Command that open a color dialog to select the background color.
        /// </summary>
        public ICommand OpenBackgroundColorDialogCommand { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="SelectColorAndOutputWizardViewModel"/> class.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        public SelectColorAndOutputWizardViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.OpenFolderOutputCommand = new RelayCommand(_ => this.OpenFolderOutput());
            this.OpenBackgroundColorDialogCommand = new RelayCommand(_ => this.OpenBackgroundColorDialog());
        }

        /// <summary>
        /// Method called before the view navigates.
        /// </summary>
        /// <param name="navigationType">The type of the navigation.</param>
        /// <returns>True if the view should navigate, False if not.</returns>
        public bool BeforeNavigate(NavigationType navigationType)
        {
            if (navigationType == NavigationType.Next)
                return this.Parent.GenerateIconAndSplash();
            else
                return true;
        }

        /// <summary>
        /// Determines if the page can navigate.
        /// </summary>
        /// <param name="navigationType">The type of the navigation.</param>
        /// <returns></returns>
        public bool CanNavigate(NavigationType navigationType)
        {
            if (navigationType == NavigationType.Next)
                return Directory.Exists(this.Parent.OutputPath);
            else
                return true;
        }

        /// <summary>
        /// Opens an open folder dialog to the user select the output folder.
        /// </summary>
        private void OpenFolderOutput()
        {
            var outputPath = this.dialogService.OpenFolderDialog(Resources.SelectColorAndOutputWizardPage_OutputFolder_Description, this.Parent.OutputPath);
            if (!string.IsNullOrEmpty(outputPath))
                this.Parent.OutputPath = outputPath;
        }

        /// <summary>
        /// Opens a color dialog to the user selecte the background color.
        /// </summary>
        private void OpenBackgroundColorDialog()
        {
            var color = this.dialogService.OpenColorDialog(this.Parent.BackgroundColor);

            if (color.HasValue)
                this.Parent.BackgroundColor = color.Value;
        }
    }
}