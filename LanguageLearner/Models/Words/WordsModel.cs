using LangData.Objects;
using LanguageLearner.Models.Shared;

namespace LanguageLearner.Models.Words
{
    public class WordsModel : LanguagePickerModel
    {
        public Word[] Words { get; set; }
        public Definition[] Definitions { get; set; }
    }
}
