namespace Maktaba.Services.Orders.Domain;

public sealed class AppSettingsJsonException : Exception
{
    public AppSettingsJsonException(string sectionName) : base($"{sectionName} is null")
    {

    }
}
