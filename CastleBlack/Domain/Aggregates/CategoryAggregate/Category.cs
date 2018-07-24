using CastleBlack.Domain.SeedWork;

namespace CastleBlack.Domain.Aggregates.CategoryAggregate
{
    public class Category : IAggregateRoot
    {
        public Category(string title)
        {
            this.Title = title;
        }

        public string Title { get; set; }
        public Category Base { get; set; }
    }
}
