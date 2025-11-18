using PhoneBook.BL.IRepositories;
using PhoneBook.BL.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.BL.Services
{
    public class ContactService
    {
        #region fields
        private readonly IContactRepository _repository;
        #endregion
        #region Ctors
        // Dependency Injection - وابستگی از طریق constructor تزریق می‌شود
        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }
        #endregion
        #region Operations
        public List<ContactDto> GetAllContacts()
        {
            var contacts = _repository.GetAll();
            return contacts.Select(MapToDto).ToList();
        }

        public TlsResult CreateContact(ContactDto dto)
        {
            // اعتبارسنجی
            var validationResult = ValidateContact(dto);
            if (!validationResult.IsSuccess)
                return validationResult;

            // ذخیره در دیتابیس
            var contact = MapToEntity(dto);
            _repository.Add(contact);

            return TlsResult.Success("مخاطب با موفقیت اضافه شد");
        }

        public TlsResult UpdateContact(ContactDto dto)
        {
            // بررسی وجود مخاطب
            if (dto.Id <= 0)
                return TlsResult.Failure("شناسه مخاطب نامعتبر است");

            var existingContact = _repository.GetById(dto.Id);
            if (existingContact == null)
                return TlsResult.Failure("مخاطب یافت نشد");

            // اعتبارسنجی
            var validationResult = ValidateContact(dto);
            if (!validationResult.IsSuccess)
                return validationResult;

            // بروزرسانی
            var contact = MapToEntity(dto);
            _repository.Update(contact);

            return TlsResult.Success("مخاطب با موفقیت ویرایش شد");
        }

        public TlsResult DeleteContact(int id)
        {
            if (id <= 0)
                return TlsResult.Failure("شناسه مخاطب نامعتبر است");

            var contact = _repository.GetById(id);
            if (contact == null)
                return TlsResult.Failure("مخاطب یافت نشد");

            _repository.Delete(id);
            return TlsResult.Success("مخاطب با موفقیت حذف شد");
        }

        #endregion
        #region Validations
        // اعتبارسنجی داده‌ها
        private TlsResult ValidateContact(ContactDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return TlsResult.Failure("لطفا نام را وارد کنید");

            if (string.IsNullOrWhiteSpace(dto.Phone))
                return TlsResult.Failure("لطفا شماره تلفن را وارد کنید");

            if (dto.Name.Length > 100)
                return TlsResult.Failure("نام نباید بیشتر از 100 کاراکتر باشد");

            if (dto.Phone.Length > 20)
                return TlsResult.Failure("شماره تلفن نباید بیشتر از 20 کاراکتر باشد");

            return TlsResult.Success();
        }
        #endregion
        #region Mapper
        // تبدیل Entity به DTO
        private ContactDto MapToDto(Contact contact)
        {
            return new ContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                Phone = contact.Phone,
                Address = contact.Address ?? ""
            };
        }

        // تبدیل DTO به Entity
        private Contact MapToEntity(ContactDto dto)
        {
            return new Contact
            {
                Id = dto.Id,
                Name = dto.Name.Trim(),
                Phone = dto.Phone.Trim(),
                Address = dto.Address?.Trim()
            };
        } 
        #endregion
    }
}
