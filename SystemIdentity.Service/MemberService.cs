using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIdentity.Repository.Models;

namespace SystemIdentity.Service
{
    public class MemberService : IMemberService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public MemberService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CheckPasswordAsync(string password, string userName)
        {
            var currentUser = await _userManager.FindByNameAsync(userName);
            if (currentUser == null)
                return false;

            return await _userManager.CheckPasswordAsync(currentUser, password);
        }

        public async Task<(bool, IEnumerable<IdentityError>?)> ChangePasswordAsync(string userName, string OldPassword, string PasswordNew)
        {
            var currentUser = await _userManager.FindByNameAsync(userName);

            var resultChangePassword = await _userManager.ChangePasswordAsync(currentUser, OldPassword, PasswordNew);
            if (!resultChangePassword.Succeeded)
            {
                return (false, resultChangePassword.Errors);
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, PasswordNew, true, false);

            return (true, null);
        }
    }

}
