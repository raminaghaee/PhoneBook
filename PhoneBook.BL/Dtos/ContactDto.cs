namespace PhoneBook.BL
{
    // DTO - Data Transfer Object برای انتقال داده بین لایه‌ها
    public class ContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}