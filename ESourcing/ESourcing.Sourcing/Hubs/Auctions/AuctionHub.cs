using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Hubs.Auctions
{
    public class AuctionHub : Hub<IAuctionHub>
    {
        public async Task AddToGroupAsync(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendBidAsync(string groupName, string user, string bid)
        {
            await Clients.Group(groupName).BidsAsync(user, bid);
        }
    }
}
