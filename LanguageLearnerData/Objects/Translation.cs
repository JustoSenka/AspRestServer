using System.ComponentModel.DataAnnotations;

namespace LanguageLearnerData.Objects
{
    public class Translation
    {
        public int ID { get; set; }

        [Required]
        public Word Word { get; set; }
        [Required]
        public Definition Definition { get; set; }
    }
}
