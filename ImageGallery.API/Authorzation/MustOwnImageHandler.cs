using ImageGallery.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.API.Authorzation
{
    public class MustOwnImageHandler : AuthorizationHandler<MustOwnImageRequirement>
    {  
        IHttpContextAccessor _httpContextAccessor;
        IGalleryRepository _galleryRepository;
        public MustOwnImageHandler(IHttpContextAccessor httpContextAccessor ,   IGalleryRepository galleryRepository) { 
         _httpContextAccessor = httpContextAccessor 
                ?? throw new ArgumentException(nameof(httpContextAccessor));

        _galleryRepository = galleryRepository ?? throw new ArgumentException(nameof(galleryRepository));
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MustOwnImageRequirement requirement)
        {
            var imageId = _httpContextAccessor.HttpContext?
                .GetRouteValue("id")?.ToString();

            Console.WriteLine($"This is Image id from url: {imageId}");

            if (!Guid.TryParse(imageId , out Guid imageIdAsGuid))
            {
                context.Fail();
                return;
            }
                Console.WriteLine($"This is image as guid id from url: {imageIdAsGuid}");

            var ownerId = context.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            Console.WriteLine($"This is owner id from claim: {ownerId}");
            if (ownerId == null)
            {  

                context.Fail();
                return;
            }

            if(!await _galleryRepository.IsImageOwnerAsync(imageIdAsGuid , ownerId))
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
    }
}
