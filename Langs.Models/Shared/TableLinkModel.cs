using System;
using System.Collections.Generic;

namespace Langs.Models.Shared
{
    public class TableLinkModel : TableModel
    {
        public IEnumerable<IEnumerable<TableElement>> TableElements { get; set; }
        public IDictionary<int, (string AspController, string AspAction)> ColumnLinks = new Dictionary<int, (string, string)>();
        public IDictionary<int, string> AdditionalStyleClass = new Dictionary<int, string>();

        /// <summary>
        /// Dictionary from column indices to function which returns new route values 
        /// and accepts link id which is set in table element and passes it as default id = TableElement.ID
        /// </summary>
        public IDictionary<int, Func<int, object>> AdditionalRoutingValues = new Dictionary<int, Func<int, object>>();

        public string AspController { get; set; }
        public string AspAction { get; set; }

        public string GetController(int columnIndex) => HasLinkActionForColumn(columnIndex) ? ColumnLinks[columnIndex].AspController : AspController;
        public string GetAction(int columnIndex) => HasLinkActionForColumn(columnIndex) ? ColumnLinks[columnIndex].AspAction : AspAction;

        public bool HasLinkAction(int columnIndex) => HasGlobalLinkAction || HasLinkActionForColumn(columnIndex);
        private bool HasGlobalLinkAction => !string.IsNullOrEmpty(AspAction);
        private bool HasLinkActionForColumn(int columnIndex) => ColumnLinks.ContainsKey(columnIndex);
        public bool HasAdditionalStyleClass(int columnIndex) => AdditionalStyleClass.ContainsKey(columnIndex);
        public bool HasAdditionalRoutingValues(int columnIndex) => AdditionalRoutingValues.ContainsKey(columnIndex);
    }
}
