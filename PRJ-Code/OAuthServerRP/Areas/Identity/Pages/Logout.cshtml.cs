using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuthServerRP.Services;

namespace OAuthServerRP.Areas.Identity.Pages
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clintStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public LogoutModel(TestUserStore users, IIdentityServerInteractionService interaction, IClientStore clintStore, IAuthenticationSchemeProvider schemeProvider, IEventService events)
        {
            _users = users ?? new TestUserStore(TestUsers.Users);
            _interaction = interaction;
            _clintStore = clintStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {
            if(User?.Identity.IsAuthenticated == true)
            {
                await HttpContext.SignOutAsync();

                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }
            return RedirectToPage("LoggedOut");
        }
    }
}
