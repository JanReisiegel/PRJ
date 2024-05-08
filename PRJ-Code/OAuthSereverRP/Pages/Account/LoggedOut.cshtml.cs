using IdentityModel;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OAuthSereverRP.Pages.Account
{
    public class LoggedOutModel : PageModel
    {
        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        public string PostLogoutRedirectUri { get; set; }
        public string ClientName { get; set; }
        public string SignOutIframeUrl { get; set; }

        public string LogoutId { get; set; }
        public LoggedOutModel(TestUserStore users, IIdentityServerInteractionService interaction, IEventService events)
        {
            _users = users;
            _interaction = interaction;
            _events = events;
        }
        public async Task<IActionResult> OnGet(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);
            PostLogoutRedirectUri = logout?.PostLogoutRedirectUri;
            ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName;
            SignOutIframeUrl = logout?.SignOutIFrameUrl;
            LogoutId = logoutId;

            if(User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    if(LogoutId == null)
                    {
                        LogoutId = await _interaction.CreateLogoutContextAsync();
                    }
                }
            }
            return Page();
        }
    }
}
