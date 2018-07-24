using CastleBlack.Domain.Aggregates.CategoryAggregate;

namespace CastleBlack.Domain.Aggregates.CampaignAggregate
{
    public class Campaign
    {
        public Campaign(Category category, double rate, int quantity, DiscountType discountType)
        {
            this.Category = category;
            this.Rate = rate;
            this.Quantity = quantity;
            this.DiscountType = discountType;
        }

        public double Rate { get; set; }
        public int Quantity { get; set; }
        public DiscountType DiscountType { get; set; }
        public Category Category { get; set; }
    }
}
