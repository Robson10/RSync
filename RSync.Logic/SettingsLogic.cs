using RSync.Domain;
using RSync.Domain.Model;
using System;
using System.Linq;

namespace RSync.Logic
{
    /// <summary>
    /// Settings logic.
    /// </summary>
    public class SettingsLogic : LogicContext
    {
        /// <summary>
        /// Return settings object based on User id.
        /// </summary>
        /// <param name="UserId">User id.</param>
        /// <returns>Settings object. Null if not found.</returns>
        public static Settings GetSettings(int? UserId)
        {
            if (UserId.HasValue)
            {
                return context.Setting.Where(x => x.UserId == UserId).FirstOrDefault();
            }

            return null;
        }

        /// <summary>
        /// Create settings object.
        /// </summary>
        /// <param name="settings">Settings object.</param>
        public static void AddSettings(Settings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException();
            }

            context.Add(settings);
            context.SaveChanges();
        }

        public static void UpdateSetings(Settings settings)
        {
            var entity = context.Setting.Find(settings.SettingsId);
            if (entity == null)
            {
                return;
            }

            context.Entry(entity).CurrentValues.SetValues(settings);
            context.SaveChanges();
        }
    }
}