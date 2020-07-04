using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HotelSite.Models;
using HotelSite.Data;
using HotelSite.Models.HomeViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.EntityFrameworkCore;

namespace HotelSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationContext applicationContext,
            UserManager<ApplicationUser> userManager)
        {
            this._applicationContext = applicationContext;
            this._userManager = userManager;
        }

        public IActionResult Index(DateTime? arrival, ComfortLayer? comfort,
            int? days)
        {
            arrival ??= DateTime.Now.Date;
            ViewData["arrival"] = arrival.Value.ToString("yyyy-MM-dd");
            ViewData["comfort"] = comfort;
            ViewData["days"] = days;
            days ??= 0;

            var notIn = this._applicationContext.Reservations.Where(x =>
              (x.Arrival <= arrival && x.Arrival.AddDays(x.PlannedNumberOfDays) >= arrival
              ||
              x.Arrival >= arrival && arrival.Value.AddDays(days.Value) >= x.Arrival))
                .Select(x => x.RoomNumber).ToArray();


            var model = this._applicationContext.Rooms
                .Where(x => x.IsReady && (comfort == null || comfort == x.Comfort) && (!notIn.Contains(x.Number)))
                .Select(x => new RoomCardViewModel
                {
                    Number = x.Number,
                    Capacity = x.Capacity,
                    Comfort = x.Comfort,
                    Price = x.Price
                });
            return View(model);
        }

        public async Task<IActionResult> RoomDetails(byte roomNumber)
        {
            Room room = await this._applicationContext.Rooms.FindAsync(roomNumber);
            if (room != null)
            {
                await this._applicationContext.Entry(room).Collection(x => x.Photos).LoadAsync();
                var model = new RoomDetailsViewModel
                {
                    Number = room.Number,
                    Floor = room.Floor,
                    Capacity = room.Capacity,
                    Price = room.Price,
                    Comfort = room.Comfort,
                    Description = room.Description,
                    Photos = room.Photos
                };
                return View(model);
            }
            return View("Error");
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckInterval(DateTime arrival, int plannedNumberOfDays,
            byte roomNumber)
        {
            bool check=await this._applicationContext.Reservations.Where(x =>
            x.RoomNumber == roomNumber &&
            (x.Arrival <= arrival && x.Arrival.AddDays(x.PlannedNumberOfDays) >= arrival
            ||
            x.Arrival >= arrival && arrival.AddDays(plannedNumberOfDays) >= x.Arrival))
                .AnyAsync();
            if (check)
            {
                return Json($"В заданных временных промежутках номер уже занят.");
            }

            return Json(true);
        }

        [Authorize]
        public async Task<IActionResult> BookARoom(byte roomNumber)
        {
            Room room = await this._applicationContext.Rooms.FindAsync(roomNumber);
            if(room!=null)
            {
                var model = new CreateReservationViewModel
                {
                    RoomNumber = roomNumber,
                    PlannedNumberOfDays = 1,
                    Arrival = DateTime.Now.Date
                };
                return View(model);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> BookARoom(CreateReservationViewModel model)
        {
            if(ModelState.IsValid)
            {
                Reservation reservation = new Reservation
                {
                    RoomNumber = model.RoomNumber,
                    Arrival = model.Arrival,
                    PlannedNumberOfDays = model.PlannedNumberOfDays,
                    UserId = this._userManager.GetUserId(User)
                };

                await this._applicationContext.Reservations.AddAsync(reservation);
                await this._applicationContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Search(string searchString)
        {
            var result = this._applicationContext.Rooms
                .Where(x => x.IsReady && x.Description.Contains(searchString))
                .Select(x => new RoomCardViewModel
                {
                    Number = x.Number,
                    Capacity = x.Capacity,
                    Comfort = x.Comfort,
                    Price = x.Price
                });
            return View(result);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
