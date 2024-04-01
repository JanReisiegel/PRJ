using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuthServerRP.Services;
using OAuthServerRP.ViewModels;
using System.Reflection;

namespace OAuthServerRP.Areas.Identity.Pages
{
    public class LoginModel : PageModel
    {
        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clintStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        [BindProperty]
        public LoginIM Input { get; set; }
        public string ReturnUrl { get; set; }

        public LoginModel(TestUserStore users, IIdentityServerInteractionService interaction, IClientStore clintStore, IAuthenticationSchemeProvider schemeProvider, IEventService events)
        {
            _users = users??new TestUserStore(TestUsers.Users);
            _interaction = interaction;
            _clintStore = clintStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }

        public IActionResult OnGet()
        {
            ReturnUrl = HttpContext.Request.Headers["Referer"];
            return Page();
        }

        public async Task<IActionResult> OnPost()
        { 
            ReturnUrl ??= Url.Content("~/Index");
            var context = await _interaction.GetAuthorizationContextAsync(ReturnUrl);
            if(ModelState.IsValid)
            {
                if(_users.ValidateCredentials(Input.Username, Input.Password))
                {
                    var user = _users.FindByUsername(Input.Username);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.SubjectId, user.Username, clientId: context?.Client.ClientId));

                    AuthenticationProperties props = null;
                    if(Input.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(1))
                        };
                    }

                    var issuer = new IdentityServerUser(user.SubjectId)
                    {
                        DisplayName = user.Username
                    };

                    await HttpContext.SignInAsync(issuer, props);

                    if(context != null)
                    {
                        await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);
                        return Redirect(ReturnUrl);
                    }
                    if(Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else if(string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        throw new Exception("Invalid return URL");
                    }
                }
                await _events.RaiseAsync(new UserLoginFailureEvent(Input.Username, "invalid credentials", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }
            return Page();
        }
    }
}
