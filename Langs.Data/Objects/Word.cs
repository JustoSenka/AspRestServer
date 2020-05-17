using Langs.Data.Objects.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;

namespace Langs.Data.Objects
{
    [DebuggerDisplay("Word: Lang: {Language.Name} Text: {Text}")]
    public class Word : BaseObject, IHaveID, IDisplayText, IListableElement
    {
        public Word() { }
        public Word(MasterWord masterWord, string text, Language language, string article = "")
        {
            MasterWord = masterWord;
            Text = text;
            Language = language;
            Article = article;

            masterWord.Words.Add(this);
        }

        [Required, MaxLength(k_WordLength)] // Top 10 longest words in english average length is below 30, I believe 40 is safe length
        public virtual string Text { get; set; }

        [Required]
        public virtual Language Language { get; set; }

        [MaxLength(10)]
        public virtual string Article { get; set; }

        [MaxLength(k_WordLength)]
        public virtual string Pronunciation { get; set; }

        [MaxLength(k_WordLength)]
        public virtual string AlternateSpelling { get; set; }

        public virtual Definition Definition { get; set; }

        public virtual MasterWord MasterWord { get; set; }
        public virtual ICollection<Explanation> Explanations { get; set; }

        // API ----------

        [NotMapped]
        public string Description => Definition?.Description;

        public void AddTranslation(Word word) => MasterWord.Words.Add(word);
        public void RemoveTranslation(Word word) => MasterWord.Words.Remove(word);

        [NotMapped]
        public IEnumerable<Word> Translations
        {
            get => MasterWord?.Words?.Where(w => w.ID != ID);
            set
            {
                var master = MasterWord;

                // Nullify MasterWord from all words previously attached to them
                foreach (var w in master.Words)
                    w.MasterWord = null;

                master.Words = value.ToHashSet();

                // Assign master word to all added words and add self back
                master.Words.Add(this);
                foreach (var w in master.Words)
                    w.MasterWord = master;
            }
        }


        public Word this[int langID] => MasterWord?[langID];
        public Word this[Language lang] => MasterWord?[lang];

        string IDisplayText.DisplayText => Text;
    }
}
