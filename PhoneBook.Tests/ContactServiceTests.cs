using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneBook.BL;
using PhoneBook.BL.IRepositories;
using PhoneBook.BL.Services;

namespace PhoneBook.Tests
{
    [TestClass]
    public class ContactServiceTests
    {
        private ContactService _contactService;
        private IContactRepository _repository;

        // این متد قبل از هر تست اجرا می‌شود
        [TestInitialize]
        public void Setup()
        {
            // آماده‌سازی: ایجاد Repository و Service
            _repository = new MockContactRepository();
            _contactService = new ContactService(_repository);
        }

        #region Create
        // CREATE TESTS - تست‌های افزودن
        [TestMethod]
        public void CreateContact_WithValidData_ShouldReturnSuccess()
        {
            // Arrange - آماده‌سازی
            var dto = new ContactDto
            {
                Name = "علی احمدی",
                Phone = "09121234567",
                Address = "تهران"
            };

            // Act - اجرا
            var result = _contactService.CreateContact(dto);

            // Assert - بررسی
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("مخاطب با موفقیت اضافه شد", result.Message);

            var contacts = _contactService.GetAllContacts();
            Assert.AreEqual(1, contacts.Count);
            Assert.AreEqual("علی احمدی", contacts[0].Name);
        }

        [TestMethod]
        public void CreateContact_WithEmptyName_ShouldReturnFailure()
        {
            // Arrange
            var dto = new ContactDto
            {
                Name = "",
                Phone = "09121234567",
                Address = "تهران"
            };

            // Act
            var result = _contactService.CreateContact(dto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("لطفا نام را وارد کنید", result.Message);

            var contacts = _contactService.GetAllContacts();
            Assert.AreEqual(0, contacts.Count);
        }

        [TestMethod]
        public void CreateContact_WithEmptyPhone_ShouldReturnFailure()
        {
            // Arrange
            var dto = new ContactDto
            {
                Name = "علی احمدی",
                Phone = "",
                Address = "تهران"
            };

            // Act
            var result = _contactService.CreateContact(dto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("لطفا شماره تلفن را وارد کنید", result.Message);
        }

        [TestMethod]
        public void CreateContact_WithLongName_ShouldReturnFailure()
        {
            // Arrange
            var dto = new ContactDto
            {
                Name = new string('ا', 101), // 101 کاراکتر
                Phone = "09121234567",
                Address = "تهران"
            };

            // Act
            var result = _contactService.CreateContact(dto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("نام نباید بیشتر از 100 کاراکتر باشد", result.Message);
        }

        [TestMethod]
        public void CreateContact_WithLongPhone_ShouldReturnFailure()
        {
            // Arrange
            var dto = new ContactDto
            {
                Name = "علی احمدی",
                Phone = "012345678901234567890", // 21 کاراکتر
                Address = "تهران"
            };

            // Act
            var result = _contactService.CreateContact(dto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("شماره تلفن نباید بیشتر از 20 کاراکتر باشد", result.Message);
        } 
        #endregion
        #region Update
        // UPDATE TESTS - تست‌های ویرایش
        [TestMethod]
        public void UpdateContact_WithValidData_ShouldReturnSuccess()
        {
            // Arrange
            _contactService.CreateContact(new ContactDto { Name = "علی", Phone = "123" });
            var contacts = _contactService.GetAllContacts();
            var contactId = contacts[0].Id;

            var updatedDto = new ContactDto
            {
                Id = contactId,
                Name = "علی احمدی",
                Phone = "09121234567",
                Address = "تهران"
            };

            // Act
            var result = _contactService.UpdateContact(updatedDto);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("مخاطب با موفقیت ویرایش شد", result.Message);

            var updated = _contactService.GetAllContacts()[0];
            Assert.AreEqual("علی احمدی", updated.Name);
            Assert.AreEqual("09121234567", updated.Phone);
        }

        [TestMethod]
        public void UpdateContact_WithInvalidId_ShouldReturnFailure()
        {
            // Arrange
            var dto = new ContactDto
            {
                Id = 0,
                Name = "علی",
                Phone = "123"
            };

            // Act
            var result = _contactService.UpdateContact(dto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("شناسه مخاطب نامعتبر است", result.Message);
        }

        [TestMethod]
        public void UpdateContact_WithNonExistingId_ShouldReturnFailure()
        {
            // Arrange
            var dto = new ContactDto
            {
                Id = 999,
                Name = "علی",
                Phone = "123"
            };

            // Act
            var result = _contactService.UpdateContact(dto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("مخاطب یافت نشد", result.Message);
        }

        [TestMethod]
        public void UpdateContact_WithEmptyName_ShouldReturnFailure()
        {
            // Arrange
            _contactService.CreateContact(new ContactDto { Name = "علی", Phone = "123" });
            var contactId = _contactService.GetAllContacts()[0].Id;

            var dto = new ContactDto
            {
                Id = contactId,
                Name = "",
                Phone = "123"
            };

            // Act
            var result = _contactService.UpdateContact(dto);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("لطفا نام را وارد کنید", result.Message);
        }
        #endregion
        #region Delete
        // DELETE TESTS - تست‌های حذف
        [TestMethod]
        public void DeleteContact_WithValidId_ShouldReturnSuccess()
        {
            // Arrange
            _contactService.CreateContact(new ContactDto { Name = "علی", Phone = "123" });
            var contactId = _contactService.GetAllContacts()[0].Id;

            // Act
            var result = _contactService.DeleteContact(contactId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("مخاطب با موفقیت حذف شد", result.Message);

            var contacts = _contactService.GetAllContacts();
            Assert.AreEqual(0, contacts.Count);
        }

        [TestMethod]
        public void DeleteContact_WithInvalidId_ShouldReturnFailure()
        {
            // Act
            var result = _contactService.DeleteContact(0);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("شناسه مخاطب نامعتبر است", result.Message);
        }

        [TestMethod]
        public void DeleteContact_WithNonExistingId_ShouldReturnFailure()
        {
            // Act
            var result = _contactService.DeleteContact(999);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("مخاطب یافت نشد", result.Message);
        }
        #endregion
        #region Read
        // READ TESTS - تست‌های خواندن
        [TestMethod]
        public void GetAllContacts_WithNoData_ShouldReturnEmptyList()
        {
            // Act
            var contacts = _contactService.GetAllContacts();

            // Assert
            Assert.IsNotNull(contacts);
            Assert.AreEqual(0, contacts.Count);
        }

        [TestMethod]
        public void GetAllContacts_WithMultipleContacts_ShouldReturnAllContacts()
        {
            // Arrange
            _contactService.CreateContact(new ContactDto { Name = "علی", Phone = "123" });
            _contactService.CreateContact(new ContactDto { Name = "رضا", Phone = "456" });
            _contactService.CreateContact(new ContactDto { Name = "سارا", Phone = "789" });

            // Act
            var contacts = _contactService.GetAllContacts();

            // Assert
            Assert.AreEqual(3, contacts.Count);
            Assert.AreEqual("علی", contacts[0].Name);
            Assert.AreEqual("رضا", contacts[1].Name);
            Assert.AreEqual("سارا", contacts[2].Name);
        }
        #endregion
        #region Integration
        // Integration Test - تست‌های یکپارچه
        [TestMethod]
        public void CompleteWorkflow_CreateUpdateDelete_ShouldWorkCorrectly()
        {
            // 1. Create
            var createDto = new ContactDto { Name = "علی", Phone = "123", Address = "تهران" };
            var createResult = _contactService.CreateContact(createDto);
            Assert.IsTrue(createResult.IsSuccess);

            // 2. Read
            var contacts = _contactService.GetAllContacts();
            Assert.AreEqual(1, contacts.Count);
            var contactId = contacts[0].Id;

            // 3. Update
            var updateDto = new ContactDto
            {
                Id = contactId,
                Name = "علی احمدی",
                Phone = "09121234567",
                Address = "تهران - ونک"
            };
            var updateResult = _contactService.UpdateContact(updateDto);
            Assert.IsTrue(updateResult.IsSuccess);

            // 4. Verify Update
            var updatedContacts = _contactService.GetAllContacts();
            Assert.AreEqual("علی احمدی", updatedContacts[0].Name);
            Assert.AreEqual("09121234567", updatedContacts[0].Phone);

            // 5. Delete
            var deleteResult = _contactService.DeleteContact(contactId);
            Assert.IsTrue(deleteResult.IsSuccess);

            // 6. Verify Delete
            var finalContacts = _contactService.GetAllContacts();
            Assert.AreEqual(0, finalContacts.Count);
        }

        [TestMethod]
        public void MultipleContacts_CreateAndDelete_ShouldMaintainCorrectCount()
        {
            // Create 5 contacts
            for (int i = 1; i <= 5; i++)
            {
                _contactService.CreateContact(new ContactDto
                {
                    Name = $"مخاطب {i}",
                    Phone = $"09120000000{i}"
                });
            }

            Assert.AreEqual(5, _contactService.GetAllContacts().Count);

            // Delete 2 contacts
            var contacts = _contactService.GetAllContacts();
            _contactService.DeleteContact(contacts[0].Id);
            _contactService.DeleteContact(contacts[2].Id);

            Assert.AreEqual(3, _contactService.GetAllContacts().Count);
        } 
        #endregion
    }
}
