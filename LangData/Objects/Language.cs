using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LangData.Objects
{
    [DebuggerDisplay("Language: {Name}")]
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
