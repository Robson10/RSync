﻿using RSync.AppResources.Localization;
using RSync.Core.Extends;
using System.ComponentModel;

namespace RSync.Core.Enumerations
{
    /// <summary>
    /// Enum containing all supported languages by application.
    /// </summary>
    public enum AppLanguage
    {
        /// <summary>
        /// Default English language. Use resource file res.en-GB.resx
        /// </summary>
        [Resource(nameof(res.EnumAppLanguageEnglish))]
        [Description("en-GB")]
        [DefaultValue(true)]
        English = 0,

        /// <summary>
        /// Polish language. Use resource file res.pl-PL.resx
        /// </summary>
        [Resource(nameof(res.EnumAppLanguagePolish))]
        [Description("pl-PL")]
        Polish = 1,
    }
}