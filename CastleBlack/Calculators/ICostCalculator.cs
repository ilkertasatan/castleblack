using CastleBlack.Domain.Aggregates.CartAggregate;

namespace CastleBlack.Calculators
{
    public interface ICostCalculator
    {
        double CalculateFor(Cart cart);
    }
}
