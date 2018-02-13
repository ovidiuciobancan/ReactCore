using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    public interface ICommonSettings
    {
        string DomainUrl { get; set; }
        string ApiUrl { get; set; }
        string ApiName { get; set; }
    }

    public interface IAuthSettings
    {
        string AuthAuthority { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string RedirectUri { get; set; }
        string ResponseType { get; set; }
        string Scope { get; set; } 
    }

    public interface IAppSettings
    {
        CommonSettings CommonSettings { get; set; }
        AuthSettings AuthSettings { get; set; }
    }
}
