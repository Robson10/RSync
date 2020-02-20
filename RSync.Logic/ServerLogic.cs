using RSync.Core.Enumerations;
using RSync.Domain;
using RSync.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace RSync.Logic
{
    public class ServerLogic:LogicContext
    {
        /// <summary>
        /// Return all servers pinned to user account.
        /// </summary>
        /// <returns>All servers for user</returns>
        public static IEnumerable<Server> GetAllServers(int? UserId)
        {
            IEnumerable<Server> servers = Enumerable.Empty<Server>();

            if (UserId.HasValue)
            {
                //using (SQLiteDBContext context = new SQLiteDBContext())
                {
                    servers = context.Server.Where(x => x.UserId == UserId);
                }
            }

            return servers;
        }

        /// <summary>
        /// Validate is CustomName is in use.
        /// </summary>
        /// <param name="CustomName">Custom name for user given by user.</param>
        /// <returns>True if CustomName already exist.</returns>
        public static bool IsCustomNameExist(string CustomName)
        {
            bool isExist = false;

            //using (SQLiteDBContext context = new SQLiteDBContext())
            {
                isExist = context.Server.Any(x => x.CustomName.Equals(CustomName));
            }

            return isExist;
        }

        /// <summary>
        /// Validate combination login and server name has not already been used.
        /// </summary>
        /// <param name="login">Server login.</param>
        /// <param name="serverName">Name of server.</param>
        /// <returns>True if combination exist.</returns>
        public static bool IsServerExist(string login, ServerType serverName)
        {
            bool isExist = false;

            //using (SQLiteDBContext context = new SQLiteDBContext())
            {
                isExist = context.Server.Any(x => (x.Login.Equals(login) && x.ServerType == serverName));
            }

            return isExist;
        }
    }
}