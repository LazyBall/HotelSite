using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSite.Models
{
    [Table ("Rents")]
    [Display (Name = "Съем")]
    public class Rent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display (Name ="Дата заселения")]
        [Column (TypeName ="date")]
        [Required]
        public DateTime Arrival { get; set; }

        [Display(Name ="Срок поселения")]
        [Required]
        [Range(1,365, ErrorMessage ="Недопустимое значение")]
        [Column(TypeName ="smallint")]
        public int PlannedNumberOfDays { get; set; }

        [Display(Name ="Фактическая дата выезда")]
        [Column(TypeName = "date")]
        [DefaultValue(null)]
        public DateTime Departure { get; set; }

        public byte RoomNumber { get; set; }

        [ForeignKey("RoomNumber")]
        public Room Room { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}