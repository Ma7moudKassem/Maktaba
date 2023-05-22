namespace Services.Shared;

public static class ExceptionExtensions
{
    public static string GetExceptionErrorSimplified(this Exception exception)
    {
        string exceptionMessege =
            GetNewLineSeparator() +
            $"Exception Source: {exception.Source}\n\n\n" +
            $"Exception Stack Trace: {exception?.StackTrace}\n\n\n" +
            $"Exception Target Site Name: {exception?.TargetSite?.Name}\n\n\n" +
            $"Exception Message: {exception?.Message}\n\n\n" +
            $"Inner Exception Message: {exception?.InnerException?.Message}" +
            GetNewLineSeparator();

        return exceptionMessege;
    }

    public static string GetNewLineSeparator()
        => "\n\n************************************************************************************************\n\n";
}