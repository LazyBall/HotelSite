using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models.ModeratorViewModels
{
    [Display(Name = "Гостиничный номер")]
    public class CreateRoomViewModel
    {
        [Display(Name = "Номер")]   
        [Required(ErrorMessage ="Укажите номер")]
        [Range(1, byte.MaxValue, ErrorMessage = "Недопустимое значение")]
        [Remote(controller: "Moderator", action: "CheckNumber", 
            ErrorMessage = "Этот номер уже используется")]
        public byte Number { get; set; }

        [Display(Name = "Этаж")]
        [Required(ErrorMessage ="Укажите этаж")]
        [Range(1, byte.MaxValue, ErrorMessage = "Недопустимое значение")]     
        public byte Floor { get; set; }

        [Display(Name = "Вместимость")]
        [Required(ErrorMessage ="Укажите вместимость")]
        [Range(1, byte.MaxValue, ErrorMessage = "Недопустимое значение")] 
        public byte Capacity { get; set; }

        [Display(Name = "Цена за один день")]
        [Required(ErrorMessage ="Укажите цену")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Недопустимое значение")]
        public decimal Price { get; set; }

        [Display(Name = "Готов к приему посетителей")]
        [Required(ErrorMessage ="Укажите, доступен ли номер для съема и бронирования")]
        [DefaultValue(false)]
        public bool IsReady { get; set; }

        [Display(Name = "Комфортность")]
        [Required(ErrorMessage ="Укажите уровень комфорта")]
        [DefaultValue(ComfortLayer.Economy)]
        public ComfortLayer Comfort { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Фотографии номера")]
        public IFormFileCollection Photos { get; set; }
    }
}
