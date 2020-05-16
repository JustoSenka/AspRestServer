using Langs.Data.Objects.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Langs.Data.Objects
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
        public virtual string Name { get; set; }

        // ----------------
        string IDisplayText.DisplayText => Name;
    }
}
