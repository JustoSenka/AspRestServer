using System;
using System.Collections.Generic;

namespace Langs.Models.Shared
{
    public class SearcheableDropdownModel
    {
        public bool IsSubmit { get; set; }

        public string Text { get; set; }
        public string AdditionalClasses { get; set; }
        public string AdditionalDropdownClasses { get; set; }

        /// <summary>
        /// Func to return custom route values for elements. 
        /// Passes current row id which is set in ListElement.ID
        /// Not supported if IsSubmit == true, in that case, a button instead of link will be generated.
        /// </summary>
        public Func<int, object> AdditionalRoutingValues = null;

        public IEnumerable<ListElement> Elements { get; set; }

        public string AspController { get; set; }
        public string AspAction { get; set; }
    }
}
