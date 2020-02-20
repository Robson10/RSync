using RSync.Domain.Model;
using System;
using System.Linq;

namespace RSync.Logic
{
    /// <summary>
    /// User logic.
    /// </summary>
    public class UserLogic : LogicContext
    {
        /// <summary>
        /// Return user by Login.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <returns>User object. Null if not found.</returns>
        public static User GetUser(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return null;
            }

            return context.User.FirstOrDefault(x => x.Login.Equals(login));
        }

        /// <summary>
        /// Get user by login and password.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">Decrypted user password.</param>
        /// <returns>Object if credentials correct otherwise null.</returns>
        public static User GetUser(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            return context.User.FirstOrDefault(x => x.Login.Equals(login) && x.Password.Equals(password));
        }

        /// <summary>
        /// Create new user object.
        /// </summary>
        /// <param name="user">User object to insert.</param>
        /// <exception cref="ArgumentNullException">Throw if User object is empty.</exception>
        /// <exception cref="ArgumentException">Throw if property of user is not correct.</exception>
        public static void AddUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(user.Login))
            {
                throw new ArgumentException(nameof(user.Login));
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentException(nameof(user.Password));
            }

            context.User.Add(user);
            context.SaveChanges();
        }

        public static void UpdateUser(User user)
        {
            User entity = context.User.Find(user.UserId);
            if (entity == null)
            {
                return;
            }

            context.Entry(entity).CurrentValues.SetValues(user);
            context.SaveChanges();
        }
    }
}
