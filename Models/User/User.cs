namespace finalTaskItra.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; } = null!;
        public string saltedPassword { get; set; } = null!;
        public string fullName { get; set; } = null!;
        public int role { get; set; }
        public bool access { get; set; }
        public string accessToken { get; set; } = null!;
        public DateTime joinDate { get; set; }
        public DateTime loginDate { get; set; }
        public bool isOnline { get; set; }
        public ICollection<Collection>? collections { get; set; }
    }
}
