using Microsoft.AspNetCore.Authorization;

namespace CmosGalleryApi.Authorization
{
    public static class AuthorizationPolicies
    {

        public static AuthorizationPolicy CanAddImage()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("country", "be") // add multiple claims here to list
                .RequireRole("PayingUser") // add multiple roles here to list
                .Build();
        }
    }
}
