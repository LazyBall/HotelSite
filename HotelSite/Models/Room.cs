using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSite.Models
{
    [Table("Rooms")]
    [Display(Name = "Гостиничный номер")]
    public class Room
    {
        [Key]
        [Display(Name = "Номер")]
        public byte Number { get; set; }

        [Required]
        [Display(Name = "Этаж")]
        public byte Floor { get; set; }

        [Required]
        [Display(Name = "Вместимость")]
        public byte Capacity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Display(Name = "Цена за один день")]
        public decimal Price { get; set; }

        [Required]
        [DefaultValue(false)]
        [Display(Name = "Готов к приему посетителей")]
        public bool IsReady { get; set; }

        [Required]
        [DefaultValue(false)]
        [Display(Name = "Заселен")]
        public bool IsBusy { get; set; }

        [Required]
        [Display(Name = "Уровень комфортности")]
        public ComfortLayer Comfort { get; set; }

        [Required]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Фотографии")]
        public List<Photo> Photos { get; set; }
    }

    public enum ComfortLayer : byte
    {
        [Display(Name = "Эконом класс")]
        Economy,

        [Display(Name = "Премиум класс")]
        Premium,

        [Display(Name = "Люкс класс")]
        Luxury,

        [Display(Name = "Президентский номер")]
        Presidential
    }

    public static class ComfortLayerExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList(this ComfortLayer comfort)
        {
            List<SelectListItem> selectItems = new List<SelectListItem>
            {
                new SelectListItem("Эконом класс",ComfortLayer.Economy.ToString(),
                ComfortLayer.Economy==comfort),
                new SelectListItem("Премиум класс",ComfortLayer.Premium.ToString(),
                ComfortLayer.Premium==comfort),
                new SelectListItem("Люкс класс",ComfortLayer.Luxury.ToString(),
                ComfortLayer.Luxury==comfort),
                new SelectListItem("Президентский номер",ComfortLayer.Presidential.ToString(),
                ComfortLayer.Presidential==comfort),
            };

            return selectItems;
        }
    }
}
