using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Helpers.Interfaces;
using xilopro2.Models;

namespace xilopro2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IUserHelper userHelper, UserManager<AppUser> userManager, DataContext context)
        {
            _logger = logger;
            _userHelper = userHelper;
            _userManager = userManager;
            _dataContext = context;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                int totalEntity1 = _dataContext.Users.Count();
                int totalEntity2 = _dataContext.Players.Count();
                int totalEntity3 = _dataContext.Torneos.Count();
                int totalEntity4 = _dataContext.Parents.Count();
                var viewModel = new DashboardViewModel
                {
                    TotalPlayers = totalEntity2,
                    TotalParents = totalEntity4,
                    TotalTorneos = totalEntity3,
                    TotalUsers = totalEntity1
                };
                return View(viewModel);
            }
            return RedirectToAction("Login", "Account");
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

        public IActionResult _Nav()
        {
            var usuarioActual = _userManager.GetUserAsync(User);
            ViewBag.ProfileImage = usuarioActual.Result.User_Image;
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
