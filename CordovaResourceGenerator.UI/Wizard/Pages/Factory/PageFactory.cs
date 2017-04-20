using CordovaResourceGenerator.UI.Properties;
using SimpleInjector;
using System;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// The factory that creates the wizard's pages.
    /// </summary>
    public class PageFactory : IPageFactory
    {
        /// <summary>
        /// The IoC container.
        /// </summary>
        private readonly Container container;

        /// <summary>
        /// Creates a new instance of the <see cref="PageFactory"/> class.
        /// </summary>
        /// <param name="container">The IoC container.</param>
        public PageFactory(Container container)
        {
            this.container = container;
        }

        /// <summary>
        /// Create a new instance of a wizard page.
        /// </summary>
        /// <param name="parent">The view model of the parent wizard.</param>
        /// <param name="pageType">The type of the page.</param>
        /// <returns>New instance of a wizard page.</returns>
        public IWizardPage CreatePage(WizardViewModel parent, WizardPages pageType)
        {
            IWizardPage page;

            switch (pageType)
            {
                case WizardPages.Introduction:
                    page = this.container.GetInstance<IntroductionWizardPage>();
                    break;
                case WizardPages.SelectConfig:
                    page = this.container.GetInstance<SelectConfigWizardPage>();
                    break;
                case WizardPages.SelectImages:
                    page = this.container.GetInstance<SelectImagesWizardPage>();
                    break;
                case WizardPages.SelectColorAndOutput:
                    page = this.container.GetInstance<SelectColorAndOutputWizardPage>();
                    break;
                case WizardPages.Finished:
                    page = this.container.GetInstance<FinishedWizardPage>();
                    break;
                default:
                    throw new Exception(Resources.PageFactory_PageTypeInvalid);
            }

            page.ViewModel.Parent = parent;
            return page;
        }
    }
}