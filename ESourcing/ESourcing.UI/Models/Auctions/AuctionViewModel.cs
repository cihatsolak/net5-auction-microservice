using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ESourcing.UI.Models.Auctions
{
    public class AuctionViewModel : BaseViewModel
    {
        public AuctionViewModel()
        {
            Sellers = new List<SelectListItem>();
            Products = new List<SelectListItem>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }

        public string SellerId { get; set; }
        public IList<SelectListItem> Sellers { get; set; }

        public string ProductId { get; set; }
        public IList<SelectListItem> Products { get; set; }
    }
}
