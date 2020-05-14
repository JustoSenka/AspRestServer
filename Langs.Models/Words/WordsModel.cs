using Langs.Data.Objects;
using Langs.Models.Shared;

namespace Langs.Models.Words
{
    public class WordsModel : LanguagePickerModel
    {
        public Word[] Words { get; set; }
        public Definition[] Definitions { get; set; }
    }
}
