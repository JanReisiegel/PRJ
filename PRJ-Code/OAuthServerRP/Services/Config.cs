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
                }
            };
    }
}
