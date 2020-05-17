using System.Diagnostics;

namespace Langs.Data.Objects
{
    [DebuggerDisplay("BookWord: Book: {Book.Name} MasterWordId: {MasterWordId}")]
    public class BookWord
    {
        public virtual int BookId { get; set; }
        public virtual Book Book { get; set; }

        public virtual int MasterWordId { get; set; }
        public virtual MasterWord MasterWord { get; set; }
    }
}
