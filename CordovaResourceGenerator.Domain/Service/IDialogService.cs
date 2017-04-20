using System.Drawing;

namespace CordovaResourceGenerator.Domain.Service
{
    /// <summary>
    /// Service to all login related for any dialog on the application.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Opens an open file dialog.
        /// </summary>
        /// <param name="title">The dialog's title.</param>
        /// <param name="filter">The dialog's filter.</param>
        /// <returns>The full path of the selected file, null if the user exit the dialog early.</returns>
        string OpenFileDialog(string title, string filter);

        /// <summary>
        /// Opens an open folder dialog.
        /// </summary>
        /// <param name="title">The dialog's title.</param>
        /// <param name="selectedPath">The dialog's selected path.</param>
        /// <returns>The full path of the selected folder, null if the user exit the dialog early.</returns>
        string OpenFolderDialog(string title, string selectedPath);

        /// <summary>
        /// Opens an open color dialog.
        /// </summary>
        /// <param name="selectedColor">The dialo'g selected color.</param>
        /// <returns>The selected color, null if the user exit the dialog early.</returns>
        Color? OpenColorDialog(Color selectedColor);
    }
}