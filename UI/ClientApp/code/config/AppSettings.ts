import { UserManagerSettings } from 'oidc-client'

interface ICommonSettings {
    apiUrl: string
};

interface IAuthSettings {
    authority: string,
    clientId: string,
    scope: string 
    oidcConfig: UserManagerSettings
}

interface IAppSettings {
    common: ICommonSettings,
    auth: IAuthSettings,
   
}


const settings = (document as any)["app"]["settings"] as IAppSettings;

settings.auth.oidcConfig = <UserManagerSettings>{
    authority: settings.auth.authority,
    client_id: settings.auth.clientId,
    redirect_uri: `${window.location.protocol}//${window.location.hostname}:${window.location.port}/oidc-callback`,
    response_type: "id_token token",
    scope: "openid profile api",
    silent_redirect_uri: `${window.location.protocol}//${window.location.hostname}:${window.location.port}/oidc-callback`,
    popup_redirect_uri: `${window.location.protocol}//${window.location.hostname}:${window.location.port}/oidc-callback`,
    popup_post_logout_redirect_uri: `${window.location.protocol}//${window.location.hostname}:${window.location.port}/oidc-callback`,
    automaticSilentRenew: true,
    filterProtocolClaims: true,
    //loadUserInfo: true
}


export const appSettings = {
    ...settings,
    
} as IAppSettings