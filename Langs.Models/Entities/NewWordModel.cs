using Langs.Data.Objects;
using Langs.Models.Shared;

namespace Langs.Models
{
    public class NewWordModel : EditEntityModel, IAlertMessageModel
    {
        public int TranslationForID { get; set; }
    }
}
