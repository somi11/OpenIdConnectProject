using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.API.Authorzation
{
    public class MustOwnImageAttribute: AuthorizeAttribute, IAuthorizationRequirementData
    {

        public IEnumerable<IAuthorizationRequirement> GetRequirements()
        {
            return new[] { new MustOwnImageRequirement() };
        }
    }
     
 
}
 