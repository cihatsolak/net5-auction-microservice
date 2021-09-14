using System;

namespace ESourcing.UI.Models.Bids
{
    public class BidViewModel
    {
        public string Id{ get; set; }
        public string AuctionId { get; set; }
        public string ProductId { get; set; }
        public string SellerUserName { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
