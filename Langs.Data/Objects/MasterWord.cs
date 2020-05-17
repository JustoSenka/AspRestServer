using Langs.Data.Objects.Base;
using System.Collections.Generic;
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
        /// Don't use me. I'm here because EF CORE does not support many to many relations. Use provided interfaces instead.
        /// </summary>
        public virtual ICollection<BookWord> _BookWordCollection { get; set; } = new List<BookWord>();

        // API -------------

        public Word this[int langID] => Words.FirstOrDefault(w => w.Language.ID == langID);
        public Word this[Language lang] => Words.FirstOrDefault(w => w.Language.ID == lang.ID);


    }
}
