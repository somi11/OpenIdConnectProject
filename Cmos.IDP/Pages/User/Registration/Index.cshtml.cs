using Cmos.IDP.Services;
using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cmos.IDP.Pages.User.Registration
{
    [AllowAnonymous]
    [SecurityHeaders]
    public class IndexModel : PageModel

    {
        private readonly ILocalUserService _localUserService;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        private readonly EmailService _emailService;
        public IndexModel(ILocalUserService localUserService , IIdentityServerInteractionService identityServerInteractionService, EmailService emailService)
        {
            _localUserService = localUserService ?? throw new ArgumentNullException(nameof(_localUserService));
            _identityServerInteractionService = identityServerInteractionService ??
                throw new ArgumentNullException(nameof(_identityServerInteractionService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(_emailService));
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public IActionResult OnGet(string returnUrl)
        {
            BuildModel(returnUrl);
            return Page();
        }

        public void BuildModel(string returnUrl)
        {
            Input = new InputModel
            {
                ReturnUrl = returnUrl,

            };
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                BuildModel(Input.ReturnUrl);
                return Page();
            }

            var userToCreate = new Entities.User
            {
                //Password = Input.Password,
                UserName = Input.UserName,
                Subject = Guid.NewGuid().ToString(),
                Email = Input.Email,
                Active = false
            };

            userToCreate.Claims.Add(new Entities.UserClaim()
            {
                Type = JwtClaimTypes.GivenName,
                Value = Input.GivenName
            });

            userToCreate.Claims.Add(new Entities.UserClaim()
            {
                Type = JwtClaimTypes.FamilyName,
                Value = Input.FamilyName
            });

            userToCreate.Claims.Add(new Entities.UserClaim()
            {
                Type = "country",
                Value = Input.Country

            });
            _localUserService.AddUser(userToCreate , Input.Password);
           // await _localUserService.SaveChangesAsync();

            var activationLink = Url.
                PageLink("/user/activation/index", 
                values: new { securityCode = userToCreate.SecurityCode });
            
            await _emailService.SendEmailAsync(userToCreate.Email, "Account Activation" , activationLink);
            Console.WriteLine(activationLink);
            if(EmailService.isUserRegisterd)
            {
            return Redirect("~/User/ActivationCodeSent");
            } else
            {
                ModelState.AddModelError("Email", "We have checked your email. The email is invalid. Please enter a valid email address.");
                //return RedirectToPage();
                return Page();
                //   return Redirect("~/User/Registration");
            }
            //login

            //var isUser = new IdentityServerUser(userToCreate.Subject)
            //{
            //    DisplayName = userToCreate.UserName
            //};

            //await HttpContext.SignInAsync(isUser);

            //// continue with flow
            //if(_identityServerInteractionService.IsValidReturnUrl(Input.ReturnUrl)
            //    || Url.IsLocalUrl(Input.ReturnUrl))
            //{
            //    return Redirect(Input.ReturnUrl);
            //}

            //return Redirect("~/");
        }

    }
}
