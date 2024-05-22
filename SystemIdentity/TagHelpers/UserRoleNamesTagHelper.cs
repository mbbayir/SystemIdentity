using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SystemIdentity.Repository.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SystemIdentity.TagHelpers
{
    public class UserRoleNamesTagHelper : TagHelper
    {
        public string UserId { get; set; } = null!;

        private readonly UserManager<AppUser> _userManager;

        public UserRoleNamesTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            var userRoles= await _userManager.GetRolesAsync(user!);

            var stringBuilder=new StringBuilder();
            userRoles.ToList().ForEach(x =>
            {
                stringBuilder.Append(@$"<span class='badge text - bg - secondary'>{x.ToLower()}</span>");
            });

            output.Content.SetHtmlContent(stringBuilder.ToString());
        }
    }
    
      
    
}
