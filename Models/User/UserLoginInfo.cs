namespace finalTaskItra.Models
{
    public class UserLoginInfo
    {
        public string email { get; set; } = null!;
        public string saltedPassword { get; set; } = null!;
    }
}
