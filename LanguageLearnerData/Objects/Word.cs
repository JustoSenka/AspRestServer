using System.ComponentModel.DataAnnotations;

namespace LanguageLearnerData.Objects
{
    public class Word
    {
        public int ID { get; set; }

        [Required]
        public Language Language { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
