namespace PhoneBook.BL.Utilities
{
    // نتیجه عملیات - برای مدیریت خطاها و موفقیت‌ها
    // برخی کد ها در بیشتر پروژه یکسان هستند
    public class TlsResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public static TlsResult Success(string message = "عملیات با موفقیت انجام شد")
        {
            return new TlsResult { IsSuccess = true, Message = message };
        }
        public static TlsResult Failure(string message)
        {
            return new TlsResult { IsSuccess = false, Message = message };
        }
    }
}