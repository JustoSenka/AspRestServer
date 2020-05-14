using System.Collections.Generic;

namespace Langs.Models.Shared
{
    public class TableModel
    {
        public string Title { get; set; }
        public IEnumerable<string> LeftHeaders { get; set; }
        public IEnumerable<string> TopHeader { get; set; }
        public IEnumerable<IEnumerable<string>> Table { get; set; }
    }
}
