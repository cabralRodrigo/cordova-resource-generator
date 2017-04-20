using System;
using System.Windows.Input;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Command responsible for controling the navigation of the wizard.
    /// </summary>
    public class NavigateCommand : ICommand
    {
        /// <summary>
        /// The wizard.
        /// </summary>
        private readonly IWizard wizard;

        /// <summary>
        /// The command's navigation type.
        /// </summary>
        private readonly NavigationType navigationType;

        /// <summary>
        /// Event fired when the commands state might changed.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Creates a new instace of the <see cref="NavigateCommand"/> class.
        /// </summary>
        /// <param name="wizard">The wizard.</param>
        /// <param name="navigationType">The command's navigation type.</param>
        public NavigateCommand(IWizard wizard, NavigationType navigationType)
        {
            this.wizard = wizard;
            this.navigationType = navigationType;
        }

        /// <summary>
        /// Determines if the command can be executed or not.
        /// </summary>
        /// <param name="parameter">The wizard page.</param>
        /// <returns>True if the command can be executed, False if not.</returns>
        public bool CanExecute(object parameter)
        {
            if (parameter is IWizardPage page)
                return page.ViewModel.CanNavigate(this.navigationType);
            else
                return false;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="parameter">Parameter not used.</param>
        public void Execute(object parameter)
        {
            this.wizard.Navigate(this.navigationType);
        }
    }
}