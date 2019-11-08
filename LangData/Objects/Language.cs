using System.ComponentModel.DataAnnotations;

namespace LangData.Objects
{
    public class Language
    {
        public Language() { }
        public Language(string name)
        {
            Name = name;
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
