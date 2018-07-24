using CastleBlack.Domain.Aggregates.ProductAggregate;
using System;
using CastleBlack.Domain.Aggregates.CartAggregate;
using CastleBlack.Domain.Aggregates.CategoryAggregate;
using Xunit;

namespace CastleBlack.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void AddItem_ShouldArgumentException_WhenProductNameIsNull()
        {
            var result = Record.Exception(
                () =>
                {
                    var cart = new Cart();
                    cart.AddItem(new Product(null, 0, null), 3);
                }
            );

            Assert.NotNull(result);

            var exception = Assert.IsType<ArgumentNullException>(result);
            var actual = exception.ParamName;

            const string expected = "apple";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddItem_ShouldArgumentException_WhenProductPriceEqualZero()
        {
            var result = Record.Exception(
                () =>
                {
                    var cart = new Cart();
                    cart.AddItem(new Product("apple", 0, new Category("food")), 3);
                }
            );

            Assert.NotNull(result);

            const int expected = 1;

            Assert.Equal(expected, 0)
        }

        [Fact]
        public void AddItem_ShouldArgumentException_WhenProductCategoryIsNull()
        {
            var result = Record.Exception(
                () =>
                {
                    var cart = new Cart();
                    cart.AddItem(new Product("apple", 1, null), 3);
                }
            );

            Assert.NotNull(result);

            var exception = Assert.IsType<ArgumentNullException>(result);
            var actual = exception.ParamName;

            const string expected = "food";

            Assert.Equal(expected, actual);
        }
    }
}
