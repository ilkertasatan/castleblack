using System;
using CastleBlack.Domain.Aggregates.CampaignAggregate;
using CastleBlack.Domain.Aggregates.CartAggregate;
using CastleBlack.Domain.Aggregates.CategoryAggregate;
using CastleBlack.Domain.Aggregates.ProductAggregate;

namespace CastleBlack.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var category = new Category("food");

            var apple = new Product("Apple", 100.0, category);
            var almond = new Product("Almonds", 150.0, category);

            var cart = new Cart();

            cart.AddItem(apple, 3);
            cart.AddItem(almond, 1);


            var campaign1 = new Campaign(category, 20.0, 3, DiscountType.Rate);
            var campaign2 = new Campaign(category, 50.0, 5, DiscountType.Rate);
            var campaign3 = new Campaign(category, 5.0, 5, DiscountType.Amount);

            cart.ApplyDiscounts(campaign1, campaign2, campaign3);


            var coupon = new Coupon(100, 10, DiscountType.Rate);
            cart.ApplyCoupon(coupon);

            Console.WriteLine(cart.Print());
            Console.ReadLine();
        }
    }
}
