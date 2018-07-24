using CastleBlack.Calculators;
using CastleBlack.Domain.Aggregates.CampaignAggregate;
using CastleBlack.Domain.Aggregates.ProductAggregate;
using CastleBlack.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CastleBlack.Domain.Aggregates.CartAggregate
{
    public class Cart : IAggregateRoot
    {
        private double totalAmountAfterDiscounts = 0;
        private double couponDiscount = 0;
        private double campaignDiscount = 0;

        public Cart()
        {
            this.Products = new Dictionary<Product, int>();
        }

        public readonly IDictionary<Product, int> Products;

        public void AddItem(Product product, int quantity)
        {
            if (product == null || quantity == 0)
                throw new ArgumentNullException(nameof(product));

            if (product.Category == null)
                throw new ArgumentNullException(nameof(product.Category));

            if (string.IsNullOrEmpty(product.Title))
                throw new ArgumentNullException(nameof(product.Title));

            this.Products.Add(product, quantity);
        }

        public void ApplyCoupon(Coupon coupon)
        {
            if (coupon == null)
                return;

            var totalPriceOfAllProducts = this.Products.Keys.ToList().Sum(x => x.Price);

            if (totalPriceOfAllProducts > coupon.Price)
            {
                var allProducts = this.Products.Keys.ToList();

                foreach (var product in allProducts)
                {
                    switch (coupon.DiscountType)
                    {
                        case DiscountType.Rate:

                            var newPriceAccordingToRate = product.Price - GetPriceOfRate(product.Price, coupon.Rate);
                            totalAmountAfterDiscounts += newPriceAccordingToRate;
                            couponDiscount += newPriceAccordingToRate;


                            break;

                        case DiscountType.Amount:

                            var newPriceAccordingToAmount = GetPriceOfAmount(product.Price, coupon.Rate);
                            totalAmountAfterDiscounts += newPriceAccordingToAmount;
                            couponDiscount += newPriceAccordingToAmount;

                            break;
                    }
                }
            }
        }

        public void ApplyDiscounts(params Campaign[] campaigns)
        {
            if (campaigns == null || campaigns.Length == 0)
                return;

            var grouppedProducts = this.Products.GroupBy(r => r.Key.Category)
                                                .ToDictionary(x => x.Key, x => x.ToList());

            foreach (var grouppedProduct in grouppedProducts.Keys)
            {
                foreach (var campaign in campaigns.Where(x => x.Category == grouppedProduct))
                {
                    foreach (var item in grouppedProducts[grouppedProduct])
                    {
                        var product = item.Key;

                        if (this.Products[product] > campaign.Quantity)
                        {
                            switch (campaign.DiscountType)
                            {
                                case DiscountType.Rate:

                                    var newPriceAccordingToRate = product.Price - GetPriceOfRate(product.Price, campaign.Rate);
                                    totalAmountAfterDiscounts += newPriceAccordingToRate;
                                    campaignDiscount += newPriceAccordingToRate;

                                    break;

                                case DiscountType.Amount:

                                    var newPriceAccordingToAmount = GetPriceOfAmount(product.Price, campaign.Rate);
                                    totalAmountAfterDiscounts += newPriceAccordingToAmount;
                                    campaignDiscount += newPriceAccordingToAmount;

                                    break;
                            }
                        }
                    }
                }
            }
        }

        public double GetTotalAmountAfterDiscounts()
        {
            return totalAmountAfterDiscounts;
        }

        public double GetCouponDiscount()
        {
            return couponDiscount;
        }

        public double GetCampaignDiscount()
        {
            return campaignDiscount;
        }

        public double GetDeliveryCost()
        {
            return new DeliveryCostCalculator().CalculateFor(this);
        }

        public string Print()
        {
            var culture = new CultureInfo("tr-TR");
            return $"Total amount: {this.GetTotalAmountAfterDiscounts().ToString(culture)}, " +
                   $"Delivery cost: {this.GetDeliveryCost().ToString(culture)}";
        }

        public override string ToString()
        {
            return this.Print();
        }

        private double GetPriceOfRate(double price, double rate)
        {
            if (price <= 0)
                return 0;

            return (price * rate) / 100;
        }

        private double GetPriceOfAmount(double price, double amount)
        {
            if (price <= 0)
                return 0;

            return price - amount;
        }
    }
}
