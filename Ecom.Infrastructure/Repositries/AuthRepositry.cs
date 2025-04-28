using Ecom.Core.DTO;
using Ecom.Core.Entites;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositries
{
    public class AuthRepositry:IAuth
    {
        private readonly UserManager<AppUser>_userManager;
        public AuthRepositry(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
         
        public async Task<string> RegisterAsync(RegisterDTO registerDTO)
        {
            if(registerDTO == null)
            {
                return null;
            }
            if (await _userManager.FindByNameAsync(registerDTO.UserName) is not null) {
                return "this UserName already registerd";
            }
            if(await _userManager.FindByEmailAsync(registerDTO.Email) is not null)
            {
                return "this Email already registerd";
            }
            AppUser user = new AppUser()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
            };
            var result = await _userManager.CreateAsync(user,registerDTO.Password);
            if(result.Succeeded is not true)
            {
                return result.Errors.ToList()[0].Description;
            }
            // Send Email Comferm
            return "Done";
        }

    }
}
