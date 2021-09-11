using ESourcing.UI.Models.Auctions;
using Microsoft.AspNetCore.Mvc;

namespace ESourcing.UI.Controllers
{
    public class AuctionController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AuctionViewModel());
        }

        [HttpPost]
        public IActionResult Create(AuctionViewModel auctionViewModel)
        {
            return View();
        }
    }
}
