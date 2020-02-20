using Microsoft.EntityFrameworkCore;
using RSync.AppResources.Configuration;
using RSync.Domain.Model;

namespace RSync.Domain
{
    public class SQLiteDBContext : DbContext
    {
        //Add-Migration InitialCreate
        //Update-Database
        public DbSet<User> User { get; set; }
        public DbSet<Server> Server { get; set; }
        public DbSet<Settings> Setting { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(string.Format("Data Source={0}", config.SqLiteDataBaseFilePath));
        }
    }
}
