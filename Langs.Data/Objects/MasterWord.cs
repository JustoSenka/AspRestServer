using Langs.Data.Objects.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;

namespace Langs.Data.Objects
{
    [DebuggerDisplay("ID: {ID}, Words: {Words.Count}")]
    public class MasterWord : BaseObject, IHaveID
    {
        public MasterWord() { }

        public virtual HashSet<Word> Words { get; set; } = new HashSet<Word>();

        /// <summary>
        /// Don't use me. I'm here because EF CORE does not support many to many relations. Use provided API instead.
        /// </summary>
        public virtual HashSet<BookWord> _BookWordCollection { get; set; } = new HashSet<BookWord>();

        // API -------------

        [NotMapped]
        public IEnumerable<Book> Books => _BookWordCollection.Select(e => e.Book);

        [NotMapped]
        public IEnumerable<int> BookIDs => _BookWordCollection.Select(e => e.BookId);

        public void AddBook(Book book)
        {
            if (_BookWordCollection.Any(bw => bw.BookId == book.ID || bw.Book == book))
                return;

            _BookWordCollection.Add(new BookWord() { Book = book, BookId = book.ID, MasterWord = this, MasterWordId = this.ID });
        }

        public Word this[int langID] => Words.FirstOrDefault(w => w.LanguageID == langID);
        public Word this[Language lang] => Words.FirstOrDefault(w => w.LanguageID == lang.ID);


    }
}
