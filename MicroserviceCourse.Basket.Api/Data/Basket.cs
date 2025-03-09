using MicroserviceCourse.Basket.Api.Dtos;
using System.Text.Json.Serialization;

namespace MicroserviceCourse.Basket.Api.Data
{
    //Rich domain model (behavior + data) içerir
    public class Basket
    {
        public Guid UserId { get; set; }
        public List<BasketItem> Items { get; set; } = new();
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        public Basket(Guid userId, List<BasketItem> items)
        {
            userId = UserId;
            Items = items;
        }

        public Basket()
        {

        }


        public decimal TotalPrice => Items.Sum(x => x.Price);
        public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);

        public decimal? TotalPriceByApplyDiscountRate =>
            !IsApplyDiscount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);

        public void ApplyNewDiscount(string coupon, float discountRate)
        {
            Coupon = coupon;
            DiscountRate = discountRate;

            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - discountRate);
            }
        }

        public void ApplyAvailableDiscount()
        {
            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - DiscountRate!);
            }
        }

        public void ClearDiscount()
        {
            DiscountRate = null;
            Coupon = null;
            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = null;
            }
        }

    }
}
