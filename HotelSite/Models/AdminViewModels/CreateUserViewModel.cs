using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models.AdminViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Укажите email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [StringLength(maximumLength: 100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Введите пароль еще раз")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(70)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        [MaxLength(70)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}
