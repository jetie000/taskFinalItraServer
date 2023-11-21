namespace finalTaskItra.Models
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public ICollection<Tag> tags { get; set; } = new List<Tag>();
        public DateTime creationDate { get; set; }
        public ICollection<ItemFields> fields { get; set; } = new List<ItemFields>();
        public ICollection<Comment> comments { get; set; } = new List<Comment>();
        public ICollection<Reaction> likes { get; set; } = new List<Reaction>();
    }
}
