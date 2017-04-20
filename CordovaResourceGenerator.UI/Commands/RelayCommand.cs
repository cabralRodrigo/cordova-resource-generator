using System;
using System.Windows.Input;

namespace CordovaResourceGenerator.UI.Commands
{
    /// <summary>
    /// Command responsible for encapsulate methods in a view model.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Event fired when the commands state might changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// The action of the command.
        /// </summary>
        private readonly Action<object> action;

        /// <summary>
        /// Creates a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="action">The action of the command.</param>
        public RelayCommand(Action<object> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// Determines if the command can be executed or not.
        /// </summary>
        /// <param name="parameter">The wizard page.</param>
        /// <returns>True if the command can be executed, False if not.</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="parameter">Parameter of the command.</param>
        public void Execute(object parameter) => this.action(parameter);
    }
}