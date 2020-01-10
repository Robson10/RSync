using RSync.Core.Extends;
using System;
using System.Globalization;
using System.Windows.Data;

namespace RSync.Core.ViewConverters
{
    /// <summary>
    /// Converter from enum to text localized in res file. Enum need Resource attribute.
    /// </summary>
    public class EnumToResourceConverter : IValueConverter
    {
        /// <summary>
        /// Converter from enum to text localized in res file. Enum need Resource attribute
        /// </summary>
        /// <param name="value">Enum value.</param>
        /// <param name="targetType">Ignored.</param>
        /// <param name="parameter">Ignored.</param>
        /// <param name="culture">Ignored.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string valueToDipslay = ((Enum)value).GetResourceValue();
                return valueToDipslay;
            }
            return string.Empty;
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