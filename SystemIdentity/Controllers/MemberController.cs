using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemIdentity.Repository.Models;
using SystemIdentity.Core.ViewModels;
using SystemIdentity.Service;
using Azure.Core;
namespace SystemIdentity.Controllers
{

    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMemberService _memberService;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMemberService memberService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _memberService = memberService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        

        public IActionResult PasswordChange()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(string userName, PasswordChangeModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!await _memberService.CheckPasswordAsync(userName, request.OldPassword))
            {
                ModelState.AddModelError(string.Empty, "Eski Şifreniz Yanlış");
                return View();
            }
            var (isSuccess, errors) = await _memberService.ChangePasswordAsync(userName, request.OldPassword, request.PasswordNew);

            if (!isSuccess)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }

            TempData["SuccessedMessage"] = "Şifreniz Başarıyla Değiştirilmiştir.";

            return View();
        }
    

        public async Task<IActionResult> AccessDenied(string returnUrl)
        {
            string message =string.Empty;
            message = "Bu sayfayı görmeye yetkiniz yoktur Sayfa Yetkilisi ile görüşünüz!";
            return View();
        }
    }
}
