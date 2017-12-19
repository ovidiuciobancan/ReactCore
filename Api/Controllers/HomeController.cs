using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Common;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Api.Controllers
{
    public class HomeController : Controller
    {
        private AppSettings AppSettings;

        public HomeController(AppSettings appSettings)
        {
            AppSettings = appSettings;
        }

        public async Task<IActionResult> Index()
        {
            await WriteOutIdentityInformation();
            return View();
        }

        [Authorize(Roles="PayingUser")]
        public async Task<IActionResult> OrderFrame()
        {
            var discoveryClient = new DiscoveryClient(AppSettings.AuthAuthority);
            var metadataResponse = await discoveryClient.GetAsync();

            var userInfoClient = new UserInfoClient(metadataResponse.UserInfoEndpoint);

            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var response = await userInfoClient.GetAsync(accessToken);

            if(response.IsError)
            {
                throw new Exception("Problem accessing the UserInfo endpoint", response.Exception);
            }

            var address = response.Claims.FirstOrDefault(c => c.Type == "address")?.Value;

            return Ok(address);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        public async Task WriteOutIdentityInformation()
        {
            Debug.WriteLine("______________________________________________________________________________");
            Debug.WriteLine("______________________________________________________________________________");

            var identityToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            Debug.WriteLine($"Identity token: {identityToken}");

            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }

            Debug.WriteLine("______________________________________________________________________________");
            Debug.WriteLine("______________________________________________________________________________");

        }
    }
}
