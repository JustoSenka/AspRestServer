using LangData.Objects.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LangData.Objects
{
    [DebuggerDisplay("Language: {Name}")]
    public class Language : BaseObject, IHaveID, IDisplayText, IListableElement
    {
        public Language() { }
        public Language(string name)
        {
            Name = name;
        }

        [Required]
        public string Name { get; set; }

        string IDisplayText.DisplayText => Name;
    }
}
