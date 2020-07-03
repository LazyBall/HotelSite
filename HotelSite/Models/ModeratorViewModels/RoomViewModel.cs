using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models.ModeratorViewModels
{
    [Display(Name = "Гостиничный номер")]
    public class RoomViewModel
    {
        [Display(Name = "Номер")]
        public byte Number { get; set; }

        [Display(Name = "Занят на данный момент")]
        public bool IsBusy { get; set; }

        [Display(Name = "Готов к приему посетителей")]
        public bool IsReady { get; set; }
    }
}
