using System.ComponentModel.DataAnnotations;

namespace MusicBase.Web.Models.Songs
{
    public class UpdateSongViewModel
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The Song name length is invalid!")]
        public string Name { get; set; }

    }
}
