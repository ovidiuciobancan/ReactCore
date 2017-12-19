using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    public interface IAppSettings
    {
        string AuthAuthority { get; set; }
        string ClientAppId { get; set; }
        string ClientSecret { get; set; }
    }
}
