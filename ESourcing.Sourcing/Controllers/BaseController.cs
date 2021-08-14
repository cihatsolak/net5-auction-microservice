using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ESourcing.Sourcing.Controllers
{
    [Route("api/v1/[controller]/[action]"), ApiController, Produces(MediaTypeNames.Application.Json)]
    public class BaseController : ControllerBase
    {
    }
}
