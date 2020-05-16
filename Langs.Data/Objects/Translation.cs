using Langs.Data.Objects.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Langs.Data.Objects
{
    [DebuggerDisplay("Translation: Word: {Word.Text} Def: {Definition.Text}")]
    public class Translation : BaseObject, IHaveID, IDisplayText, IListableElement
    {
        public Translation() { }
        public Translation(Word word, Definition definition)
        {
            Word = word;
            Definition = definition;
        }

        public virtual Word Word { get; set; }
        public virtual Definition Definition { get; set; }

        // ----------
        string IDisplayText.DisplayText => Word.Text + " - " + Definition.Text;
    }
}
