using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.API.Authorzation
{
    public class MustOwnImageRequirement : IAuthorizationRequirement
    {
        public MustOwnImageRequirement()
        {
        }
    }
}
