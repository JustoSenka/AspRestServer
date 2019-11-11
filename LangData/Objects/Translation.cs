using System.Diagnostics;

namespace LangData.Objects
{
    [DebuggerDisplay("Translation: Word: {Word.Text} Def: {Definition.Text}")]
    public class Translation
    {
        public Translation() { }
        public Translation(Word word, Definition definition)
        {
            Word = word;
            Definition = definition;
        }

        public int ID { get; set; }

        public Word Word { get; set; }
        public Definition Definition { get; set; }
    }
}
