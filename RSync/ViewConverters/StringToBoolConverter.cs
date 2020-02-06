using RSync.Core.Extends;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace RSync.ViewConverters
{
    /// <summary>
    /// Converter from string to boolean property.
    /// </summary>
    public class StringToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Converter from string to boolean.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <param name="targetType">Ignored.</param>
        /// <param name="parameter">Ignored.</param>
        /// <param name="culture">Ignored.</param>
        /// <returns>False when string is empty.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visibility = true;

            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                visibility = false;
            }

            return visibility;
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