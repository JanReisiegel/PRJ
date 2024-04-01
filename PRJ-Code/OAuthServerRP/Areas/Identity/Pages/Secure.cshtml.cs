using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace OAuthServerRP.Areas.Identity.Pages
{
    [Authorize]
    public class SecureModel : PageModel
    {
        public IEnumerable<string> Clients { get; set; }
        public AuthenticateResult AuthenticateResult { get; set; }


        public async Task<IActionResult> OnGet()
        {
            var localAddresses = new string[]
            {
                "127.0.0.1",
                "::1",
                HttpContext.Connection.LocalIpAddress.ToString(),
                "localhost"
            };
            if(!localAddresses.Contains(HttpContext.Connection.RemoteIpAddress.ToString()))
            {
                return NotFound();
            }

            var AuthenticateResult = await HttpContext.AuthenticateAsync();

            if (AuthenticateResult.Properties.Items.ContainsKey("client_list"))
            {
                var encoded = AuthenticateResult.Properties.Items["client_list"];
                var bytes = Convert.FromBase64String(encoded);
                var json = Encoding.UTF8.GetString(bytes);

                Clients = JsonConvert.DeserializeObject<IEnumerable<string>>(json);
                return Page();
            }
            return Page();
        }
    }
}
