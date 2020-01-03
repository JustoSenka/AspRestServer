using LangData.Objects;

namespace LanguageLearner.Models.Words
{
    public class WordsModel
    {
        public Language[] AvailableLanguages { get; set; }

        public Language From { get; set; }
        public Language To { get; set; }

        public Word[] Words { get; set; }
        public Definition[] Definitions { get; set; }
    }
}
