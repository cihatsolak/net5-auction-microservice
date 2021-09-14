using ESourcing.UI.Models.Bids;
using System.Collections.Generic;

namespace ESourcing.UI.Models.Auctions
{
    public class AuctionBidsViewModel
    {
        public string AuctionId { get; set; }
        public string ProductId { get; set; }
        public string SellerUserName { get; set; }
        public List<BidViewModel> Bids { get; set; }
    }

}
