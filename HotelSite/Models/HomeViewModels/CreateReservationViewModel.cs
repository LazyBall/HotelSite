using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models.HomeViewModels
{
    public class CreateReservationViewModel
    {
        [Remote(action:"CheckInterval", controller:"Home",
            AdditionalFields = nameof(PlannedNumberOfDays) + "," + nameof(RoomNumber))]
        [Display(Name = "Дата заселения")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Arrival { get; set; }

        [Remote(action: "CheckInterval", controller: "Home",
            AdditionalFields = nameof(Arrival) + "," + nameof(RoomNumber))]
        [Display(Name = "Срок поселения")]
        [Required]
        [Range(1, 365, ErrorMessage = "Недопустимое значение")]
        public int PlannedNumberOfDays { get; set; }

        [HiddenInput(DisplayValue = false)]
        public byte RoomNumber { get; set; }
    }
}
