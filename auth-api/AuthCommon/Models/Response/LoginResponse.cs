namespace Auth.Common.Models.Response
{
    public class Data
    {
        public Token token { get; set; }
        public UserData userData { get; set; }
    }

    public class LoginResponse
    {
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public Data data { get; set; }
    }

    public class Token
    {
        public string accessToken { get; set; }
        public int expires { get; set; }
        public string refreshToken { get; set; }
        public string pinToken { get; set; }
    }

    public class UserData
    {
        public string userName { get; set; }
        public string customerNo { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string customerIdentity { get; set; }
    }
}
