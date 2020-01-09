using System.Collections.Generic;

namespace LanguageLearner.Models.Shared
{
    public class ExpandableListModel
    {
        public string Title { get; set; }
        public IEnumerable<ListElement> Elements { get; set; }
    }
}
