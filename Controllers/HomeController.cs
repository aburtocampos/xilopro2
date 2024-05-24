using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
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

        public IActionResult Index() { 
      
            //golesxTorneo();
            // jugadoresxCat()
            // jugadoresgoleadores();


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

                List<SelectListItem> torneosLista = _dataContext.Torneos
                 .Select(gd => new
                 {
                     Torneo = gd,
                     CategoriaID = gd.SelectedCategoryIds.FirstOrDefault()
                 })
                 .ToList()
                 .Select(item => new SelectListItem
                 {
                     // Text = $"{item.Torneo.Torneo_Name} - {(_dataContext.Categories.FirstOrDefault(cat => cat.Category_ID == item.CategoriaID)?.Category_Name ?? "Categoría desconocida")}",
                     Text = $"{item.Torneo.Torneo_Name} - {(item.CategoriaID != null ? (_dataContext.Categories.FirstOrDefault(cat => cat.Category_ID == item.CategoriaID)?.Category_Name ?? "Categoría desconocida") : "Categoría desconocida")}",
                     Value = item.Torneo.Torneo_ID.ToString()
                 })
                 .ToList();

                ViewBag.TorneosLista = torneosLista;

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


        #region Chart

        public IActionResult golesxCategorias() {

          //  var temporadas = _dataContext.Torneos.ToDictionary(c => c.Torneo_ID, c => c.Torneo_Season);
            var playerStatistics = _dataContext.PlayerStatistics.ToList();
            var categoryNames = _dataContext.Categories.ToDictionary(c => c.Category_ID, c => c.Category_Name);

            var Lista = playerStatistics
                .GroupBy(stat => stat.TorneoId)
                .Select(g => new GolesxtorneoViewModel
                {
                   
                    Categorias = categoryNames[g.Key],
                    CantidadGoles = g.Sum(stat => stat.Goals)
                })
                .ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        public IActionResult jugadoresgoleadores()
        {
            /*  var players = _dataContext.Players.ToList();
              var playerStatistics = _dataContext.PlayerStatistics.ToList();
              var categoryNames = _dataContext.Categories.ToDictionary(c => c.Category_ID, c => c.Category_Name);

              List<GolesxCatViewModel> Lista = players
                  .SelectMany(player => player.SelectedCategoryIds.Select(categoryId => new { Player = player, CategoryId = categoryId }))
                  .GroupBy(cp => new { cp.CategoryId, cp.Player.Player_ID, cp.Player.Player_FullName })
                  .Select(g => new GolesxCatViewModel
                  {
                      Categorias = categoryNames[g.Key.CategoryId],
                      Player = g.Key.Player_FullName,
                      Goles = g.Max(x => playerStatistics
                          .Where(stat => stat.PlayerId == g.Key.Player_ID && stat.TorneoId == g.Key.CategoryId)
                          .Sum(stat => stat.Goals))
                  })
                  .GroupBy(cp => cp.Categorias)
                  .Select(g => g.OrderByDescending(x => x.Goles).FirstOrDefault())
                  .ToList();*/

            /* var players = _dataContext.Players.ToList();
             var playerStatistics = _dataContext.PlayerStatistics.ToList();
             var categoryNames = _dataContext.Categories.ToDictionary(c => c.Category_ID, c => c.Category_Name);

             List<GolesxCatViewModel> Lista = players
                 .SelectMany(player => player.SelectedCategoryIds.Select(categoryId => new { Player = player, CategoryId = categoryId }))
                 .GroupBy(cp => new { cp.CategoryId, cp.Player.Player_ID, cp.Player.Player_FullName })
                 .Select(g => new GolesxCatViewModel
                 {
                     Categorias = categoryNames[g.Key.CategoryId],
                     Player = g.Key.Player_FullName,
                     Goles = playerStatistics
                         .Where(stat => stat.PlayerId == g.Key.Player_ID && stat.TorneoId == g.Key.CategoryId)
                         .Sum(stat => stat.Goals)
                 })
                 .GroupBy(cp => cp.Categorias)
                 .SelectMany(g => g.OrderByDescending(x => x.Goles).Take(3)) // Aquí es donde seleccionamos los top 3 goleadores por categoría
                 .ToList();*/


            // Primero traemos los datos necesarios a memoria
            /* var players = _dataContext.Players.ToList();
             var categories = _dataContext.Categories.ToList();
             var playerStats = _dataContext.PlayerStatistics.ToList();

             // Luego usamos LINQ to Objects para procesar los datos
             var query = from playerStat in playerStats
                         join player in players on playerStat.PlayerId equals player.Player_ID
                         from categoryId in player.SelectedCategoryIds
                         join category in categories on categoryId equals category.Category_ID
                         group playerStat by new
                         {
                             playerStat.PlayerId,
                             player.Player_FirstName,
                             player.Player_LastName,
                             categoryId,
                             category.Category_Name
                         } into grouped
                         where grouped.Sum(ps => ps.Goals) > 0
                         orderby grouped.Sum(ps => ps.Goals) descending
                         select new GolesxCatViewModel
                         {
                             Player = $@"{grouped.Key.Player_LastName} {grouped.Key.Player_FirstName}",
                             Categorias = grouped.Key.Category_Name,
                             Goles = grouped.Sum(ps => ps.Goals)
                         };

             var Lista = query.ToList();*/
            var players = _dataContext.Players.ToList();
            var categories = _dataContext.Categories.ToList();
            var playerStats = _dataContext.PlayerStatistics.ToList();

            // Procesar los datos usando LINQ to Objects
            var query = from playerStat in playerStats
                        join player in players on playerStat.PlayerId equals player.Player_ID
                        from categoryId in player.SelectedCategoryIds
                        join category in categories on categoryId equals category.Category_ID
                        group playerStat by new
                        {
                           // playerStat.PlayerId,
                            category.Category_Name
                        } into groupedByCategory
                        select new
                        {
                           // CategoryId = groupedByCategory.Key.CategoryId,
                            CategoryName = groupedByCategory.Key.Category_Name,
                            TopScorer = groupedByCategory
                                         .GroupBy(x => x.PlayerId)
                                         .Select(g => new
                                         {
                                             PlayerId = g.Key,
                                             Goles = g.Sum(x => x.Goals),
                                             PlayerName = $"{players.First(p => p.Player_ID == g.Key).Player_LastName} {players.First(p => p.Player_ID == g.Key).Player_FirstName}"
                                         })
                                         .OrderByDescending(g => g.Goles)
                                         .FirstOrDefault() // Tomamos el jugador con más goles
                        };

            var Lista = query.ToList()
               // .GroupBy(x => x.CategoryName)
                .Select(x => new GolesxCatViewModel
            {
                Player = $"{x.TopScorer.PlayerName} - {x.CategoryName.Substring(0,5)}",
                Categorias =  x.CategoryName,
                Goles = x.TopScorer.Goles
            }).ToList().OrderByDescending(g => g.Goles);


            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        public IActionResult cantjugadoresxCategorias()
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

        public IActionResult golesxTemporadasCategorias(string nombreTorneo, string season)
        {

            //  var temporadas = _dataContext.Torneos.ToDictionary(c => c.Torneo_ID, c => c.Torneo_Season);
            /*   var playerStatistics = _dataContext.PlayerStatistics.ToList();
               var categoryNames = _dataContext.Categories.ToDictionary(c => c.Category_ID, c => c.Category_Name);

               var Lista = playerStatistics
                   .GroupBy(stat => stat.TorneoId)
                   .Select(g => new GolesxtorneoViewModel
                   {

                       Categorias = categoryNames[g.Key],
                       CantidadGoles = g.Sum(stat => stat.Goals)
                   })
                   .ToList();*/
            var torneosFiltrados = _dataContext.Torneos
               .Where(t => t.Torneo_Name == nombreTorneo && t.Torneo_Season == season)
               .ToDictionary(t => t.Torneo_ID);

            // Filtrar las estadísticas de jugadores por los torneos seleccionados
            var playerStatistics = _dataContext.PlayerStatistics
                .Where(ps => torneosFiltrados.ContainsKey(ps.TorneoId))
                .ToList();

            var categoryNames = _dataContext.Categories.ToDictionary(c => c.Category_ID, c => c.Category_Name);

            // Agrupar y proyectar las estadísticas según el torneo
            var Lista = playerStatistics
                .GroupBy(stat => new { TorneoId = stat.TorneoId, NombreTorneo = torneosFiltrados[stat.TorneoId].Torneo_Name, Temporada = torneosFiltrados[stat.TorneoId].Torneo_Season })
                .Select(g => new GolesxtorneoViewModel
                {
                  //  NombreTorneo = g.Key.NombreTorneo,
                 //   Temporada = g.Key.Temporada,
                    CantidadGoles = g.Sum(stat => stat.Goals),
                    //Categorias = categoryNames[g.Key],
                })
                .ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        public IActionResult CargarTemporadas(string torneoName)
        {
            var torneos = _dataContext.Torneos
              .Where(t => t.Torneo_Name == torneoName)
              .Select(t => new { TorneoName = t.Torneo_Name, Temporada = t.Torneo_Season })
              .ToList();

            return StatusCode(StatusCodes.Status200OK, torneos);
        }

        #endregion

    }
}
