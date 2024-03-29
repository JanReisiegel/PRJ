using IdentityServer4;
using IdentityServer4.Models;
using OAuthServerRP.Models;

namespace OAuthServerRP.Services
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(AuthorizationConstants.ADMINISTRATOR_ROLE, "My API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // scopes that client has access to
                    AllowedScopes = { AuthorizationConstants.ADMINISTRATOR_ROLE }
                },

                new Client
                {
                    ClientId = "RazorPages",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:7210/signin-oidc" },

                    PostLogoutRedirectUris = { "https://localhost:7210/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    } }
            };
    }
}
