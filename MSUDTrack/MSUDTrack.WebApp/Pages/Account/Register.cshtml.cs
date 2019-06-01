using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSUDTrack.WebApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly TrackerDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public RegisterModel(
            TrackerDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [EmailAddress]
            [DataType(DataType.EmailAddress)]
            [Display(Prompt = "Optional")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Username, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (ModelState.ErrorCount < 1)
                    {
                        //if (_context.LicenseKeys.Count() < 1)
                        //{
                        //    var key = TTP.Licensing.Utils.CreateCandleTrialKey();
                        //    await _context.LicenseKeys.AddAsync(key);
                        //    user.LicenseKeyId = key.Id;
                        //}
                        //if (_context.Users.Count() < 2) //First user
                        //    await _userManager.AddToRoleAsync(user, Roles.Administrator);

                        if (ReturnUrl != "/setup/users")
                            await _signInManager.SignInAsync(user, isPersistent: false);

                        _logger.LogInformation("User created a new account with password.");
                        return LocalRedirect(Url.GetLocalUrl(returnUrl));
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}