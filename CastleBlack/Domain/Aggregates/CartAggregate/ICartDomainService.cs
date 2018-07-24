using CastleBlack.Domain.Aggregates.CampaignAggregate;
using CastleBlack.Domain.Aggregates.ProductAggregate;

namespace CastleBlack.Domain.Aggregates.CartAggregate
{
    public interface ICartDomainService
    {
        void AddItem(Product product, int quantity);
        void ApplyDiscounts(params Campaign[] campaigns);
        void ApplyCoupon(Coupon coupon);
    }
}