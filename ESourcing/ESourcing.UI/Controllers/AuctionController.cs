using Microsoft.AspNetCore.Mvc;

namespace ESourcing.UI.Controllers
{
    public class AuctionController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
