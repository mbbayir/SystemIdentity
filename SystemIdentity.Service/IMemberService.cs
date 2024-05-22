using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIdentity.Service
{
    public interface IMemberService
    {
        Task<bool> CheckPasswordAsync(string userName, string password);
        Task<(bool, IEnumerable<IdentityError>?)> ChangePasswordAsync(string userName, string OldPassword, string PasswordNew);
    }

}
