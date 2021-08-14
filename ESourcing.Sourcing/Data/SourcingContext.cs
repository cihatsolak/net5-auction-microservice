using ESourcing.Sourcing.Data.Interfaces;
using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Settings.SourcingDatabase;
using MongoDB.Driver;

namespace ESourcing.Sourcing.Data
{
    public class SourcingContext : ISourcingContext
    {
        #region Ctor
        public SourcingContext(ISourcingDatabaseSettings sourcingDatabaseSettings)
        {
            var mongoClient = new MongoClient(sourcingDatabaseSettings.ConnectionString);
            var database = mongoClient.GetDatabase(sourcingDatabaseSettings.DatabaseName);

            Auctions = database.GetCollection<Auction>(nameof(Auction));
            Bids = database.GetCollection<Bid>(nameof(Bid));
        }
        #endregion

        #region Mongo Collections
        public IMongoCollection<Auction> Auctions { get; }
        public IMongoCollection<Bid> Bids { get; }
        #endregion
    }
}
