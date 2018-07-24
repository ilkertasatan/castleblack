using CastleBlack.Domain.Aggregates.CartAggregate;
using System.Linq;

namespace CastleBlack.Calculators
{
    public class DeliveryCostCalculator : ICostCalculator
    {
        public double CalculateFor(Cart cart)
        {
            const double fixedCost = 2.99;
            double deliveryCost = 0;

            var numberOfDeliveries = cart.Products.Keys.Select(x => x.Category).Distinct().Count();
            var numberOfProducts = cart.Products.Keys.Distinct().Count();
            var costOfPerProduct = 1.99; //It wasn't wrote how I calculate it in the case.
            var costOfPerDelivery = 3.90; //It wasn't wrote how I calculate it in the case.

            deliveryCost = costOfPerDelivery * numberOfDeliveries + costOfPerProduct * numberOfProducts + fixedCost;
            return deliveryCost;
        }
    }
}
