using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ESourcing.UI.Infrastructure.Extensions
{
    internal static class MVCHelper
    {
        internal static void AddModelErrors(this IdentityResult identityResult, ModelStateDictionary ModelState)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
