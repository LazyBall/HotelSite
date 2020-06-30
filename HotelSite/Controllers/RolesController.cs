using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelSite.Models;
using HotelSite.Models.RoleViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelSite.Controllers
{
    [Authorize(Roles ="admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
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

        public async Task<ActionResult> EditUsersList(string id)
        {
            IdentityRole role = await this._roleManager.FindByIdAsync(id);
            var users = this._userManager.Users;
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
        public async Task<ActionResult> EditUsersList(RoleModificationModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd)
                {
                    ApplicationUser user = await this._userManager.FindByIdAsync(userId);
                    if(user!=null)
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
                return RedirectToAction("Index");

            }
            return View("Error");
        }
    }
}
