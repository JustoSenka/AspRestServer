using Langs.Data.Objects.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;

namespace Langs.Data.Objects
{
    [DebuggerDisplay("Book: {Name} Count: {WordCount}")]
    public class Book : BaseObject, IHaveID, IDisplayText, IListableElement
    {
        public Book() { }
        public Book(string name, string description, List<Word> words = null)
        {
            Name = name;
            Description = description;

            if (words == null)
                BookWordCollection = new Collection<BookWord>();
            else
                BookWordCollection = words.Select(w => new BookWord() { Book = this, Word = w }).ToList();
        }

        [Required]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual ICollection<BookWord> BookWordCollection { get; set; }


        // -----------

        [NotMapped]
        public IEnumerable<Word> Words => BookWordCollection?.Select(c => c.Word);


        string IDisplayText.DisplayText => Name;
    }
}
