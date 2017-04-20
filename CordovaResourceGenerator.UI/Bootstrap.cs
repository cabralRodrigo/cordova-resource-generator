using CordovaResourceGenerator.Domain.Service;
using CordovaResourceGenerator.Service;
using SimpleInjector;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Bootstrap the application.
    /// </summary>
    internal static class Bootstrap
    {
        /// <summary>
        /// Configures the IoC container for tha application.
        /// </summary>
        /// <param name="container">The IoC container.</param>
        public static void ConfigInjectionDependency(Container container)
        {
            container.RegisterSingleton<WizardWindow>();
            container.RegisterSingleton<WizardViewModel>();
            container.RegisterSingleton<IPageFactory, PageFactory>();

            container.Register<IntroductionWizardPage>();
            container.Register<SelectConfigWizardPage>();
            container.Register<SelectImagesWizardPage>();
            container.Register<SelectColorAndOutputWizardPage>();
            container.Register<FinishedWizardPage>();

            container.RegisterSingleton<ICordovaProjectService, CordovaProjectService>();
            container.RegisterSingleton<IAndroidService, AndroidService>();
            container.RegisterSingleton<IDialogService, DialogService>();
            container.RegisterSingleton<IImageGenerationService, ImageGenerationService>();

            container.Verify();
        }
    }
}