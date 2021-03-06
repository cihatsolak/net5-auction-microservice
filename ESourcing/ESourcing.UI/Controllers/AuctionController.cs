using ESourcing.UI.Clients;
using ESourcing.UI.Core.Entities;
using ESourcing.UI.Core.ResultModels;
using ESourcing.UI.Models.Auctions;
using ESourcing.UI.Models.Bids;
using Microsoft.AspNetCore.Http;
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
        private readonly BidClient _bidClient;
        #endregion

        #region Ctor
        public AuctionController(
            UserManager<AppUser> userManager,
            ProductClient productClient,
            AuctionClient auctionClient, 
            BidClient bidClient)
        {
            _userManager = userManager;
            _productClient = productClient;
            _auctionClient = auctionClient;
            _bidClient = bidClient;
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
            auctionViewModel.Status = default;
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
        public async Task<IActionResult> Detail(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return RedirectToAction(nameof(Index));

            var auctionResult = await _auctionClient.GetAuctionByIdAsync(id);
            if (!auctionResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            var bidsResult = await _bidClient.GetBidsByAuctionId(id);
            
            AuctionBidsViewModel auctionBidsViewModel = new()
            {
                AuctionId = auctionResult.Data.Id,
                ProductId = auctionResult.Data.ProductId,
                SellerUserName = HttpContext.User.Identity.Name,
                Bids = bidsResult.Data,
                IsAdmin = bool.Parse(HttpContext.Session.GetString("IsAdmin"))
            };

            return View(auctionBidsViewModel);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SendBid(BidViewModel bidViewModel)
        {
            bidViewModel.CreatedAt = DateTime.Now;
            var sendBidResponse = await _bidClient.SendBidAsync(bidViewModel);
            return Json(sendBidResponse);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> CompleteAuction(string auctionId)
        {
            var result = await _auctionClient.CompleteAuctionAsync(auctionId);
            return Json(result.Data);
        }
        #endregion
    }
}
