﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LangData.Objects
{
    [DebuggerDisplay("Book: {Name} Count: {Words.Count}")]
    public class Book
    {
        public Book() { }
        public Book(string name, string description, List <Word> words = null)
        {
            Name = name;
            Words = words;
            Description = description;

            if (Words == null)
                Words = new List<Word>();

            WordCount = Words.Count;
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public int WordCount { get; set; }
        public string Description{ get; set; }

        public List<Word> Words { get; set; }
    }
}
