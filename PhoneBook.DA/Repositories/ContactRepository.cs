using PhoneBook.BL;
using PhoneBook.BL.IRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PhoneBook.DA.Repositories
{
    public class ContactRepository : IContactRepository
    {
        public List<Contact> GetAll()
        {
            using (var context = new PhoneBookContext())
            {
                return context.Contacts.ToList();
            }
        }
        public Contact GetById(int id)
        {
            using (var context = new PhoneBookContext())
            {
                return context.Contacts.Find(id);
            }
        }

        public void Add(Contact contact)
        {
            using (var context = new PhoneBookContext())
            {
                context.Contacts.Add(contact);
                context.SaveChanges();
            }
        }

        public void Update(Contact contact)
        {
            using (var context = new PhoneBookContext())
            {
                context.Entry(contact).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new PhoneBookContext())
            {
                var contact = context.Contacts.Find(id);
                if (contact != null)
                {
                    context.Contacts.Remove(contact);
                    context.SaveChanges();
                }
            }
        }
    }
}