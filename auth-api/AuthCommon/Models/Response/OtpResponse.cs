namespace Auth.Common.Models.Response
{
    public class DataOtp
    {
        public TokenOtp Token { get; set; }
        public UserDataOtp UserData { get; set; }
    }

    public class OtpResponse
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public DataOtp Data { get; set; }
    }

    public class TokenOtp
    {
        public string AccessToken { get; set; }
        public int Expires { get; set; }
        public string RefreshToken { get; set; }
        public string PinToken { get; set; }
    }

    public class UserDataOtp
    {
        public string UserName { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerIdentity { get; set; }
    }
}
