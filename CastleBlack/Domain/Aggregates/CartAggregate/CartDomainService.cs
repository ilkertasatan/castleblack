using CastleBlack.Domain.Aggregates.CampaignAggregate;
using CastleBlack.Domain.Aggregates.ProductAggregate;
using System;

namespace CastleBlack.Domain.Aggregates.CartAggregate
{
    public class CartDomainService : ICartDomainService
    {
        public void AddItem(Product product, int quantity)
        {

        }

        public void ApplyCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public void ApplyDiscounts(params Campaign[] campaigns)
        {
            throw new NotImplementedException();
        }
    }
}
