using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OAuthSereverRP.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        public LogoutModel(TestUserStore users, IIdentityServerInteractionService interaction, IEventService events)
        {
            _users = users;
            _interaction = interaction;
            _events = events;
        }
        [BindProperty]
        public string LogoutId { get; set; }
        public async Task<IActionResult> OnGet(string logoutId)
        {
            LogoutId = logoutId;
            if (User?.Identity.IsAuthenticated == true)
            {
                await HttpContext.SignOutAsync();

                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }
            return RedirectToAction("LoggedOut");
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            if (User?.Identity.IsAuthenticated == true)
            {
                await HttpContext.SignOutAsync();

                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }
            return RedirectToAction("LoggedOut", new { returnUrl });
        }
    }
}
