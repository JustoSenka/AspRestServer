using System.Collections.Generic;

namespace Langs.Models.Shared
{
    public class TableLinkModel : TableModel
    {
        public IEnumerable<IEnumerable<TableElement>> TableElements { get; set; }
        public IDictionary<int, (string AspController, string AspAction)> ColumnLinks = new Dictionary<int, (string, string)>();

        public string AspController { get; set; }
        public string AspAction { get; set; }


        public string GetController(int columnIndex) => HasLinkActionForColumn(columnIndex) ? ColumnLinks[columnIndex].AspController : AspController;
        public string GetAction(int columnIndex) => HasLinkActionForColumn(columnIndex) ? ColumnLinks[columnIndex].AspAction : AspAction;

        public bool HasLinkAction(int columnIndex) => HasGlobalLinkAction || HasLinkActionForColumn(columnIndex);
        private bool HasGlobalLinkAction => !string.IsNullOrEmpty(AspAction);
        private bool HasLinkActionForColumn(int columnIndex) => ColumnLinks.ContainsKey(columnIndex);
    }
}
