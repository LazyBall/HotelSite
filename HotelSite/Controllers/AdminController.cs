using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelSite.Models;
using HotelSite.Models.AdminViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelSite.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Roles()
        {
            var result = new List<RoleViewModel>();
            foreach (IdentityRole role in this._roleManager.Roles)
            {
                var roleView = new RoleViewModel
                {
                    Name = role.Name,
                    Id = role.Id,
                    UsersInRole =
                        await this._userManager.GetUsersInRoleAsync(role.Name)
                };
                result.Add(roleView);
            }

            return View(result);
        }

        public async Task<IActionResult> EditListOfUsers(string id)
        {
            IdentityRole role = await this._roleManager.FindByIdAsync(id);
            var users = this._userManager.Users.ToList();
            if (role != null)
            {
                IEnumerable<ApplicationUser> members, nonMembers;
                members = await this._userManager.GetUsersInRoleAsync(role.Name);
                nonMembers = users.Except(members);

                return View(new RoleEditModel
                {
                    Role = role,
                    Members = members,
                    NonMembers = nonMembers
                });
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditListOfUsers(RoleModificationModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd)
                {
                    ApplicationUser user = await this._userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        IdentityResult result = await
                            this._userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            return View("Error");
                        }
                    }
                    else
                    {
                        return View("Error");
                    }
                }

                foreach (string userId in model.IdsToDelete)
                {
                    ApplicationUser user = await this._userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        IdentityResult result = await
                            this._userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            return View("Error");
                        }
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                return RedirectToAction(nameof(Roles));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public IActionResult Users()
        {
            return View(from x in _userManager.Users
                        select new UserViewModel()
                        {
                            Id = x.Id,
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName
                        });
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Users));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        // GET: UsersController/EditUser/5
        public async Task<IActionResult> EditUser(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(model);
        }

        // POST: UsersController/EditUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Users));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            return View(model);
        }

        // POST: UsersController/DeleteUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return RedirectToAction(nameof(Users));
        }
    }
}
