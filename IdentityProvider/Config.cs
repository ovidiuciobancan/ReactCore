using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityProvider
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {

                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "Frank",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Frank"),
                        new Claim("family_name", "Underwood"),
                        new Claim("address", "1, MainRoad"),
                        new Claim("role", "FreeUser"),
                    }
                },
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "Claire",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Claire"),
                        new Claim("family_name", "Underwood"),
                        new Claim("address", "1, MainRoad"),
                        new Claim("role", "PayingUser")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdetityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "Roles", new List<string>(){ "role" })
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("api", "api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientName = "Api Client",
                    ClientId = "apiclientid",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44344/signin-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "api",
                    },
                    ClientSecrets =
                    {
                        new Secret("apiclientsecret".Sha256())
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44344/signout-callback-oidc"
                    },
                    AllowOfflineAccess = true
                }
            };


        }
    }
}
