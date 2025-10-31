using System.Data.Entity;

namespace PhoneBook
{
    // DbContext
    public class PhoneBookContext : DbContext
    {
        #region DbSets
        public DbSet<Contact> Contacts { get; set; }

        #endregion
        #region Ctor
        public PhoneBookContext() : base("name=PhoneBookConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PhoneBookContext>());
        } 
        #endregion

    }
}
