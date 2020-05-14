public enum AlertType
{
    None, Error, Info, Success, Warning
}

public static class AlertTypeExtensions
{
    public static string AlertClass(this AlertType LogType)
    {
        switch (LogType)
        {
            case AlertType.Success:
                return "alert alert-success";
            case AlertType.Info:
                return "alert alert-info";
            case AlertType.Warning:
                return "alert alert-warning";
            case AlertType.Error:
                return "alert alert-danger";
            case AlertType.None:
                return "alert alert-primary";
            default:
                return "alert";
        }
    }
}
