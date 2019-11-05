using System.ComponentModel.DataAnnotations;

namespace LanguageLearnerData.Objects
{
    public class Language
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
