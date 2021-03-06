﻿using System.Collections.Generic;

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

        /// <summary>
        /// Accounts
        /// </summary>
        private static List<Account> accounts;

        /// <summary>
        /// Settings property
        /// </summary>
        public static List<Account> Accounts
        {
            get
            {
                if (accounts == null)
                {
                    accounts = AccountInitialize();
                }

                return accounts;
            }
            set => accounts = value;
        }

        private static List<Account> AccountInitialize()
        {

            return new List<Account>();
        }
    }
}