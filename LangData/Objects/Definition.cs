using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LangData.Objects
{
    public class Definition
    {
        public int ID { get; set; }

        [Required]
        public string Text { get; set; }

        public string Examples { get; set; }

        public Language Language { get; set; }
        public IEnumerable<Translation> Translations { get; set; }
    }
}
