using IdentityServer4;
using IdentityServer4.Models;

namespace Infra.IdentityServer.Configurations
{
    public class Configs
    {
        //clients
        public static IEnumerable<Client> Clients =>
            new List<Client> {
            new Client { ClientId = "BackendUI", AllowedGrantTypes = GrantTypes.CodeAndClientCredentials, ClientSecrets = { new Secret("secret".Sha256()) },
            RedirectUris = { "http://localhost:8075/Backendui/signin-oidc" },PostLogoutRedirectUris = { "http://localhost:8075/Backendui/logout" }, AllowOfflineAccess = true,
            AllowedScopes = { "BackendApi", IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile } },
            new Client { ClientId = "BackendUILogin", AllowedGrantTypes = GrantTypes.CodeAndClientCredentials, ClientSecrets = { new Secret("secret".Sha256()) },
            RedirectUris = { "http://localhost:8075/Backendui/signin-oidc" },PostLogoutRedirectUris = { "http://localhost:8075/Backendui/logout" }, AllowOfflineAccess = true,
            AllowedScopes = { "BackendApi", IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile } },
            new Client { ClientId = "FrontendUI", AllowedGrantTypes = GrantTypes.CodeAndClientCredentials, ClientSecrets = { new Secret("secret".Sha256()) },
            RedirectUris = { "http://localhost:8075/FrontendUI/signin-oidc" },PostLogoutRedirectUris = { "http://localhost:8075/FrontendUI/logout" }, AllowOfflineAccess = true,
            AllowedScopes = { "FrontendApi", IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile } } 
            };
        //identityResources
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        //ApiScopes
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> { 
            new ApiScope("BackendApi", "Backend Api"),
            new ApiScope("FrontendApi", "Frontend Api") 
            };
}
}
