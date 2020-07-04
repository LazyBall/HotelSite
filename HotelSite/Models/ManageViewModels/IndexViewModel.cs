using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSite.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name ="Логин")]
        public string UserName { get; set; }

        [Display(Name ="Имя")]
        public string FirstName { get; set; }

        [Display(Name ="Фамилия")]
        public string LastName { get; set; }
    }
}
