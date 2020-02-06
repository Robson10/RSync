using RSync.AppResources.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace RSync.ViewConverters
{
    /// <summary>
    /// Converter from string to foreground color property.
    /// </summary>
    public class StringToForegroundConverter : IValueConverter
    {
        /// <summary>
        /// Convert string to foreground color.
        /// </summary>
        /// <param name="value">String to compare.</param>
        /// <param name="targetType">Ignored</param>
        /// <param name="parameter">Expected string.</param>
        /// <param name="culture">Ignored</param>
        /// <returns>Green if equal to parameter. Otherwise red.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush foreground = Brushes.Red;
            if(value!=null && parameter!= null)
            {
                string text = value.ToString();
                string expectedString = parameter.ToString();
                if(text.Equals(expectedString))
                {
                    foreground = Brushes.Green;
                }
            }
            return foreground;
        }

        /// <summary>
        /// Not implemented method.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
