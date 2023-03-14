namespace Auth.Common.Models.Request
{
    public class OtpRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? SmsToken { get; set; }
    }
}
