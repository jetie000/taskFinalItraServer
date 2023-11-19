namespace finalTaskItra.Models
{
    public class UserRegisterInfo
    {
        public string email { get; set; } = null!;
        public string saltedPassword { get; set; } = null!;
        public string fullName { get; set; } = null!;
    }
}
