using Langs.Data.Objects.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Langs.Data.Objects
{
    [DebuggerDisplay("Book: {Name} Count: {WordCount}")]
    public class Account : BaseObject, IHaveID, IDisplayText, IListableElement
    {
        public Account() { }

        [Required, MaxLength(k_WordLength)]
        public virtual string Name { get; set; }


        public virtual int? NativeLanguageID { get; set; }
        public virtual int? LearningLanguageID { get; set; }
        public virtual int? AdditionalLanguageID { get; set; }


        [ForeignKey("NativeLanguageID")]
        public virtual Language NativeLanguage { get; set; }

        [ForeignKey("LearningLanguageID")]
        public virtual Language LearningLanguage { get; set; }
        
        [ForeignKey("AdditionalLanguageID")]
        public virtual Language AdditionalLanguage { get; set; }


        string IDisplayText.DisplayText => Name;
    }
}
