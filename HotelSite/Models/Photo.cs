using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSite.Models
{
    [Table("Photos")]
    public class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public byte[] ByteArray { get; set; }

        public byte RoomNumber { get; set; }

        [ForeignKey("RoomNumber")]
        public Room Room { get; set; }
    }
}
