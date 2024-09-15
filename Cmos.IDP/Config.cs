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
            new IdentityResource("roles" , "Your role(s)" , new List<string> { "role" }),
            new IdentityResource("country" , "The country you are living in" , new List<string> { "country" }),
        };

    public static IEnumerable<ApiResource> ApiResources =>
      new ApiResource[]
          {
              new ApiResource("cmosApi" , 
                  "CMOS Photo Api" ,
                  new [] {"role" , "country"})
              { 
                  Scopes = { "cmosApi.fullAccess" ,"cmosApi.write" , "cmosApi.read" },
                  ApiSecrets = { new Secret("apisecret".Sha256() ) }

              },

             
          };
    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
             new ApiScope("cmosApi.fullAccess"),
                new ApiScope("cmosApi.write"),
                new ApiScope("cmosApi.read")
            };

  
    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                new Client {
                    ClientName = "CMOSClient",
                    ClientId = "cmos",
                    AllowedGrantTypes = GrantTypes.Code,
                    AccessTokenType = AccessTokenType.Reference,
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh =true, // to refresth claims of a user
                    AccessTokenLifetime = 120,
                    
                    //AuthorizationCodeLifetime= ...
                    //IdentityTokenLifetime = ..,
                    RedirectUris = new List<string> { "https://localhost:7184/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7184/signout-callback-oidc" },
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile ,
                        "roles" ,
                        //"cmosApi.fullAccess" ,
                        "cmosApi.read",
                        "cmosApi.write",
                        "country" },
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    RequireConsent = true,

                }
            };
   }