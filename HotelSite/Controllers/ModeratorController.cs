using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HotelSite.Data;
using HotelSite.Models;
using HotelSite.Models.ModeratorViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelSite.Controllers
{
    public class ModeratorController : Controller
    {
        private readonly ApplicationContext _applicationContext;

        public ModeratorController(ApplicationContext applicationContext)
        {
            this._applicationContext = applicationContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckNumber(byte number)
        {
            if (await this._applicationContext.Rooms.FindAsync(number) != null)
                return Json(false);
            return Json(true);
        }

        public IActionResult Rooms()
        {
            return View(this._applicationContext.Rooms.Select(
                x => new RoomViewModel
                { Number = x.Number, IsBusy = x.IsBusy }
                ));
        }

        public IActionResult CreateRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                Room newRoom = new Room
                {
                    Number = model.Number,
                    Floor = model.Floor,
                    Capacity = model.Capacity,
                    Price = model.Price,
                    IsReady = model.IsReady,
                    Comfort = model.Comfort,
                    Description = model.Description ?? string.Empty
                };
                await this._applicationContext.Rooms.AddAsync(newRoom);
                await this._applicationContext.SaveChangesAsync();
                await this.UploadAsync(model.Photos, model.Number);
                RedirectToAction(nameof(Rooms));
            }

            return View(model);
        }

        public async Task<IActionResult> RoomDetails(byte roomNumber)
        {
            Room room = await this._applicationContext.Rooms.FindAsync(roomNumber);
            if (room != null)
            {
                await this._applicationContext.Entry(room).Collection(x => x.Photos).LoadAsync();
                return View(room);
            }
            return View("Error");
        }

        public async Task<IActionResult> EditRoom(byte roomNumber)
        {
            Room room = await this._applicationContext.Rooms.FindAsync(roomNumber);
            
            if (room != null)
            {
                EditRoomViewModel model = new EditRoomViewModel
                {
                    OldNumber=room.Number,
                    Number=room.Number,
                    Floor=room.Floor,
                    Capacity=room.Capacity,
                    Price=room.Price,
                    IsReady=room.IsReady,
                    IsBusy=room.IsBusy,
                    Comfort=room.Comfort,
                    Description=room.Description
                };
                return View(model);            
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> EditRoom(EditRoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                Room editRoom = await this._applicationContext.Rooms.FindAsync(model.OldNumber);
                if (editRoom != null)
                {
                    editRoom.Floor = model.Floor;
                    editRoom.Capacity = model.Capacity;
                    editRoom.Price = model.Price;
                    editRoom.IsReady = model.IsReady;
                    editRoom.IsBusy = model.IsBusy;
                    editRoom.Comfort = model.Comfort;
                    editRoom.Description = model.Description;
                    this._applicationContext.Rooms.Update(editRoom);
                    await this._applicationContext.SaveChangesAsync();
                    await this._applicationContext.Database.ExecuteSqlRawAsync(
                        "UPDATE Rooms SET Number={0} WHERE Number={1}",
                        model.Number, model.OldNumber);
                    return RedirectToAction(nameof(Rooms));
                }
                else
                {
                    return View("Error");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> EditRoomPhotos(byte roomNumber)
        {
            bool isRoomNumber = await
                this._applicationContext.Rooms.AnyAsync(x => x.Number == roomNumber);
            var model = new RoomPhotosViewModel
            {
                RoomNumber = roomNumber,
                Photos = this._applicationContext.Photos.Where(x => x.RoomNumber == roomNumber)
            };
            if (isRoomNumber)
            {
                return View(model);
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> EditRoomPhotos(EditRoomPhotosViewModel model)
        {
            if(ModelState.IsValid)
            {
                await UploadAsync(model.PhotosToAdd, model.RoomNumber);
                this._applicationContext.Photos.RemoveRange(
                    this._applicationContext.Photos.Where(
                        x => model.IdsToDelete.Contains(x.Id)));
                await this._applicationContext.SaveChangesAsync();
                return RedirectToAction(nameof(Rooms));
            }

            return View("Error");
        }

        #region Helpers

        private async Task UploadAsync(IFormFileCollection photos, byte roomNumber)
        {
            if (photos != null)
            {
                foreach (var uploadedPhoto in photos)
                {
                    if (uploadedPhoto != null)
                    {
                        byte[] imageData = null;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (Stream source = uploadedPhoto.OpenReadStream())
                            {
                                source.Position = 0;
                                source.CopyTo(ms);
                            }
                            imageData = ms.ToArray();
                        }
                        await this._applicationContext.Photos.AddAsync(new Photo
                        {
                            ByteArray = imageData,
                            RoomNumber = roomNumber
                        });
                    }
                }
                await this._applicationContext.SaveChangesAsync();
            }
        }
        #endregion
    }
}
