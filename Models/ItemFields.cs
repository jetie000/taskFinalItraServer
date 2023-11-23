namespace finalTaskItra.Models
{
    public class ItemFields
    {
        public int id { get; set; }
        public string fieldName { get; set; } = null!;
        public string? stringFieldValue { get; set; } = null!;
        public double? doubleFieldValue { get; set; }
        public DateTime? dateFieldValue { get; set; }
        public bool? boolFieldValue { get; set; }
        public virtual Item? item { get; set; }
    }
}
