namespace finalTaskItra.Models
{
    public class Reaction
    {
        public int id { get; set; }
        public int userId { get; set; }
        public bool isLike { get; set; }
        public DateTime creationDate { get; set; }
    }
}
