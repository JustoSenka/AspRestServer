using Langs.Data.Objects;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Langs.Utilities
{
    public class LanguageEqualityComparer : IEqualityComparer<Word>
    {
        public bool Equals([AllowNull] Word x, [AllowNull] Word y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null)
                return false;
            else if (y == null)
                return false;
            else
                return x.LanguageID == y.LanguageID;
        }

        public int GetHashCode([DisallowNull] Word obj)
        {
            return obj.LanguageID.GetHashCode();
        }
    }
}
