using ESourcing.UI.Core.Entities;
using ESourcing.UI.Infrastructure.Extensions;
using ESourcing.UI.Models.Users;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ESourcing.UI.Controllers
{
    [AllowAnonymous]
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
        public IActionResult SignIn(string returnUrl = "~/")
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Auction");

            if (!Url.IsLocalUrl(returnUrl))
                return View();

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel, string returnUrl = "~/")
        {
            var user = await _userManager.FindByEmailAsync(signInViewModel.Email);
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "User not found!");
                return View(signInViewModel);
            }

            await _signInManager.SignOutAsync();
            var signInResult = await _signInManager.PasswordSignInAsync(user, signInViewModel.Password, false, false);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email address or password is invalid");
                return View(signInViewModel);
            }

            return RedirectToAction("Index", "Home");
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

        public async Task<IActionResult> SignOutt()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion
    }
}
