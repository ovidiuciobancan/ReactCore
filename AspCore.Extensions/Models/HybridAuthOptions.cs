using System;
using System.Collections.Generic;
using System.Text;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;

namespace AspCore.Extensions.Auth
{
    public class HybridAuthOptions
    {
        public AuthenticationOptions AuthenticationOptions { get; set; }
        public CookieAuthenticationOptions CookieAuthenticationOptions { get; set; }
        public OpenIdConnectOptions OpenIdConnectOptions { get; set; }
        public IdentityServerAuthenticationOptions IdentityServerAuthenticationOptions { get; set; }
    }
}
