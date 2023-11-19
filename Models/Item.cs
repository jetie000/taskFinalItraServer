namespace finalTaskItra.Models
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public ICollection<Tag>? tags { get; set; }
        public DateTime creationDate { get; set; }
        public ICollection<ItemFields>? fields { get; set; }
        public ICollection<Comment>? comments { get; set; }
        public ICollection<Reaction>? likes { get; set; }
    }
}
