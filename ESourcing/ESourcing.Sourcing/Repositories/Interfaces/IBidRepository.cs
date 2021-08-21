using ESourcing.Sourcing.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Repositories.Interfaces
{
    public interface IBidRepository
    {
        Task SendBidAsync(Bid bid);
        Task<List<Bid>> GetBidsByAuctionIdAsync(string id);
        Task<List<Bid>> GetAllBidsByAuctionIdAsync(string id);
        Task<Bid> GetAuctionWinnigBidByAuctionIdAsync(string auctionId);
    }
}
