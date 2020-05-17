using Langs.Data.Objects;

namespace Langs.Models
{
    public class EntityModel
    {
        public Word Word { get; set; }
        public Language PreferredDefaultLanguage { get; set; }
    }
}
