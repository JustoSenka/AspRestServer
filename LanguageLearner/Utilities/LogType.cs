namespace LanguageLearner.Utilities
{
    public enum LogType
    {
        None, Error, Info, Success, Warning
    }

    public static class LogTypeExtensions
    {
        public static string AlertClass(this LogType LogType)
        {
            switch (LogType)
            {
                case LogType.Success:
                    return "alert alert-success";
                case LogType.Info:
                    return "alert alert-info";
                case LogType.Warning:
                    return "alert alert-warning";
                case LogType.Error:
                    return "alert alert-danger";
                case LogType.None:
                    return "alert alert-primary";
                default:
                    return "alert";
            }
        }
    }
}
