using System.Windows;

namespace CordovaResourceGenerator.UI
{
    /// <summary>
    /// A attached property that specifies when the window should close.
    /// </summary>
    public static class ShouldWindowClose
    {
        /// <summary>
        /// The dependency property of the attached property.
        /// </summary>
        public static readonly DependencyProperty ShouldWindowCloseAttachedProperty = DependencyProperty.RegisterAttached
        (
            name: "Value",
            ownerType: typeof(ShouldWindowClose),
            propertyType: typeof(bool),
            defaultMetadata: new PropertyMetadata(false, ShouldWindowCloseChanged)
        );

        /// <summary>
        /// Sets the value of the attached property.
        /// </summary>
        /// <param name="target">The window target.</param>
        /// <param name="value">The new value.</param>
        public static void SetValue(Window target, bool value)
        {
            target.SetValue(ShouldWindowClose.ShouldWindowCloseAttachedProperty, value);
        }

        /// <summary>
        /// Method called when the value of the property changed.
        /// </summary>
        /// <param name="dependencyObject">The dependecy object that have the attached property value changed.</param>
        /// <param name="eventArgs">The event's arguments.</param>
        private static void ShouldWindowCloseChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            //Close the window if the new value is true.
            if (eventArgs.NewValue is bool shouldClose)
                if (shouldClose && dependencyObject is Window window)
                    window.Close();
        }
    }
}