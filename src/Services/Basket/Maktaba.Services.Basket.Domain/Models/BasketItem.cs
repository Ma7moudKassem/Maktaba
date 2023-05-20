namespace Maktaba.Services.Basket.Domain;

public class BasketItem : IValidatableObject
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string BookName { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public decimal OldUnitPrice { get; set; }
    public int Quantity { get; set; }
    public string? PictureUrl { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> results = new();

        if (Quantity < 1)
            results.Add(new ValidationResult("Invalid number of units", new[] { "Quantity" }));

        return results;
    }
}