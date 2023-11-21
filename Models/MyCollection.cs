namespace finalTaskItra.Models
{
    public class MyCollection
    {
        public int id { get; set; }
        public string title { get; set; } = null!;
        public string? description { get; set; }
        public string theme { get; set; } = null!;
        public string? photoPath { get; set; }
        public DateTime creationDate { get; set; }
        public ICollection<Item> items { get; set; } = new List<Item>();
        public ICollection<CollectionFields> collectionFields { get; set; } = new List<CollectionFields>();
    }
}
