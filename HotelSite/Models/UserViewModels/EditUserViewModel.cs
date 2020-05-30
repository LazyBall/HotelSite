using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models.UserViewModels
{
    public class EditUserViewModel
    {
        [Required]
        [ScaffoldColumn(true)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Укажите email")]
        [EmailAddress]
        [Display(Name = "Новый Email пользователя")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(70)]
        [Display(Name = "Новое имя пользователя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        [MaxLength(70)]
        [Display(Name = "Новая фамилия пользователя")]
        public string LastName { get; set; }
    }
}
