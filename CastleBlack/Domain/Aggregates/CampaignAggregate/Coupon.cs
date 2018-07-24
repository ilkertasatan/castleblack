namespace CastleBlack.Domain.Aggregates.CampaignAggregate
{
    public class Coupon
    {
        public Coupon(double price, int rate, DiscountType discountType)
        {
            this.Price = price;
            this.Rate = rate;
            this.DiscountType = discountType;
        }

        public double Price { get; set; }
        public int Rate { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}
