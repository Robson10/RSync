using System.Windows;

namespace RSync.Helpers
{
    /// <summary>
    /// Closing window interface for ViewModel.
    /// </summary>
    public interface IWindowClose
    {
        /// <summary>
        /// Close window with specified dialog result.
        /// </summary>
        /// <param name="window">Window.</param>
        /// <param name="dialogResult">Dialog result.</param>
        void CloseWindow(Window window, bool dialogResult)
        {
            if (window != null)
            {
                window.DialogResult = dialogResult;
                //window.Close();
            }
        }
    }
}
