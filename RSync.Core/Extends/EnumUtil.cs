using RSync.AppResources.Configuration;
using RSync.AppResources.Localization;
using RSync.Core.Helpers;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace RSync.Core.Extends
{
    /// <summary>
    /// Class with methods extend enum
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>Returns the description of the specified enum.</summary>
        /// <param name="value">The value of the enum for which to return the description.</param>
        /// <returns>A description of the enum, or the enum name if no description exists.</returns>
        public static string GetDescription(this Enum value)
        {
            if (value is null)
            {
                ArgumentNullException ex = new ArgumentNullException(nameof(value));
                LogHelper.LogError(ex, CultureInfo.CurrentCulture);
                throw ex;

            }

            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>Returns the value from resource file of the specified enum.</summary>
        /// <param name="value">The value of the enum for which to return the translated resource value.</param>
        /// <returns>A resource value of the enum, or the enum expected resource name if resource not exists or resource attribute not found.</returns>
        public static string GetResourceValue(this Enum value)
        {
            if (value is null)
            {
                ArgumentNullException ex = new ArgumentNullException(nameof(value));
                LogHelper.LogError(ex, CultureInfo.CurrentCulture);
                throw ex;
            }

            string valueToDisplay = string.Empty;
            FieldInfo fi = value.GetType().GetField(value.ToString());
            ResourceAttribute[] attributes = (ResourceAttribute[])fi.GetCustomAttributes(typeof(ResourceAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                string nameOfResource = attributes[0].ResourceName;
                valueToDisplay = res.ResourceManager.GetString(nameOfResource, CultureInfo.CurrentCulture);
            }

            if (string.IsNullOrEmpty(valueToDisplay))
            {
                valueToDisplay = string.Format(CultureInfo.CurrentCulture, config.EnumResourceNameFormat, value.GetType().Name, value);
            }

            return valueToDisplay;
        }

        /// <summary>Returns the enum whose description attribute equals to description parameter.</summary>
        /// <param name="enumType">The type of enum which will be searched.</param>
        /// <param name="description">The string which will be searched in enum descriptions.</param>
        /// <returns>A resource value of the enum, or the enum expected resource name if resource not exists or resource attribute not found.</returns>
        public static T GetEnumByDescription<T>(Type enumType, string description) where T : Enum
        {
            if (enumType is null)
            {
                ArgumentNullException ex = new ArgumentNullException(nameof(enumType));
                LogHelper.LogError(ex, CultureInfo.CurrentCulture);
                throw ex;
            }

            T result = default;

            foreach (FieldInfo field in enumType.GetFields())
            {
                DescriptionAttribute attribute =
                    Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute != null && attribute.Description == description)
                {
                    result = (T)field.GetValue(null);
                }
            }

            return result;
        }
    }
}