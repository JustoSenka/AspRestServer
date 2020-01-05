using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LangData.Objects
{
    [DebuggerDisplay("Language: {Name}")]
    public class Language : IHaveID
    {
        public Language() { }
        public Language(string name)
        {
            Name = name;
        }

        [Required]
        public string Name { get; set; }
    }
}
