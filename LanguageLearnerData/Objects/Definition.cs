using System.ComponentModel.DataAnnotations;

namespace LanguageLearnerData.Objects
{
    public class Definition
    {
        public int ID { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
