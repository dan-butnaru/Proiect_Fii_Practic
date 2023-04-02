using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicBase.Repositories.Dtos;
using MusicBase.Services;
using MusicBase.Web.Models.Account;
using MusicBase;
using MusicBase.Web.Models.User;
using System.Net;
using System.Security.Claims;
using MusicBase.Repositories.Users;

namespace FiiPracticCars.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICryptographyService _cryptographyService;
        private readonly IUserRepository _userRepository;
        private readonly IMusicUnitOfWork _musicUnitOfWork;

        public AccountController(
          ICryptographyService cryptographyService
          , IUserRepository userRepository
          , IMusicUnitOfWork musicUnitOfWork)
        {
            _cryptographyService = cryptographyService;
            _userRepository = userRepository;
            _musicUnitOfWork = musicUnitOfWork;
        }

        public IActionResult Index()
        {
            var userDtos = _userRepository.GetAll();

            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (HttpContext.User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index");
            }
            var user = _userRepository.GetUserByEmail(model.Email);
            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ViewBag.ErrorMessage = "Could not find user";
                return View();
            }

            var hash = _cryptographyService.HashPassword(model.Password, user.PasswordSalt);
            if (user.PasswordHash != hash.Hash)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ViewBag.ErrorMessage = "Could not find user";
                return View();
            }

            await SignInAsync(model.Email);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User.Identity?.IsAuthenticated == false)
            {
                return RedirectToAction(nameof(Index));
            }
            await HttpContext!.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserViewModel registerModel)
        {
            var hash = _cryptographyService.HashPasswordWithSaltGeneration(registerModel.Password);
            _userRepository.CreateUser(new UserDto
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Email = registerModel.Email,
                PasswordHash = hash.Hash,
                PasswordSalt = hash.Salt
            });
            _musicUnitOfWork.SaveChanges();

            await SignInAsync(registerModel.Email);
            return RedirectToAction(nameof(Index));
        }

        private async Task SignInAsync(string email)
        {
            var claims = new List<Claim>
      {
        new Claim(ClaimsIdentity.DefaultNameClaimType, email)
      };
            var identity = new ClaimsIdentity(claims, AuthMusicConstants.Schema);
            var user = new ClaimsPrincipal(identity);
            await HttpContext!.SignInAsync(user);
        }
    }
}
