using ESourcing.UI.Clients;
using ESourcing.UI.Core.Entities;
using ESourcing.UI.Models.Auctions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESourcing.UI.Controllers
{
    public class AuctionController : BaseController
    {
        #region Fields
        private readonly UserManager<AppUser> _userManager;
        private readonly ProductClient _productClient;
        private readonly AuctionClient _auctionClient;
        #endregion

        #region Ctor
        public AuctionController(
            UserManager<AppUser> userManager,
            ProductClient productClient,
            AuctionClient auctionClient)
        {
            _userManager = userManager;
            _productClient = productClient;
            _auctionClient = auctionClient;
        }
        #endregion

        #region Actions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _auctionClient.GetAuctionsAsync();
            if (!result.IsSuccess)
            {
                //Todo: Exception    
            }

            return View(result.Data);
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

            var products = await _productClient.GetProductsAsync();
            if (products.IsSuccess)
            {
                auctionViewModel.Products = products.Data.Select(product => new SelectListItem
                {
                    Text = product.Name,
                    Value = product.Id.ToString()
                }).ToList();
            }

            return View(auctionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuctionViewModel auctionViewModel)
        {
            auctionViewModel.Status = 1;
            auctionViewModel.CreatedAt = DateTime.Now;

            var result = await _auctionClient.CreateAuctionAsync(auctionViewModel);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, "Could not create auction");
                return View(result.Data);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            return View(new AuctionBidsViewModel());
        }

        [HttpPost]
        public IActionResult Detail(AuctionViewModel auctionViewModel)
        {
            return View();
        }
        #endregion
    }
}
