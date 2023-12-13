using System.ComponentModel.DataAnnotations;

namespace eShop.Discount.Grpc.Domain.Entities;

public class Coupon
{
    [Key]
    public Guid Id { get; set; }

    public string ProductName { get; set; } = default!;

    public string Description { get; set; } = default!;

    public int Amount { get; set; } = default!;
}