namespace MicroserviceCourse.Order.Domain.Entities;

public class OrderItem : BaseEntity<int>
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    //rich domain model
    public void SetItem(Guid productId, string productName, decimal unitPrice)
    {
        if (string.IsNullOrEmpty(productName))
        {
            throw new ArgumentException("productName cannot be null or empty.");
        }

        if (unitPrice <= 0)
        {
            throw new ArgumentException("unitPrice cannot be less or equal to zero.");
        }

        this.ProductId = productId;
        this.ProductName = productName;
        this.UnitPrice = unitPrice;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
        {
            throw new ArgumentException("newPrice cannot be less or equal to zero.");
        }

        this.UnitPrice = newPrice;
    }

    public void ApplyDiscount(float discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(discountPercentage),
                "discountPercentage must be between 0 and 100.");
        }

        this.UnitPrice = this.UnitPrice - (this.UnitPrice * (decimal)discountPercentage / 100);
    }


    public bool IsSameItem(OrderItem orderItem) => this.ProductId == orderItem.ProductId;
}