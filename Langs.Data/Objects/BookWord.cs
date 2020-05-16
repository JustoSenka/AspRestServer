using Langs.Data.Objects.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Langs.Data.Objects
{
    [DebuggerDisplay("BookWord: Book: {Book.Name} Word: {Word.Text}")]
    public class BookWord
    {
        public virtual int BookId { get; set; }
        public virtual Book Book { get; set; }

        public virtual int WordId { get; set; }
        public virtual Word Word { get; set; }
    }
}
