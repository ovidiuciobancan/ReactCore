using System;
using System.Collections.Generic;
using System.Text;
using Common.Interfaces;

namespace Common
{
    public class CommonSettings
    {
        public string DomainUrl { get; set; }
        public string ApiUrl { get; set; }
        public string ApiName { get; set; }
    }
    public class AuthSettings
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string ResponseType { get; set; }
        public string Scope { get; set; }
    }

    public class AppSettings : IAppSettings 
    {
        public CommonSettings CommonSettings { get; set; } = new CommonSettings();
        public AuthSettings AuthSettings { get; set; } = new AuthSettings();
    }
}
