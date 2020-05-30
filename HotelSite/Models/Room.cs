using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSite.Models
{
    [Table("Rooms")]
    [Display(Name ="Гостиничный номер")]
    public class Room
    {
        [Display(Name = "Номер помещения")]
        [Key]
        public byte Number { get; set; }

        [Display(Name = "Этаж")]
        [Required]
        public byte Floor { get; set; }

        [Display(Name = "Вместимость")]
        [Required]
        public byte Capacity { get; set; }

        [Display(Name = "Цена")]
        [Required]
        [Column(TypeName = "money")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Недопустимое значение")]
        public decimal Price { get; set; }

        [Display(Name = "Занятость на данный момент")]
        [Required]
        [DefaultValue(false)]
        public bool IsBusy { get; set; }

        [Display(Name = "Комфортность")]
        [Required]
        public ComfortLayer Comfort { get; set; }       
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
}