using Langs.Data.Objects.Base;
using System.Collections.Generic;
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
        public Book(string name, string description)
        {
            Name = name;
            Description = description;
        }

        [Required, MaxLength(k_WordLength)]
        public virtual string Name { get; set; }

        [MaxLength(200)]
        public virtual string Description { get; set; }

        /// <summary>
        /// Don't use me. I'm here because EF CORE does not support many to many relations. Use provided interfaces instead.
        /// </summary>
        public virtual ICollection<BookWord> _BookWordCollection { get; set; } = new List<BookWord>();

        // API -----------

        public void AddWord(MasterWord word)
        {
            _BookWordCollection.Add(new BookWord() { Book = this, BookId = ID, MasterWord = word, MasterWordId = word.ID });
        }

        public void RemoveWord(MasterWord word)
        {
            var first = _BookWordCollection.First(b => b.MasterWordId == word.ID);
            _BookWordCollection.Remove(first);
        }

        [NotMapped]
        public IEnumerable<MasterWord> Words
        {
            get => _BookWordCollection?.Select(c => c.MasterWord);
            set => _BookWordCollection = value.Select(w => new BookWord() { Book = this, BookId = ID, MasterWord = w, MasterWordId = w.ID }).ToList();
        }

        string IDisplayText.DisplayText => Name;
    }
}
