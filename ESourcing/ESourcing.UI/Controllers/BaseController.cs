using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESourcing.UI.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class BaseController : Controller
    {
    }
}
