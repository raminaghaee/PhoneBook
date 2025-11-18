using PhoneBook.BL;
using PhoneBook.BL.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.Tests
{
    // Mock Repository - جایگزین دیتابیس واقعی برای تست
    // هدف: تست بدون وابستگی به دیتابیس
    public class MockContactRepository : IContactRepository
    {
        private readonly List<Contact> _contacts;
        private int _nextId;

        public MockContactRepository()
        {
            _contacts = new List<Contact>();
            _nextId = 1;
        }

        public List<Contact> GetAll()
        {
            return _contacts.ToList();
        }

        public Contact GetById(int id)
        {
            return _contacts.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Contact contact)
        {
            contact.Id = _nextId++;
            _contacts.Add(contact);
        }

        public void Update(Contact contact)
        {
            var existing = _contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existing != null)
            {
                existing.Name = contact.Name;
                existing.Phone = contact.Phone;
                existing.Address = contact.Address;
            }
        }

        public void Delete(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
        }
    }

}
