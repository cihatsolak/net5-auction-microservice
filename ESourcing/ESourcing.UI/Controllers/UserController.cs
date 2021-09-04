using ESourcing.UI.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESourcing.UI.Controllers
{
    public class UserController : Controller
    {
        #region Fields
        private readonly ILogger<UserController> _logger;
        #endregion

        #region Ctor
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
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
        #endregion
    }
}
