using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LangData.Objects
{
    [DebuggerDisplay("Book: {Name} Count: {Words.Count}")]
    public class Book
    {
        public Book() { }
        public Book(string name, List<Word> words = null)
        {
            Name = name;
            Words = words;
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public List<Word> Words { get; set; }
    }
}
