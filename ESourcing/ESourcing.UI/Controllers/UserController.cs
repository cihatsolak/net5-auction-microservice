using ESourcing.UI.Core.Entities;
using ESourcing.UI.Infrastructure.Extensions;
using ESourcing.UI.Models.Users;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ESourcing.UI.Controllers
{
    public class UserController : BaseController
    {
        #region Fields
        private readonly ILogger<UserController> _logger;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        #endregion

        #region Ctor
        public UserController(
            ILogger<UserController> logger,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region Methods
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInViewModel signInViewModel)
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserViewModel appUserViewModel)
        {
            if (!ModelState.IsValid)
                return View(appUserViewModel);

            var appUser = appUserViewModel.Adapt<AppUser>();
            appUser.IsBuyer = appUserViewModel.UserSelectedTypeId.Equals(UserType.Buyer);
            appUser.IsSeller = !appUser.IsBuyer;

            var identityResult = await _userManager.CreateAsync(appUser, appUserViewModel.Password);
            if (identityResult.Succeeded)
                return RedirectToAction(nameof(SignIn));

            identityResult.AddModelErrors(ModelState);
            return View(appUserViewModel);
        }
        #endregion
    }
}
