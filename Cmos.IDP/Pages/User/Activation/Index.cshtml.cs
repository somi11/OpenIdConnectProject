using Cmos.IDP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cmos.IDP.Pages.User.Activation
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        public readonly ILocalUserService _localUserService;

        public IndexModel(ILocalUserService localUserService)
        {
            _localUserService = localUserService ?? throw new ArgumentNullException(nameof(_localUserService));
            Input = new InputModel();
        }
        [BindProperty]
        public InputModel Input { get; set;  }
        public  async  Task<IActionResult> OnGet(string securityCode)
        {
            if (await _localUserService.ActivateUserAsync(securityCode))
            {
                Input.Message = "Your account has been activated. You can now log in.";
            } else
            {
                Input.Message = "Your account could not be activated. Please try again.";
            }

            await _localUserService.SaveChangesAsync();
            return Page();
        }
    }
}
     