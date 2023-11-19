namespace finalTaskItra.Models
{
    public class UserChangeInfo
    {
        public string email { get; set; } = null!;
        public string saltedOldPassword { get; set; } = null!;
        public string saltedNewPassword { get; set; } = null!;
        public string fullName { get; set; } = null!;
        public string accessToken { get; set; } = null!;
    }
}
