using System.Windows;
using System.Windows.Controls;

namespace RSync.Helpers
{
    /// <summary>
    /// Assistant bind password from PasswordBox to property.
    /// </summary>
    public static class PasswordBoxAssistant
    {
        /// <summary>
        /// Bound password property
        /// </summary>
        public static readonly DependencyProperty BoundPassword =
             DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxAssistant), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));
        
        /// <summary>
        /// Bind password property
        /// </summary>
        public static readonly DependencyProperty BindPassword = 
            DependencyProperty.RegisterAttached("BindPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false, OnBindPasswordChanged));

        /// <summary>
        /// Updating password property.
        /// </summary>
        private static readonly DependencyProperty UpdatingPassword =
            DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false));

        /// <summary>
        /// On bound password changed.
        /// </summary>
        /// <param name="d">Dependency object.</param>
        /// <param name="e">Event args.</param>
        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox box = d as PasswordBox;

            // only handle this event when the property is attached to a PasswordBox
            // and when the BindPassword attached property has been set to true
            if (d == null || !GetBindPassword(d))
            {
                return;
            }

            // avoid recursive updating by ignoring the box's changed event
            box.PasswordChanged -= HandlePasswordChanged;

            string newPassword = (string)e.NewValue;

            if (!GetUpdatingPassword(box))
            {
                box.Password = newPassword;
            }

            box.PasswordChanged += HandlePasswordChanged;
        }

        /// <summary>
        /// On Bind password changed.
        /// </summary>
        /// <param name="dp">Dependency object.</param>
        /// <param name="e">Event args.</param>
        private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            // when the BindPassword attached property is set on a PasswordBox,
            // start listening to its PasswordChanged event

            PasswordBox box = dp as PasswordBox;

            if (box == null)
            {
                return;
            }

            bool wasBound = (bool)(e.OldValue);
            bool needToBind = (bool)(e.NewValue);

            if (wasBound)
            {
                box.PasswordChanged -= HandlePasswordChanged;
            }

            if (needToBind)
            {
                box.PasswordChanged += HandlePasswordChanged;
            }
        }

        /// <summary>
        /// Handle password changed.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args.</param>
        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox box = sender as PasswordBox;

            // set a flag to indicate that we're updating the password
            SetUpdatingPassword(box, true);
            // push the new password into the BoundPassword property
            SetBoundPassword(box, box.Password);
            SetUpdatingPassword(box, false);
        }

        /// <summary>
        /// Set bind password property.
        /// </summary>
        /// <param name="dp">Dependency object.</param>
        /// <param name="value">Is password should be bind to property</param>
        public static void SetBindPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(BindPassword, value);
        }

        /// <summary>
        /// Return is password bind.
        /// </summary>
        /// <param name="dp">Dependency object.</param>
        /// <returns>Is password bind.</returns>
        public static bool GetBindPassword(DependencyObject dp)
        {
            return (bool)dp.GetValue(BindPassword);
        }

        /// <summary>
        /// Get password value.
        /// </summary>
        /// <param name="dp">Dependency object.</param>
        /// <returns>Password value.</returns>
        public static string GetBoundPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(BoundPassword);
        }

        /// <summary>
        /// Set password value.
        /// </summary>
        /// <param name="dp">Dependency object.</param>
        /// <param name="value">Password text.</param>
        public static void SetBoundPassword(DependencyObject dp, string value)
        {
            dp.SetValue(BoundPassword, value);
        }

        /// <summary>
        /// Get UpdateingPassword property.
        /// </summary>
        /// <param name="dp">Dependency object.</param>
        /// <returns>UpdatingPassword property.</returns>
        private static bool GetUpdatingPassword(DependencyObject dp)
        {
            return (bool)dp.GetValue(UpdatingPassword);
        }

        /// <summary>
        /// Set updating password property.
        /// </summary>
        /// <param name="dp">Dependency object.</param>
        /// <param name="value">Is updating password.</param>
        private static void SetUpdatingPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(UpdatingPassword, value);
        }
    }
}
