﻿namespace finalTaskItra.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string comment { get; set; } = null!;
        public int userId { get; set; }
        public DateTime creationDate { get; set; }
    }
}