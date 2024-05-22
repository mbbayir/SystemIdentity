using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SystemIdentity.Core.ViewModels;
using SystemIdentity.Repository.Models;
using SystemIdentity.Core.ViewModels;

namespace SystemIdentity.Controllers
{
    public class RolesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
  
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;

            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleListModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return View(roles);
        }

        [Authorize(Roles ="Admin")]
        public IActionResult RoleCreated() {
            return View();

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RoleCreated(RoleCreatedViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var role = new AppRole { Name = request.Name };
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(RolesController.Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(request);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RoleUpdate(string id)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(id);
            if (roleToUpdate == null)
            {
                throw new Exception("Güncellenecek bir rol bulunamamıştır.");
            }

            var model = new RoleUpdateModel { Id = roleToUpdate.Id, Name = roleToUpdate.Name! };
            return View(model);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleUpdateModel request)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(request.Id);
            if (roleToUpdate == null)
            {
                throw new Exception("Güncellenecek rol bulunamamıştır.");
            }

            roleToUpdate.Name = request.Name;

            await _roleManager.UpdateAsync(roleToUpdate);
            ViewData["SuccessedMessage"] = "Rol Bilgisi Güncellenmiştir";
            return RedirectToAction(nameof(Index)); 
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RoleDelete(string id)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id);
            if ((roleToDelete == null))
            {
                throw new Exception("Silinecek rol bulunmadı");
            }

            var result = await _roleManager.DeleteAsync(roleToDelete);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x=>x.Description).First());
            }


            TempData["SuccessedMessage"] = "Rol Silinmiştir";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AssignRoleToUser(string id)
        {
            var currentUser = await _userManager.FindByIdAsync(id);
            if (currentUser == null)
            {
                return NotFound();
            }

            ViewBag.userId = id;
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var roleViewModelList = new List<AssignRoleToUserModel>();

            foreach (var role in roles)
            {
                var assignRoleToUserModel = new AssignRoleToUserModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    Exist = userRoles.Contains(role.Name)
                };
                roleViewModelList.Add(assignRoleToUserModel);
            }

            return View(roleViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string userId, List<AssignRoleToUserModel> requestList)
        {
            
            var userToAssignRoles = await _userManager.FindByIdAsync(userId);
            if (userToAssignRoles == null)
            {
                return NotFound();
            }

            foreach (var role in requestList)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(userToAssignRoles, role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(userToAssignRoles, role.Name);
                }
            }
            TempData["RoleMessage"] = "Üye Rol Eklmesi Başarıyla Eklendi!";
            return RedirectToAction(nameof(RolesController.GetUserList));
        }

        public async Task<IActionResult> GetUserList()
        {
            var users = await _userManager.Users.ToListAsync();
            var userModels = new List<UserModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userModel = new UserModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    UserName = user.UserName,
                    City = user.City,
                    Roles = roles
                };
                userModels.Add(userModel);
            }

            return View(userModels);
        }





    }
}
