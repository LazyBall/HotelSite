using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(70)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(70)]
        [Required]
        public string LastName { get; set; }

        public List<Rent> Rents { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}