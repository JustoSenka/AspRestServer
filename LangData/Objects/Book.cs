using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LangData.Objects
{
    public class Book
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public IEnumerable<Word> Words { get; set; }
    }
}
