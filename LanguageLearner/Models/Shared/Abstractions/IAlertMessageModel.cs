
namespace LanguageLearner.Models.Shared
{
    public interface IAlertMessageModel
    {
        AlertType AlertType { get; set; }
        string AlertMessage { get; set; }
    }
}
