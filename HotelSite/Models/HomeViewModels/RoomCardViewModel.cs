using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models.HomeViewModels
{
    public class RoomCardViewModel
    {
        [Display(Name = "Номер")]
        public byte Number { get; set; }

        [Display(Name = "Вместимость")]
        public byte Capacity { get; set; }

        [Display(Name = "Цена за один день")]
        public decimal Price { get; set; }

        [Display(Name = "Уровень комфортности")]
        public ComfortLayer Comfort { get; set; }
    }
}
