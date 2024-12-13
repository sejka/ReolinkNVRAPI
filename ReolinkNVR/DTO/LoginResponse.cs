namespace ReolinkNVR.DTO
{
    public class LoginResponse
    {
        public string cmd { get; set; }
        public int code { get; set; }
        public LoginValue value { get; set; }
    }

    public class LoginValue
    {
        public Token Token { get; set; }
    }

    public class Token
    {
        public int leaseTime { get; set; }
        public string name { get; set; }
    }
}