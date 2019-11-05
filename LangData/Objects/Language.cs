using System.ComponentModel.DataAnnotations;

namespace LangData.Objects
{
    public class Language
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
