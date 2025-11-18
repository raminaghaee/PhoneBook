using PhoneBook.BL;
using System.Data.Entity;

namespace PhoneBook.DA
{
    // DbContext
    public class PhoneBookContext : DbContext
    {
        #region DbSets
        public DbSet<Contact> Contacts { get; set; }
        #endregion
        #region Ctor
        public PhoneBookContext() : base(GetConnectionString())
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PhoneBookContext>());
        }

        private static string GetConnectionString()
        {
            var configConnection = System.Configuration.ConfigurationManager.ConnectionStrings["PhoneBookConnection"];
            if (configConnection != null)
                return configConnection.ConnectionString;

            return @"Data Source=localhost;Initial Catalog=PhoneBookDB;Integrated Security=True;TrustServerCertificate=True";
        }
        #endregion
    }
}