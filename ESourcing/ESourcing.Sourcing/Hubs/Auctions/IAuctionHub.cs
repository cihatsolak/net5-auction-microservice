using System.Threading.Tasks;

namespace ESourcing.Sourcing.Hubs.Auctions
{
    public interface IAuctionHub
    {
        Task BidsAsync(string user, string bid);
    }
}
