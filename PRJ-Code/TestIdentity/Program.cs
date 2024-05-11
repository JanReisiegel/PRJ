using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using TestIdentity;

Console.WriteLine("Hello World!");


//RP.Run();
//TutorialMVC.Run();
//Console.ReadKey();
UrlPArser up = new UrlPArser();
string parsedUrl = up.ParseUrl("/connect/authorize/callback?client_id=mvc&redirect_uri=https%3A%2F%2Flocalhost%3A7151%2Fsignin-oidc&response_type=code&scope=openid%20profile&code_challenge=f42oSrJM-Z7MZ3whh7_bM-4lbtiFVkGFr9MLUiYZ74o&code_challenge_method=S256&response_mode=form_post&nonce=638510155581030028.ZmEwMmYwMmEtYWFkYi00NmQ3LWEzNTctNTMyOGU2MTk0ODJhMjg2OThiYTItODhiOC00MDczLThiY2UtNWFmMzkyNDY2Y2M5&state=CfDJ8HbW_Ctg4fxNponSKWJaIUKDNxokyovwW5VjVbIfZ_-dPHAfPM6Y1wsJjpu0ma9jrmUS1xdOGYcSyLWek3K0ibtweldj9mUyXf2yinGOUXxPVHMDTJm4RRa_abXvdRZg5FC-L4YgGNByh6gwRounN-ikeY79BtaznvUlYNMDdUwDPno08CdYlGrPeM1QNBqyX3JEj-gt4fFOAannDVZf4dsk2xs-YE7yVjFdWEfakIL8l-4Q8acC6Fgo9XFuGoCuR6niz8VKjaWnswEJ-VFbsH7chS0bW02u4JctuYsm7qsITm9xhhoWweXAlElL8F2u4m8D3YODIxdRad-t5uEsFh5Q0rgZeRVyKRoXNsKUEEnqMI2WjfxwCSBK8cp-BKB8pQ&x-client-SKU=ID_NET8_0&x-client-ver=7.1.2.0");
Console.WriteLine(parsedUrl);