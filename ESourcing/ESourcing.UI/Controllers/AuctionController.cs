using ESourcing.UI.Core.Entities;
using ESourcing.UI.Models.Auctions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESourcing.UI.Controllers
{
    public class AuctionController : BaseController
    {
        #region Fields
        private readonly UserManager<AppUser> _userManager;
        #endregion

        #region Ctor
        public AuctionController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        #endregion

        #region Actions
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            AuctionViewModel auctionViewModel = new();
            auctionViewModel.Sellers = await _userManager.Users.Select(user => new SelectListItem
            {
                Text = string.Concat(user.FirstName, " ", user.LastName),
                Value = user.Id
            }).ToListAsync();

            auctionViewModel.Products = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text ="test",
                    Value= "1"
                }
            };

            return View(auctionViewModel);
        }

        [HttpPost]
        public IActionResult Create(AuctionViewModel auctionViewModel)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (0 >= id)
                return null;



            return View(new AuctionViewModel());
        }

        [HttpPost]
        public IActionResult Detail(AuctionViewModel auctionViewModel)
        {
            return View();
        }
        #endregion
    }
}
