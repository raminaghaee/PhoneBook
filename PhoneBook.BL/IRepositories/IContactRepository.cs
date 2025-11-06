using System.Collections.Generic;

namespace PhoneBook.BL.IRepositories
{
    // DESIGN PATTERN: Repository Pattern
    // هدف: جداسازی منطق دسترسی به داده از بقیه لایه‌ها
    // مزیت: تست‌پذیری بالا، تغییرات دیتابیس بدون تاثیر روی کد

    // Interface - قرارداد عملیات دیتابیس (Dependency Inversion - SOLID)
    public interface IContactRepository
    {
        List<Contact> GetAll();
        Contact GetById(int id);
        void Add(Contact contact);
        void Update(Contact contact);
        void Delete(int id);
    }
}
