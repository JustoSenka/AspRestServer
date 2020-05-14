using System.Collections.Generic;

namespace Langs.Models.Shared
{
    public class SearcheableDropdownModel
    {
        public bool IsSubmit { get; set; }

        public string Text { get; set; }
        public string AdditionalClasses { get; set; }
        public string AdditionalDropdownClasses { get; set; }

        public IEnumerable<ListElement> Elements { get; set; }

        public string AspController { get; set; }
        public string AspAction { get; set; }
    }
}
