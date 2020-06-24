using Langs.Models.Shared;

namespace Langs.Models.Words
{
    public class AddWordsModel : LanguagePickerModel, IAlertMessageModel
    {
        public AlertType AlertType { get; set; }
        public string AlertMessage { get; set; }

        public bool TreatFirstWordAsArticle { get; set; }

        public string SubmitButtonName { get; set; }

        public int SelectedBookID { get; set; }
        public (int ID, string Name, string LanguageName)[] Books { get; set; }

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
