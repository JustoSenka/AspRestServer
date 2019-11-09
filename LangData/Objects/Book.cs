using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LangData.Objects
{
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
