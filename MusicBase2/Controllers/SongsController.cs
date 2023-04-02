using Microsoft.AspNetCore.Mvc;
using MusicBase.Repositories.Dtos;
using MusicBase.Web.Models.User;
using MusicBase.Web.Models.Songs;
using MusicBase.Entities;
using MusicBase.Repositories.Songs;
using MusicBase.Repositories.Artists;
using MusicBase.Repositories.Users;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MusicBase.Web.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongRepository _songRepository;
        private readonly IMusicUnitOfWork _musicUnitOfWork;
        private readonly IArtistRepository _artistRepository;
        private readonly IUserRepository _userRepository;

        public SongsController(ISongRepository songRepository, IMusicUnitOfWork musicUnitOfWork, IArtistRepository artistRepository, IUserRepository userRepository)
        {
            _songRepository = songRepository;
            _musicUnitOfWork = musicUnitOfWork;
            _artistRepository = artistRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var songDtos = _songRepository.GetAll();

            var userViewModels = songDtos.Select(s => new SongViewModel
            {
                Id = s.Id,
                Name = s.Name,
                ArtistName = _artistRepository.GetArtistName(s.ArtistId)
            });

            return View(userViewModels);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] AddSongViewModel addSongViewModel)
        {
            if (addSongViewModel == null)
            {
                return RedirectToAction("Error", new { message = "AddSongViewModel is null!" });
            }

            if (!ModelState.IsValid)
            {
                return View(addSongViewModel);
            }

            if (_artistRepository.GetArtistId(addSongViewModel.ArtistName) == 0)
            {
                return RedirectToAction("Error", new { message = "Artist is no added!" });
            }

            var songDto = new SongDto
            {
                Name = addSongViewModel.Name,
                ArtistId = _artistRepository.GetArtistId(addSongViewModel.ArtistName),
            };

            _songRepository.AddSong(songDto);
            _musicUnitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            if (id < 1)
            {
                return RedirectToAction("Error", new { message = "Song Id is negative!" });
            }

            var userDto = _songRepository.GetSong(id);
            if (userDto == null)
            {
                return RedirectToAction("Error", new { message = "User not found!" });
            }

            var updateSongViewModel = new UpdateSongViewModel
            {
                Name = userDto.Name,
            };

            return View(updateSongViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] UpdateSongViewModel updateSongViewModel)
        {
            if (updateSongViewModel == null)
            {
                return RedirectToAction("Error", new { message = "UpdateSongViewModel is null!" });
            }

            if (!ModelState.IsValid)
            {
                return View(updateSongViewModel);
            }

            var songDto = _songRepository.GetSong(updateSongViewModel.Id);
            if (songDto == null)
            {
                return RedirectToAction("Error", new { message = "Song not found!" });
            }

            songDto.Name = updateSongViewModel.Name;

            _songRepository.UpdateSong(songDto);
            _musicUnitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            if (id < 1)
            {
                return RedirectToAction("Error", new { message = "Song Id is negative!" });
            }

            var songDto = _songRepository.GetSong(id);
            if (songDto == null)
            {
                return RedirectToAction("Error", new { message = "Song not found!" });
            }

            var songViewModel = new SongViewModel
            {
                Id = id,
                Name = songDto.Name,
                ArtistName = _artistRepository.GetArtistName(songDto.ArtistId),
            };

            return View(songViewModel);
        }
        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            if (id < 1)
            {
                return RedirectToAction("Error", new { message = "User Id is negative!" });
            }

            var songDto = _songRepository.GetSong(id);
            if (songDto == null)
            {
                return RedirectToAction("Error", new { message = "User not found!" });
            }

            var songViewModel = new SongViewModel
            {
                Id = id,
                Name = songDto.Name,
                ArtistName = _artistRepository.GetArtistName(songDto.ArtistId),
            };

            return View(songViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            if (id < 1)
            {
                return RedirectToAction("Error", new { message = "Song Id is negative!" });
            }

            _songRepository.DeleteSong(id);
            _musicUnitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        
        public IActionResult Like([FromRoute] int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userId = _userRepository.GetUserIdFromEmail(userEmail);
            _userRepository.AddSongs(userId, id);
            _musicUnitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
