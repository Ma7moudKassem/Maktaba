namespace Maktaba.Domain;

public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string name) : base($"{name} is not provided") { }
}