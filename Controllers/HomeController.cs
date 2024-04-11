using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Helpers.Interfaces;
using xilopro2.Models;
using xilopro2.Models.toCharts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
           // jugadoresxCat();
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
                    TotalUsers = totalEntity1 - 1,
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
        public IActionResult golesxTorneo() {

            List<GolesxtorneoViewModel> Lista = _dataContext.Torneos
                .Include(torneo => torneo.Groups)
                    .ThenInclude(grupo => grupo.GroupDetails)
                        .ThenInclude(detalle => detalle.Team) 
                        .AsEnumerable()
                .Where(torneo => torneo.SelectedCategoryIds.Contains(1)) 
                .Select(torneo => new GolesxtorneoViewModel
                {
                    NombreTorneo = torneo.Torneo_Name,
                    Temporada = torneo.Torneo_Season,
                    CantidadGoles = torneo.Groups
                        .SelectMany(grupo => grupo.GroupDetails)
                        .Where(df=>df.teamId == 1)
                        .Sum(detalle => detalle.GoalsFor)
                })
                .ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }


        public IActionResult jugadoresxCat()
        {
            var categoryPlayersCount = _dataContext.Players
            .AsEnumerable() // AsEnumerable aquí para manipular objetos en memoria, dependiendo de la lógica necesaria.
            .SelectMany(player => player.SelectedCategoryIds.Select(categoryId => new { CategoryId = categoryId }))
            .GroupBy(cp => cp.CategoryId)
            .Select(g => new { CategoryId = g.Key, Count = g.Count() })
            .ToList();

            // Unir con los nombres de las categorías usando un left join
            List<JugadoresxCatViewModel> Lista = (from categoryCount in categoryPlayersCount
                                                 join category in _dataContext.Categories on categoryCount.CategoryId equals category.Category_ID into categoryGroup
                                                 from subCategory in categoryGroup.DefaultIfEmpty()
                                                 select new JugadoresxCatViewModel
                                                 {
                                                     Categorias = subCategory != null ? subCategory.Category_Name : "Unknown Category",
                                                     CantidadJugadores = categoryCount.Count
                                                 }).ToList();

            // List<JugadoresxCatViewModel> Lista = _dataContext.Torneos.ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }

    }
}
