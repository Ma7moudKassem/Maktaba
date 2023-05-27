namespace Maktaba.Services.Orders.Domain.Entities;

public class Address : BaseEntity
{
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Country { get; set; } = null!;

    public Address() { }

    public Address(string city,
        string street,
        string country)
    {
        City = city;
        Street = street;
        Country = country;
    }
}