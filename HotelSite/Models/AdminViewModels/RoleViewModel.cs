using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models.AdminViewModels
{
    public class RoleViewModel
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; } // ID 

        [Required(ErrorMessage = "Пожалуйста введите название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; } // название 

        [Display(Name = "Список пользователей")]
        public IEnumerable<ApplicationUser> UsersInRole { get; set; }
    }
}
