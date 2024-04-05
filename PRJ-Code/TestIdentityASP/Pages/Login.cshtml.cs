using IdentityModel.Client;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;

namespace TestIdentityASP.Pages
{
    public class LoginModel : PageModel
    {
        public string Conetnt { get; set; } = "";
        public IDictionary<string, string> Clients { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var authResult = await HttpContext.GetTokenAsync("oidc", "access_token");
            /*var client = new HttpClient();
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            Conetnt = await client.GetStringAsync("https://localhost:7210/identity");*/
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            Conetnt = await client.GetStringAsync("https://localhost:7210/identity");
            return Page();
        }   
    }
}
