using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Cmos.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles" , "Your role(s)" , new List<string> { "role" })
        };

    public static IEnumerable<ApiResource> ApiResources =>
      new ApiResource[]
          {
              new ApiResource("cmosApi" , "CMOS Photo Api")
              { 
                  Scopes = { "cmosApi.fullAccess" }
              },
             
          };
    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
             new ApiScope("cmosApi.fullAccess") 
            };

  
    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                new Client {
                    ClientName = "CMOSClient",
                    ClientId = "cmos",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string> { "https://localhost:7184/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7184/signout-callback-oidc" },
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile , "roles" , "cmosApi.fullAccess" },
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    RequireConsent = true,

                }
            };
   }