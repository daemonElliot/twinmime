using Microsoft.AspNetCore.Identity;

namespace TwinMIME.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int Rating { get; set; } = 0;
        public string DisplayName { get; set; } = string.Empty;
    }
}