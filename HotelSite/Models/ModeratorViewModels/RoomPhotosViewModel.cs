using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelSite.Models.ModeratorViewModels
{
    public class RoomPhotosViewModel
    {
        [ScaffoldColumn(false)]
        public byte RoomNumber { get; set; }

        [Display(Name = "Фотография")]
        public IEnumerable<Photo> Photos { get; set; }
    }

    public class EditRoomPhotosViewModel
    {
        public byte RoomNumber { get; set; }
        public List<int> IdsToDelete { get; set; }

        public IFormFileCollection PhotosToAdd { get; set; }

        public EditRoomPhotosViewModel()
        {
            this.IdsToDelete = new List<int>();
        }
    }
}
