using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSite.Models
{
    [Table("Reservations")]
    [Display(Name ="Бронь")]
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Дата заселения")]
        [Column(TypeName = "date")]
        [Required]
        public DateTime Arrival { get; set; }

        [Display(Name = "Срок поселения")]
        [Required]
        [Range(1, 365, ErrorMessage = "Недопустимое значение")]
        [Column(TypeName = "smallint")]
        public int PlannedNumberOfDays { get; set; }

        public byte RoomNumber { get; set; }

        [ForeignKey("RoomNumber")]
        public Room Room { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}