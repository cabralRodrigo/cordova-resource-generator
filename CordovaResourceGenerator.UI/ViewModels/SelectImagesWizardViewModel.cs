using CordovaResourceGenerator.Domain.Service;
using CordovaResourceGenerator.UI.Commands;
using CordovaResourceGenerator.UI.Properties;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// The view model for the select images wizard page.
    /// </summary>
    public class SelectImagesWizardViewModel : BaseViewModel, IWizardPageViewModel
    {
        /// <summary>
        /// The dialog service.
        /// </summary>
        private readonly IDialogService dialogService;

        /// <summary>
        /// The type of the page.
        /// </summary>
        public WizardPages PageType => WizardPages.SelectImages;

        /// <summary>
        /// The parent view model.
        /// </summary>
        public WizardViewModel Parent { get; set; }

        /// <summary>
        /// Command that opens an open file dialog to the user select the icon source image.
        /// </summary>
        public ICommand OpenFileDialogIconCommand { get; }

        /// <summary>
        /// Command that opens an open file dialog to the user select the splash source image.
        /// </summary>
        public ICommand OpenFileDialogSplashCommand { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="SelectImagesWizardViewModel"/> class.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        public SelectImagesWizardViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            this.OpenFileDialogIconCommand = new RelayCommand(_ => this.OpenFileDialogIcon());
            this.OpenFileDialogSplashCommand = new RelayCommand(_ => this.OpenFileDialogSplash());
        }

        /// <summary>
        /// Method called before the view navigates.
        /// </summary>
        /// <param name="navigationType">The type of the navigation.</param>
        /// <returns>True if the view should navigate, False if not.</returns>
        public bool BeforeNavigate(NavigationType navigationType)
        {
            if (navigationType == NavigationType.Next)
            {
                //If the icon is set and the splash is not, warning the user that icon source image will be used to generate the splash.
                if (File.Exists(this.Parent.IconPath) && (string.IsNullOrEmpty(this.Parent.SplashPath) || !File.Exists(this.Parent.SplashPath)))
                    return MessageBox.Show(
                        messageBoxText: Resources.SelectImagesWizardPage_OnlyIconSelected_MessagBox_Message,
                        caption: Resources.SelectImagesWizardPage_OnlyIconSelected_MessagBox_Title,
                        button: MessageBoxButton.OKCancel,
                        icon: MessageBoxImage.Warning,
                        defaultResult: MessageBoxResult.OK) == MessageBoxResult.OK;
                else
                    return true;
            }
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
                return File.Exists(this.Parent.IconPath);
            else
                return true;
        }

        /// <summary>
        /// Opens an open file dialog to the user select the icon source image.
        /// </summary>
        private void OpenFileDialogIcon()
        {
            var iconPath = this.dialogService.OpenFileDialog(Resources.SelectImagesWizardPage_FileDialogIcon_Title, $"{Resources.SelectImagesWizardPage_FileDialog_Filter}|*.png");
            if (!string.IsNullOrEmpty(iconPath))
                this.Parent.IconPath = iconPath;
        }

        /// <summary>
        /// Opens an open file dialog to the user select the splash source image.
        /// </summary>
        private void OpenFileDialogSplash()
        {
            var splashPath = this.dialogService.OpenFileDialog(Resources.SelectImagesWizardPage_FileDialogSplash_Title, $"{Resources.SelectImagesWizardPage_FileDialog_Filter}|*.png");
            if (!string.IsNullOrEmpty(splashPath))
                this.Parent.SplashPath = splashPath;
        }
    }
}