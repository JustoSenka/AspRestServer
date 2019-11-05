using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageLearnerData.Objects
{
    public class Book
    {
        public int ID { get; set; }

        [Required]
        public Language LanguageFrom { get; set; }
        [Required]
        public Language LanguageTo { get; set; }

        public IEnumerable<Translation> Words { get; set; }
    }
}
