﻿using IdentityModel.Client;
using Newtonsoft.Json.Linq;

Console.WriteLine("Hello World!");


var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7210");
if(disco.IsError)
{
    Console.WriteLine(disco.Error);
    Console.ReadKey();
    return;
}

var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,
    ClientId = "client",
    ClientSecret = "secret",
    Scope = "api1"
});
if(tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    Console.ReadKey();
    return;
}
Console.WriteLine(tokenResponse.Json);
Console.WriteLine("\n\n");

var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:7210/identity");
if(!response.IsSuccessStatusCode)
{
    Console.WriteLine($"StatusCode: {response.StatusCode}");
}
else
{
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(content);
    Console.ReadKey();
}
Console.ReadKey();