using System;
using System.Collections.Generic;
using System.Text;
using Common.Interfaces;

namespace Common
{
    public class AppSettings : IAppSettings
    {
        public string AuthAuthority { get; set; }
        public string ClientAppId { get; set; }
        public string ClientSecret { get; set; }
        public string ApiName { get; set; }
    }
}
