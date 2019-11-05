using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageLearnerData.Objects
{
    public class Book
    {
        public int ID { get; set; }

        public Language LanguageFrom { get; set; }
        public Language LanguageTo { get; set; }

        public IEnumerable<Translation> Words { get; set; }
    }
}
