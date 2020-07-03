using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models.AdminViewModels
{
    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public List<string> IdsToAdd { get; set; }
        public List<string> IdsToDelete { get; set; }

        public RoleModificationModel()
        {
            this.IdsToAdd = new List<string>();
            this.IdsToDelete = new List<string>();
        }
    }
}
