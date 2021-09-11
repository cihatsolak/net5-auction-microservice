using ESourcing.UI.Clients;
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
        private readonly ProductClient _productClient;
        #endregion

        #region Ctor
        public AuctionController(UserManager<AppUser> userManager, ProductClient productClient)
        {
            _userManager = userManager;
            _productClient = productClient;
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

            var products = await _productClient.GetProducts();
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
