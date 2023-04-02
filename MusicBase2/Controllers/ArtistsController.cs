using Microsoft.AspNetCore.Mvc;
using MusicBase.Repositories.Dtos;
using MusicBase.Repositories.Artists;
using MusicBase.Web.Models.Artists;
using MusicBase.Entities;
using MusicBase.Web.Models.Songs;

namespace MusicBase.Web.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMusicUnitOfWork _unitOfWork;

        public ArtistsController(IArtistRepository artistRepository, IMusicUnitOfWork musicUnitOfWork)
        {
            _artistRepository = artistRepository;
            _unitOfWork = musicUnitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var artistDtos = _artistRepository.GetAll();

            var artistViewModels = artistDtos.Select(s => new ArtistViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Genre = s.Genre,
            });

            return View(artistViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] AddArtistViewModel addArtistViewModel)
        {
            if (addArtistViewModel == null)
            {
                return RedirectToAction("Error", new { message = "AddArtistViewModel is null!" });
            }

            if (!ModelState.IsValid)
            {
                return View(addArtistViewModel);
            }

            var artistDto = new ArtistDto
            {
                Name = addArtistViewModel.Name,
                Genre = addArtistViewModel.Genre,
            };

            _artistRepository.AddArtist(artistDto);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            if (id < 1)
            {
                return RedirectToAction("Error", new { message = "Artist Id is negative!" });
            }

            var artistDto = _artistRepository.GetArtistById(id);
            if (artistDto == null)
            {
                return RedirectToAction("Error", new { message = "Artist not found!" });
            }

            var updateArtistViewModel = new UpdateArtistViewModel
            {
                Name = artistDto.Name,
                Genre = artistDto.Genre,
            };

            return View(updateArtistViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] UpdateArtistViewModel updateArtistViewModel)
        {
            if (updateArtistViewModel == null)
            {
                return RedirectToAction("Error", new { message = "UpdateArtistViewModel is null!" });
            }

            if (!ModelState.IsValid)
            {
                return View(updateArtistViewModel);
            }

            var artistDto = _artistRepository.GetArtistById(updateArtistViewModel.Id);
            if (artistDto == null)
            {
                return RedirectToAction("Error", new { message = "Artist not found!" });
            }

            artistDto.Name = updateArtistViewModel.Name;
            artistDto.Genre = updateArtistViewModel.Genre;

            _artistRepository.UpdateArtist(artistDto);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

      

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            if (id < 1)
            {
                return RedirectToAction("Error", new { message = "Artist Id is negative!" });
            }

            var artistDto = _artistRepository.GetArtistById(id);
            if (artistDto == null)
            {
                return RedirectToAction("Error", new { message = "Artist not found!" });
            }

            var ArtistViewModel = new ArtistViewModel
            {
                Id = id,
                Name = artistDto.Name,
                Genre = artistDto.Genre,
            };

            return View(ArtistViewModel);
        }
        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            if (id < 1)
            {
                return RedirectToAction("Error", new { message = "Artist Id is negative!" });
            }

            var artistDto = _artistRepository.GetArtistById(id);
            if (artistDto == null)
            {
                return RedirectToAction("Error", new { message = "Artist not found!" });
            }

            var artistViewModel = new ArtistViewModel
            {
                Id = id,
                Name = artistDto.Name,
                Genre = artistDto.Genre,
            };

            return View(artistViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            if (id < 1)
            {
                return RedirectToAction("Error", new { message = "Artist Id is negative!" });
            }

            _artistRepository.RemoveArtist(id);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
