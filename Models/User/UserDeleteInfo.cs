namespace finalTaskItra.Models
{
    public class UserDeleteInfo
    {
        public string email { get; set; } = null!;
        public string saltedPassword { get; set; } = null!;
        public string accessToken { get; set; } = null!;
    }
}
