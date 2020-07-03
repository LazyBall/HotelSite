using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models.ModeratorViewModels
{
    public class EditRoomViewModel
    {
        [ScaffoldColumn(false)]
        public byte OldNumber { get; set; }

        [Required]
        [Display(Name = "Номер")]
        public byte Number { get; set; }

        [Required]
        [Display(Name = "Этаж")]
        public byte Floor { get; set; }

        [Required]
        [Display(Name = "Вместимость")]
        public byte Capacity { get; set; }

        [Required]
        [Display(Name = "Цена за один день")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Готов к приему посетителей")]
        public bool IsReady { get; set; }

        [Required]
        [Display(Name = "Заселен")]
        public bool IsBusy { get; set; }

        [Required]
        [Display(Name = "Уровень комфортности")]
        public ComfortLayer Comfort { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
