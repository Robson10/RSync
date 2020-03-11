using System.Windows;

namespace RSync.Helpers
{
    /// <summary>
    /// Changing visible of window interface for ViewModel.
    /// </summary>
    public interface IWindowVisible
    {
        /// <summary>
        /// Change visible of window.
        /// </summary>
        /// <param name="window">Window.</param>
        /// <param name="visibility">Window visibility.</param>
        void HideWindow(Window window, Visibility visibility)
        {
            if (window != null)
            {
                window.Visibility = visibility;
            }
        }
    }
}
