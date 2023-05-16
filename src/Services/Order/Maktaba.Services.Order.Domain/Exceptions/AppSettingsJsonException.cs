namespace Maktaba.Services.Order.Domain;

public sealed class AppSettingsJsonException : Exception
{
    public AppSettingsJsonException(string sectionName) : base($"{sectionName} is null")
    {

    }
}
