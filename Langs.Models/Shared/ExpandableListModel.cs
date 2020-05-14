using System.Collections.Generic;

namespace Langs.Models.Shared
{
    public class ExpandableListModel
    {
        public string Title { get; set; }
        public bool Expanded { get; set; }
        public IEnumerable<ListElement> Elements { get; set; }
    }
}
