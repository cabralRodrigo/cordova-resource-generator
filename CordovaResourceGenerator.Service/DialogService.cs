using CordovaResourceGenerator.Domain.Service;
using System.Drawing;
using System.Windows.Forms;

namespace CordovaResourceGenerator.Service
{
    /// <summary>
    /// Service to all login related for any dialog on the application.
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Opens an open color dialog.
        /// </summary>
        /// <param name="selectedColor">The dialo'g selected color.</param>
        /// <returns>The selected color, null if the user exit the dialog early.</returns>
        public Color? OpenColorDialog(Color selectedColor)
        {
            using (var dialog = new ColorDialog())
            {
                dialog.Color = selectedColor;
                dialog.AllowFullOpen = true;
                dialog.AnyColor = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                    return dialog.Color;
                else
                    return null;
            }
        }

        /// <summary>
        /// Opens an open file dialog.
        /// </summary>
        /// <param name="title">The dialog's title.</param>
        /// <param name="filter">The dialog's filter.</param>
        /// <returns>The full path of the selected file, null if the user exit the dialog early.</returns>
        public string OpenFileDialog(string title, string filter)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = title;
                dialog.Filter = filter;

                if (dialog.ShowDialog() == DialogResult.OK)
                    return dialog.FileName;
                else
                    return null;
            }
        }

        /// <summary>
        /// Opens an open folder dialog.
        /// </summary>
        /// <param name="title">The dialog's title.</param>
        /// <param name="selectedPath">The dialog's selected path.</param>
        /// <returns>The full path of the selected folder, null if the user exit the dialog early.</returns>
        public string OpenFolderDialog(string title, string selectedPath)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = title;
                dialog.SelectedPath = selectedPath;

                if (dialog.ShowDialog() == DialogResult.OK)
                    return dialog.SelectedPath;
                else
                    return null;
            }
        }
    }
}