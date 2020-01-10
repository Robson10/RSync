namespace RSync.Core.Classes
{
    /// <summary>
    /// Singleton class
    /// </summary>
    public static class Singleton
    {
        /// <summary>
        /// Settings
        /// </summary>
        private static Settings settings;

        /// <summary>
        /// Settings property
        /// </summary>
        public static Settings Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = Settings.Initialize();
                }

                return settings;
            }
            set => settings = value;
        }
    }
}