using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models.UserViewModels
{
    public class UserViewModel
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}
