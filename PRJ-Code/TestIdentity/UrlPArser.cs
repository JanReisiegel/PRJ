using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity
{
    public class UrlPArser
    {
        public string ParseUrl(string url)
        {
            List<string> urlParts = url.Split("?")[1].Split("&").ToList();
            string state = urlParts.FirstOrDefault(x => x.Contains("state"));
            Console.WriteLine(state);

            return "done";
        }
    }
}
