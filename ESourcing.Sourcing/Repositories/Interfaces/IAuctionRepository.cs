using ESourcing.Sourcing.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Repositories.Interfaces
{
    public interface IAuctionRepository
    {
        Task<IEnumerable<Auction>> GetAuctionsAsync();
        Task<Auction> GetAuctionByIdAsync(string id);
        Task<Auction> GetAuctionByNameAsync(string name);
        Task InsertAsync(Auction auction);
        Task<bool> UpdateAsync(Auction auction);
        Task<bool> DeleteAsync(string id);
    }
}
