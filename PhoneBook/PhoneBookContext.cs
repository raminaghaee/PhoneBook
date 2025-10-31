using System.Data.Entity;

namespace PhoneBook
{
    // DbContext
    public class PhoneBookContext : DbContext
    {
        #region Ctor
        public PhoneBookContext() : base("name=PhoneBookConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PhoneBookContext>());
        } 
        #endregion

    }
}
