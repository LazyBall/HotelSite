using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models.HomeViewModels
{
    public class RoomDetailsViewModel
    {
        [Display(Name = "Номер")]
        public byte Number { get; set; }

        [Display(Name = "Этаж")]
        public byte Floor { get; set; }

        [Display(Name = "Вместимость")]
        public byte Capacity { get; set; }

        [Display(Name = "Цена за один день")]
        public decimal Price { get; set; }

        [Display(Name = "Уровень комфортности")]
        public ComfortLayer Comfort { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Фотографии")]
        public List<Photo> Photos { get; set; }
    }
}
