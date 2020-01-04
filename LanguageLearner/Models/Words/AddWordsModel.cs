using LanguageLearner.Models.Shared;
using LanguageLearner.Utilities;

namespace LanguageLearner.Models.Words
{
    public class AddWordsModel : LanguagePickerModel
    {
        public LogType LogType { get; set; }
        public string LogMessage { get; set; }

        public bool TreatFirstWordAsArticle { get; set; }

        public string SubmitButtonName { get; set; }

        // add1
        public string SingleWordText { get; set; }
        public string SingleDefinitionText { get; set; }

        // add2
        public string WordsCombinedArea { get; set; }

        // add3
        public string WordsArea3 { get; set; }
        public string DefinitionsArea3 { get; set; }

        // add4
        public string WordsArea4 { get; set; }
        public string DefinitionsArea4 { get; set; }
        public string DescriptionsArea4 { get; set; }
    }
}
