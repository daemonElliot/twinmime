using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwinMIME.Data;

namespace yetNoName.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, 
                        SignInManager<ApplicationUser> signInManager, 
                        UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoginAsync(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                return RedirectToPage();
            }
            
            ModelState.AddModelError(string.Empty, "Неверные учетные данные.");
            return Page();
        }

        public async Task<IActionResult> OnPostRegisterAsync(string username, string email, string password, string confirmPassword, string displayName)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Пароли не совпадают.");
                return Page();
            }

            var user = new ApplicationUser { 
                UserName = username, 
                Email = email, 
                DisplayName = displayName,
                Rating = 0 
            };
            
            var result = await _userManager.CreateAsync(user, password);
            
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage();
            }
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage();
        }
    }
}