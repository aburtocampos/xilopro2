using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Helpers;
using xilopro2.Helpers.Interfaces;
using xilopro2.Models;

namespace xilopro2.Controllers
{
    public class PlayerStatisticsController : Controller
    {
        AppUser usuarioensesion = null;
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;

        public PlayerStatisticsController(IUserHelper userHelper, DataContext context)
        {
            _userHelper = userHelper;
            _context = context;
        }

        // GET: PlayerStatisticsController
        public async Task<IActionResult> Index()
        {

     
           usuarioensesion = await _userHelper.GetUserAsyncbyEmail(User.Identity.Name.ToString());
            List<int> filtroIdsCategories = usuarioensesion.SelectedCategoryIds;
            List<PlayerStatistics> filteredPlayers = new List<PlayerStatistics>();

            filteredPlayers = _context.PlayerStatistics
                     .Include(ps => ps.Player)
                     .ThenInclude(p => p.Team)
                      .Include(ps => ps.Matchgame)
                     .AsEnumerable()
                     .Where(ps => ps.Player.SelectedCategoryIds.Any(id => filtroIdsCategories.Contains(id)))

                     .ToList();

            var torneoIds = filteredPlayers.Select(ps => ps.TorneoId).Distinct().ToList();
            var equipoIds = filteredPlayers.Select(ps => ps.Player.Teamid).Distinct().ToList();

            var torneos = _context.Torneos
                .Where(t => torneoIds.Contains(t.Torneo_ID))
                .ToList();

            var partidos = _context.Matches
                .Where(t => torneoIds.Contains(t.Match_ID))
                .ToList();

            /*var equipos = _context.Teams
              .Where(t => filteredPlayers.Select(ps => ps.Player.Teamid).Distinct().Contains(t.Team_ID))
              .ToList();*/
            var equipos = _context.Teams
                .Where(t => equipoIds.Contains(t.Team_ID))
                .ToList();

            //agrupando suamtoria
            var playerStatisticsGrouped = filteredPlayers
            .GroupBy(ps => new { ps.PlayerId, TorneoSeason = torneos.FirstOrDefault(t => t.Torneo_ID == ps.TorneoId).Torneo_Season })
            .Select(g => new
            {
                Player = g.First().Player,
                Team = g.First().Player.Team,
                TorneoSeason = g.Key.TorneoSeason,
                Torneos = g.Select(ps => torneos.FirstOrDefault(t => t.Torneo_ID == ps.TorneoId)).Distinct().ToList(),
               // Matches = g.Select(ps => partidos.FirstOrDefault(m => m.Match_ID == ps.MatchId)).Distinct().ToList(),
                Matches = g.Select(ps => partidos.FirstOrDefault(m => m.Match_ID == ps.MatchId)).Where(m => m != null).Distinct().ToList(),
                TotalGoals = g.Sum(ps => ps.Goals), // Sumar los goles
                TotalYellowCards = g.Sum(ps => ps.YellowCards),
                TotalFouls = g.Sum(ps => ps.Fouls),
                TotalRedCards = g.Sum(ps => ps.RedCards),
                TotalGoalkeeperSaves = g.Sum(ps => ps.GoalkeeperSaves),
                TotalGoalsConceded = g.Sum(ps => ps.GoalsConceded),
                TotalPenalties = g.Sum(ps => ps.Penalties),
                TotalCornerKicks = g.Sum(ps => ps.CornerKicks)
            }).ToList();


            var filteredPlayersWithTorneos = playerStatisticsGrouped
            //.GroupBy(ps => new { ps.PlayerId, TorneoSeason = torneos.FirstOrDefault(t => t.Torneo_ID == ps.TorneoId).Torneo_Season })
                .Select(g => new PlayerStatisticReportViewModel
            {
                    PlayerStatistics = new PlayerStatistics
                    {
                        Player = g.Player,
                        Goals = g.TotalGoals, // Asignar los goles sumados
                        YellowCards = g.TotalYellowCards,
                        Fouls = g.TotalFouls,
                        RedCards = g.TotalRedCards,
                        GoalkeeperSaves = g.TotalGoalkeeperSaves,
                        GoalsConceded = g.TotalGoalsConceded,
                        Penalties = g.TotalPenalties,
                        CornerKicks = g.TotalCornerKicks,
                    },
                    Torneo = g.Torneos.FirstOrDefault(),
                    Matches = g.Matches.FirstOrDefault(),
                    Teams = g.Team
                }).ToList();


            return View(filteredPlayersWithTorneos);
        }

        // GET: PlayerStatisticsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlayerStatisticsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlayerStatisticsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlayerStatisticsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlayerStatisticsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlayerStatisticsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlayerStatisticsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
