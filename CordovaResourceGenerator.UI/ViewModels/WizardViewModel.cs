using CordovaResourceGenerator.Domain.Service;
using CordovaResourceGenerator.UI.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Business logic for the wizard window.
    /// </summary>
    public class WizardViewModel : BaseViewModel, IWizard
    {
        #region Properties

        /// <summary>
        /// The wizard current page.
        /// </summary>
        public IWizardPage CurrentPage
        {
            get => this.currentPage;
            set => this.OnPropertyChanged(this.currentPage = value);
        }
        private IWizardPage currentPage;

        /// <summary>
        /// The full path for the cordova project xml configuration file.
        /// </summary>
        public string ConfigXmlPath
        {
            get => this.configXmlPath;
            set => this.OnPropertyChanged(this.configXmlPath = value);
        }
        private string configXmlPath;

        /// <summary>
        /// The full path for the icon source image.
        /// </summary>
        public string IconPath
        {
            get => this.iconPath;
            set
            {
                this.OnPropertyChanged(this.iconPath = value);

                //Raise the property changed event for all calculated properties too.
                this.OnPropertyChanged((object)null, nameof(this.IconFileName));
            }
        }
        private string iconPath;

        /// <summary>
        /// The full path for the splash source image.
        /// </summary>
        public string SplashPath
        {
            get => this.splashPath;
            set
            {
                this.OnPropertyChanged(this.splashPath = value);

                //Raise the property changed event for all calculated properties too.
                this.OnPropertyChanged((object)null, nameof(this.SplashFileName));
            }
        }
        private string splashPath;

        /// <summary>
        /// The full path for the output folder.
        /// </summary>
        public string OutputPath
        {
            get => this.outputPath;
            set => this.OnPropertyChanged(this.outputPath = value);
        }
        private string outputPath;

        /// <summary>
        /// The background color of the generated images (icon & splash).
        /// </summary>
        public Color BackgroundColor
        {
            get => this.backgroundColor;
            set => this.OnPropertyChanged(this.backgroundColor = value);
        }
        private Color backgroundColor;

        /// <summary>
        /// Flag indication that the wizard should close.
        /// </summary>
        public bool ShouldClose
        {
            get => this.shoudClose;
            set => this.OnPropertyChanged(this.shoudClose = value);
        }
        private bool shoudClose;

        #endregion

        #region Calculated Properties

        /// <summary>
        /// The file name of the icon source image.
        /// </summary>
        public string IconFileName
        {
            get
            {
                if (string.IsNullOrEmpty(this.IconPath))
                    return null;
                else
                    return Path.GetFileName(this.IconPath);
            }
        }

        /// <summary>
        /// The file name of the splash source image.
        /// </summary>
        public string SplashFileName
        {
            get
            {
                if (string.IsNullOrEmpty(this.SplashPath))
                    return null;
                else
                    return Path.GetFileName(this.SplashPath);
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command for go to the next page on the wizard.
        /// </summary>
        public ICommand NavigateNextCommand { get; private set; }

        /// <summary>
        /// Command for go to the previous page on the wizard.
        /// </summary>
        public ICommand NavigatePreviousCommand { get; private set; }

        /// <summary>
        /// Command for cancel the wizard.
        /// </summary>
        public ICommand NavigateCancelCommand { get; private set; }

        #endregion

        #region Dependencies

        /// <summary>
        /// The page factory.
        /// </summary>
        private readonly IPageFactory pageFactory;

        /// <summary>
        /// The cordova project service.
        /// </summary>
        private readonly ICordovaProjectService cordovaService;

        /// <summary>
        /// The image generation service.
        /// </summary>
        private readonly IImageGenerationService imageGenerationService;

        #endregion

        /// <summary>
        /// Creates a new instance of the class <see cref="WizardViewModel"/>.
        /// </summary>
        /// <param name="pageFactory">The page factory.</param>
        /// <param name="cordovaService">The cordova project service.</param>
        /// <param name="imageGenerationService">The image generation service.</param>
        public WizardViewModel(IPageFactory pageFactory, ICordovaProjectService cordovaService, IImageGenerationService imageGenerationService)
        {
            this.pageFactory = pageFactory;
            this.cordovaService = cordovaService;
            this.imageGenerationService = imageGenerationService;

            this.NavigateNextCommand = new NavigateCommand(this, NavigationType.Next);
            this.NavigatePreviousCommand = new NavigateCommand(this, NavigationType.Previous);
            this.NavigateCancelCommand = new NavigateCommand(this, NavigationType.Cancel);

            this.CurrentPage = this.pageFactory.CreatePage(this, WizardPages.Introduction);
            this.BackgroundColor = Color.FromArgb(0, 0, 0, 0);
        }

        /// <summary>
        /// Generate icons and splashs.
        /// </summary>
        /// <returns>True if the generation ends successfully, False if not.</returns>
        public bool GenerateIconAndSplash()
        {
            try
            {
                //Extracts the platforms information from the cordova project xml file.
                var platforms = this.cordovaService.ExtractPlatforms(this.ConfigXmlPath);

                //If the splash path isn't set, then use the icon for generate the splash too.
                string newSplashPath;
                if (!string.IsNullOrEmpty(this.SplashPath) && File.Exists(this.SplashPath))
                    newSplashPath = this.SplashPath;
                else
                    newSplashPath = this.IconPath;

                //Generate all the images (icon & splash).
                this.imageGenerationService.Generate
                (
                    outputFolder: this.OutputPath,
                    iconSource: this.IconPath,
                    splashSource: newSplashPath,
                    backgroundColor: this.BackgroundColor,
                    platforms: platforms
                );

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show
                (
                    messageBoxText: string.Format(Resources.WizardWindow_Generate_Error_Description, ex.Message),
                    caption: Resources.WizardWindow_Generate_Error_Title,
                    button: MessageBoxButton.OK,
                    icon: MessageBoxImage.Error
                );

                return false;
            }
        }

        /// <summary>
        /// Navigate the current page of the wizard.
        /// </summary>
        /// <param name="navigationType">The type of the navigation.</param>
        public void Navigate(NavigationType navigationType)
        {
            //Holds the offset which will be used to determinate the next page.
            int offset;

            switch (navigationType)
            {
                case NavigationType.Previous:
                    offset = -1;
                    break;
                case NavigationType.Next:
                    offset = 1;
                    break;
                case NavigationType.Cancel:
                    this.ShouldClose = true;
                    return;
                default:
                    throw new Exception(Resources.WizardWindow_UnknownError);
            }

            //Calculate the next page and if the page doesn't exists, sets to the introduction.
            var newPageType = this.CurrentPage.ViewModel.PageType + offset;
            if (!Enum.IsDefined(typeof(WizardPages), newPageType))
                newPageType = WizardPages.Introduction;

            var pageChanged = newPageType != this.CurrentPage.ViewModel.PageType;
            var shouldNavigate = this.CurrentPage.ViewModel.BeforeNavigate(navigationType);

            //Only navigate if the new page is different from the current and if the current page is valid.
            if (pageChanged && shouldNavigate)
            {
                //Creates the new page and sets to the view model.
                this.CurrentPage = this.pageFactory.CreatePage(this, newPageType);

                //Forces the window's commands to refresh the 'CanExecute' property.
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}