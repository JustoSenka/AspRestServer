using System;
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

        /*
        public override int GetHashCode()
        {
            return BookId.GetHashCode() << 1 ^ MasterWordId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj))
                return true;

            var other = obj as BookWord;
            if (Object.ReferenceEquals(other, null))
                return false;

            return GetHashCode() == obj.GetHashCode();
        }

        public static bool operator ==(BookWord a, BookWord b) => a.Equals(b);
        public static bool operator !=(BookWord a, BookWord b) => !a.Equals(b);
        */
    }
}
