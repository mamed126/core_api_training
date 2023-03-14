namespace Auth.Common.Models.Request
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? SmsToken { get; set; }
    }
}
