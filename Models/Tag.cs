﻿namespace finalTaskItra.Models
{
    public class Tag
    {
        public int id { get; set; }
        public string tag { get; set; } = null!;
        public virtual Item? item { get; set; }
    }
}