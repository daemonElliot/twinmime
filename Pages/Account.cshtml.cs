using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using TwinMIME.Data;

[Authorize]
public class AccountModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public string? Username { get; set; }
    public string? DisplayName { get; set; }
    public int Rating { get; set; }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            Username = user.UserName;
            DisplayName = user.DisplayName;
            Rating = user.Rating;
        }
        else
        {
            Username = "Неизвестный";
            DisplayName = "Неизвестный";
            Rating = 0;
        }
    }
}