using LangData.Objects;

namespace LanguageLearner.Models.Languages
{
    public class LanguagesModel
    {
        public Language[] AvailableLanguages { get; set; }

        public bool IsError { get; private set; }

        private string m_Error;
        public string Error
        {
            get
            {
                return m_Error;
            }
            set
            {
                m_Error = value;
                IsError = !string.IsNullOrEmpty(m_Error);
            }
        }
    }
}
