using ESourcing.Sourcing.Data.Interfaces;
using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ESourcing.Sourcing.Repositories
{
    public class BidRepository : IBidRepository
    {
        #region Fields
        private readonly ISourcingContext _context;
        #endregion

        #region Ctor
        public BidRepository(ISourcingContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<List<Bid>> GetBidsByAuctionIdAsync(string id)
        {
            FilterDefinition<Bid> filter = Builders<Bid>.Filter.Eq(a => a.AuctionId, id);

            List<Bid> bids = await _context.Bids.Find(filter)
                                                .ToListAsync();

            bids = bids.OrderByDescending(a => a.CreatedAt)
                       .GroupBy(a => a.SellerUserName)
                       .Select(a => new Bid
                       {
                           AuctionId = a.FirstOrDefault().AuctionId,
                           Price = a.FirstOrDefault().Price,
                           CreatedAt = a.FirstOrDefault().CreatedAt,
                           SellerUserName = a.FirstOrDefault().SellerUserName,
                           ProductId = a.FirstOrDefault().ProductId,
                           Id = a.FirstOrDefault().Id
                       })
                       .ToList();

            return bids;
        }

        public async Task<List<Bid>> GetAllBidsByAuctionIdAsync(string id)
        {
            FilterDefinition<Bid> filter = Builders<Bid>.Filter.Eq(p => p.AuctionId, id);

            List<Bid> bids = await _context
                          .Bids
                          .Find(filter)
                          .ToListAsync();

            bids = bids.OrderByDescending(a => a.CreatedAt)
                                   .Select(a => new Bid
                                   {
                                       AuctionId = a.AuctionId,
                                       Price = a.Price,
                                       CreatedAt = a.CreatedAt,
                                       SellerUserName = a.SellerUserName,
                                       ProductId = a.ProductId,
                                       Id = a.Id
                                   })
                                   .ToList();

            return bids;
        }

        public async Task<Bid> GetWinnerBidAsync(string id)
        {
            List<Bid> bids = await GetBidsByAuctionIdAsync(id);

            return bids.OrderByDescending(a => a.Price).FirstOrDefault();
        }

        public async Task SendBidAsync(Bid bid)
        {
            await _context.Bids.InsertOneAsync(bid);
        }
        #endregion
    }
}
