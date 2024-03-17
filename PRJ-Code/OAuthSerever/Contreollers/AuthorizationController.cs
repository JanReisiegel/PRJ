using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;

namespace OAuthSerever.Contreollers
{
    public class AuthorizationController : Controller
    {
        private readonly IOpenIddictApplicationManager _applicationManager;

        public AuthorizationController(IOpenIddictApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }
    }
}
