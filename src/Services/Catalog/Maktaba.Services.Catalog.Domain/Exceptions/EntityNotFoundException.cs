namespace Maktaba.Services.Catalog.Domain;

public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string name) : base($"{name} is not provided") { }
}