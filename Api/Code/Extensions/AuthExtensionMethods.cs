using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AspCore.Extensions.Auth
{
    public static class AuthExtensionMethods
    {
        public static void AddHybridAuth(this IServiceCollection services, Action<HybridAuthOptions> configureOptions)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var hybridAuthOptions = new HybridAuthOptions
            {
                AuthenticationOptions = new AuthenticationOptions(),
                CookieAuthenticationOptions = new CookieAuthenticationOptions(),
                OpenIdConnectOptions = new OpenIdConnectOptions() { Events = new OpenIdConnectEvents() },
                IdentityServerAuthenticationOptions = new IdentityServerAuthenticationOptions(),
            };

             services
                .AddAuthentication(opt => 
                {
                    hybridAuthOptions.AuthenticationOptions = opt;
                    configureOptions(hybridAuthOptions);

                    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(opt => 
                {
                    hybridAuthOptions.CookieAuthenticationOptions = opt;
                    configureOptions(hybridAuthOptions);
                })
                .AddOpenIdConnect(opt => {
                    var defaultScopes = new List<string>() { "openid", "profile", "address", "roles" };

                    hybridAuthOptions.OpenIdConnectOptions = opt;
                    configureOptions(hybridAuthOptions);

                    opt.RequireHttpsMetadata = true;
                    opt.ResponseType = "code id_token";
                    //opt.CallbackPath = new PathString("/signin-oidc");

                    opt.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    opt.SaveTokens = true;
                    opt.GetClaimsFromUserInfoEndpoint = true;

                    opt.TokenValidationParameters = hybridAuthOptions.OpenIdConnectOptions.TokenValidationParameters ?? new TokenValidationParameters
                    {
                        NameClaimType = "given_name",
                        RoleClaimType = "role"
                    };

                    opt.Scope.Clear();
                    defaultScopes
                        .Union(hybridAuthOptions.OpenIdConnectOptions.Scope)
                        .Distinct()
                        .ToList()
                        .ForEach(scope => opt.Scope.Add(scope));

                    opt.Events.OnTokenValidated = ctx =>
                    {
                        var identity = ctx.Principal.Identity as ClaimsIdentity;
                        var subjectClaim = identity.Claims.FirstOrDefault(p => p.Type == "sub");

                        identity.Claims.ToList().ForEach(c => identity.RemoveClaim(c));
                        identity.AddClaim(subjectClaim);

                        return Task.FromResult(0);
                    };

                    opt.Events.OnUserInformationReceived = ctx =>
                    {
                        ClaimsIdentity claimsId = ctx.Principal.Identity as ClaimsIdentity;

                        foreach (var claim in ctx.User.ToClaims())
                        {
                            ((ClaimsIdentity)ctx.Principal.Identity).AddClaim(claim);
                        }

                        return Task.FromResult(0);
                    };
                })
                .AddIdentityServerAuthentication(opt =>
                {
                    opt.RequireHttpsMetadata = true;
                });
        }
    }
}
