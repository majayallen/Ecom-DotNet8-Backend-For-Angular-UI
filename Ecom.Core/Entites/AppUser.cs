using Microsoft.AspNetCore.Identity;

namespace Ecom.Core.Entites
{
    public class AppUser:IdentityUser
    {
        public string DispalyName { get; set; }
        public Address Address { get; set; }
    }
}
