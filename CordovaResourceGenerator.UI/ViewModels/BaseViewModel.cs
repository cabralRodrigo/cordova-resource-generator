using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// Base view model for the application.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event that is fired when a property in the view model changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="T">The type of the property that changed.</typeparam>
        /// <param name="setter">Just a placeholder parameter to set the property value and call this method on the same statement.</param>
        /// <param name="propertyName">The name of the changed property.</param>
        /// <returns></returns>
        protected void OnPropertyChanged<T>(T setter, [CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}