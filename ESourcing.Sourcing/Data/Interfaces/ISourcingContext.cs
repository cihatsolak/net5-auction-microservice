using ESourcing.Sourcing.Entities;
using MongoDB.Driver;

namespace ESourcing.Sourcing.Data.Interfaces
{
    public interface ISourcingContext
    {
        IMongoCollection<Auction> Auctions { get; }
        IMongoCollection<Bid> Bids { get; }
    }
}
