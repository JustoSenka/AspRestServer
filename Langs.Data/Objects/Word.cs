using Langs.Data.Objects.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;

namespace Langs.Data.Objects
{
    [DebuggerDisplay("Word: Lang: {Language.Name} Text: {Text}")]
    public class Word : BaseObject, IHaveID, IDisplayText, IListableElement
    {
        public Word() { }
        public Word(string text, Language language, string article = "")
        {
            Text = text;
            Language = language;
            Article = article;
        }

        [Required]
        public virtual string Text { get; set; }

        [Required]
        public virtual Language Language { get; set; }

        public virtual string Article { get; set; }
        public virtual string Pronunciation { get; set; }
        public virtual string AlternateSpelling { get; set; }

        public virtual ICollection<Translation> Translations { get; set; }

        public virtual ICollection<BookWord> BookWordCollection { get; set; }

        // ----------
        [NotMapped]
        public IEnumerable<Book> Books => BookWordCollection.Select(c => c.Book);

        string IDisplayText.DisplayText => Text;
    }
}
