using HotelSite.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSite.Data
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Rent> Rents { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}