using LangData.Objects.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace LangData.Objects
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

        public Word Word { get; set; }
        public Definition Definition { get; set; }

        string IDisplayText.DisplayText => Word.Text + " - " + Definition.Text;
    }
}
