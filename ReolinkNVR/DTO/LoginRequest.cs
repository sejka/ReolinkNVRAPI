namespace ReolinkNVR.DTO
{
    public class LoginRequest
    {
        public string cmd { get; set; } = "Login";
        public LoginParam param { get; set; }
    }

    public class LoginParam
    {
        public User User { get; set; }
    }

    public class User
    {
        public string Version { get; set; } = "0";
        public string userName { get; set; }
        public string password { get; set; }
    }
}