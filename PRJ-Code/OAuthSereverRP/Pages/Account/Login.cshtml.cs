using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuthSereverRP.Models.InputModels;

namespace OAuthSereverRP.Pages
{
    public class LoginModel : PageModel
    {
        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        public LoginModel(TestUserStore users, IIdentityServerInteractionService interaction, IEventService events)
        {
            _users = users;
            _interaction = interaction;
            _events = events;
        }
        [BindProperty]
        public LoginIM Login { get; set; } = new LoginIM();
        public async void OnGet(string redirectUrl)
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (ModelState.IsValid)
            {
                if (_users.ValidateCredentials(Login.Username, Login.Password))
                {
                    var user = _users.FindByUsername(Login.Username);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.SubjectId, user.Username, clientId: context?.Client.ClientId));

                    //pokud uživatel zadal RememberLogin, tak se mu nastaví cookie, která mu umožní pøihlášení i po zavøení prohlížeèe
                    AuthenticationProperties props = null;
                    if (Login.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30))
                        };
                    };

                    var issuer = new IdentityServerUser(user.SubjectId)
                    {
                        DisplayName = user.Username
                    };

                    await HttpContext.SignInAsync(issuer, props);

                    if (context != null)
                    {
                        return Redirect(Login.ReturnUrl);
                    }

                    //request for a local page
                    if (Url.IsLocalUrl(Login.ReturnUrl))
                    {
                        return Redirect(Login.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(Login.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        //user might have clicked on a malicious link - should be logged
                        throw new Exception("invalid return URL");
                    }
                }
                await _events.RaiseAsync(new UserLoginFailureEvent(Login.Username, "invalid credentials", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, "Invalid credentials");
            }
            return Page();
        }
        
    }
}
