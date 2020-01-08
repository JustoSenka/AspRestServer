using LangData.Objects.Base;
using System.Collections.Generic;

namespace LanguageLearner.Models.Shared
{
    public class ExpandableListModel
    {
        public string Title { get; set; }
        public IEnumerable<string> Elements { get; set; }
    }
}
