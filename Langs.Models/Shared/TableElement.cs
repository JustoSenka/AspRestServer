using System.Linq;

namespace Langs.Models.Shared
{
    public struct TableElement
    {
        public int ID { get; private set; }
        public string Value { get; private set; }
        public bool HasLinkAction { get; private set; }

        public static TableElement Text(string str)
        {
            return new TableElement { Value = str, HasLinkAction = false };
        }

        public static TableElement Link(int id, string str)
        {
            return new TableElement { Value = str, ID = id, HasLinkAction = true };
        }

        public static TableElement[] TextMany(params string[] str)
        {
            return str.Select(s => new TableElement { Value = s, HasLinkAction = false }).ToArray();
        }

        public static TableElement[] LinkMany(int id, params string[] str)
        {
            return str.Select(s => new TableElement { Value = s, ID = id, HasLinkAction = true }).ToArray();
        }
    }
}
