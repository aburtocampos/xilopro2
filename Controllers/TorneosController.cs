using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text.RegularExpressions;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Helpers;
using xilopro2.Helpers.Interfaces;
using xilopro2.Migrations;
using xilopro2.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace xilopro2.Controllers
{
    public class TorneosController : Controller
    {

        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IEntityModelConverter _entityModelConverter;
        private readonly ICombos _combos;

        AppUser usuarioensesion = null;

        public TorneosController(IUserHelper userHelper, DataContext context, IImageHelper imageHelper, IEntityModelConverter entityModelConverter, ICombos combos)
        {
            _userHelper = userHelper;
            _context = context;
            _imageHelper = imageHelper;
            _entityModelConverter = entityModelConverter;
            _combos = combos;
        }


        #region Torneo

        public async Task<IActionResult> Index()
        {
            usuarioensesion = await _userHelper.GetUserAsyncbyEmail(User.Identity.Name.ToString());
            List<int> filtroIdsCategories = usuarioensesion.SelectedCategoryIds;

            if (User.IsInRole("Editor") || User.IsInRole("Dt"))
            {
                List<Torneo> filteredTorneos = _context.Torneos
                      .Include(t => t.Groups)
                    .AsEnumerable()
                   .Where(t => t.SelectedCategoryIds.Any(id => filtroIdsCategories.Contains(id)))
                  .ToList();
                if (filteredTorneos == null)
                {
                    Problem("Entity set 'DataContext.Torneos'  is null.");
                }
                else
                {
                    // ViewBag.Cats = _combos.GetCategoriasPorIds(filtroIdsCategories);
                    ViewBag.Cats = _combos.GetCategorias();
                    return View(filteredTorneos);
                }
                // Pasar los jugadores filtrados a la vista
            }

            ViewBag.Cats = _combos.GetCategorias();

            IActionResult response = View(await _context
                .Torneos
                .Include(t=>t.Groups)
                .OrderBy(t=>t.Torneo_StartDate)
                .ToListAsync());
            return response;
        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            usuarioensesion = await _userHelper.GetUserAsyncbyEmail(User.Identity.Name.ToString());
            List<int> filtroIdsCategories = usuarioensesion.SelectedCategoryIds;

            TorneoViewModel model = new TorneoViewModel
            {
              Categories = _combos.GetCategoriasPorIds(filtroIdsCategories),

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TorneoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tournament = _entityModelConverter.ToTournamentEntity(model, true);
               // tournament.Torneo_Image =  _imageHelper.UploadImage(model.LogoFile, "Tournaments");
                try
                {
                    _context.Add(tournament);
                    await _context.SaveChangesAsync();
                    TempData["successTorneo"] = "Torneo " + tournament.Torneo_Name + " creado!!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("Torneo_Name"))
                    {

                        ModelState.AddModelError(string.Empty, $"Ya existe un torneo con el nombre {tournament.Torneo_Name.ToUpper()} en la categoria {_combos.GetCategoriaPorId(tournament.SelectedCategoryIds)} o en la misma Temporada.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }
            usuarioensesion = await _userHelper.GetUserAsyncbyEmail(User.Identity.Name.ToString());
            List<int> filtroIdsCategories = usuarioensesion.SelectedCategoryIds;
            model.Categories = _combos.GetCategoriasPorIds(filtroIdsCategories);

            return View(model);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            usuarioensesion = await _userHelper.GetUserAsyncbyEmail(User.Identity.Name.ToString());
            List<int> filtroIdsCateg = usuarioensesion.SelectedCategoryIds;
            Torneo tournamentEntity = await _context.Torneos.FindAsync(id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            TorneoViewModel model = _entityModelConverter.ToTournamentViewModel(tournamentEntity);
            model.Categories = _combos.GetCategoriasPorIds(filtroIdsCateg);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TorneoViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = model.Torneo_Image;

                try
                {
                    if (model.LogoFile != null)
                    {//si carga archivo

                        if (!string.IsNullOrEmpty(model.Torneo_Image))
                        {
                            if (_imageHelper.DeleteImage(model.Torneo_Image, "Tournaments"))
                            {
                                model.Torneo_Image = _imageHelper.UploadImage(model.LogoFile, "Tournaments");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Torneos");
                            }
                        }
                        else
                        {
                            model.Torneo_Image = _imageHelper.UploadImage(model.LogoFile, "Tournaments");
                        }
                    }

                    //  var player = await _swithUsers.ModelToPlayer(model, false);

                    Torneo tournamentEntity = _entityModelConverter.ToTournamentEntity(model, false);

                    _context.Update(tournamentEntity);
                    await _context.SaveChangesAsync();
                    TempData["successTorneo"] = "Torneo " + tournamentEntity.Torneo_Name + " editado!!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("Torneo_Name"))
                    {

                        ModelState.AddModelError(string.Empty, $"Ya existe un torneo con el nombre {model.Torneo_Name.ToUpper()} en la categoria {_combos.GetCategoriaPorId(model.SelectedCategoryIds)}.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }
            usuarioensesion = await _userHelper.GetUserAsyncbyEmail(User.Identity.Name.ToString());
            List<int> filtroIdsCategories = usuarioensesion.SelectedCategoryIds;
            model.Categories = _combos.GetCategoriasPorIds(filtroIdsCategories);
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Torneos == null)
            {
                return NotFound();
            }

            var torneo = await _context.Torneos.FirstOrDefaultAsync(m => m.Torneo_ID == id);

            TorneoViewModel model = new()
            {
                Torneo_ID = torneo.Torneo_ID,
                Torneo_Name = torneo.Torneo_Name,
                Torneo_StartDate = torneo.Torneo_StartDate,
                Torneo_EndDate = torneo.Torneo_EndDate,
                Torneo_Season = torneo.Torneo_Season,
                Torneo_Image = torneo.Torneo_Image,
                Torneo_Status = torneo.Torneo_Status,
            };

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Torneos == null)
            {
                return Problem("Entity set 'DataContext.Torneos'  is null.");
            }
            var torneo = await _context.Torneos.FindAsync(id);
            if (torneo != null)
            {
                _context.Torneos.Remove(torneo);
            }

            try
            {
                await _context.SaveChangesAsync();
                _imageHelper.DeleteImage(torneo.Torneo_Image, "Tournaments");
                TempData["successTorneo"] = "Torneo " + torneo.Torneo_Name + " Eliminado!!";
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }


        #endregion


        #region Grupos

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userName = User.Identity.Name;
            var user = await _userHelper.GetUserAsyncbyEmail(userName);
         //   List<string> catnames = _context.Categories.Where(e => user.SelectedCategoryIds.Contains(e.Category_ID)).Select(e => e.Category_Name).ToList();

            Torneo tournamentEntity = await _context.Torneos
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.TeamLocal)
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.TeamVisitor)
                .Include(t => t.Groups)
                .ThenInclude(t => t.GroupDetails)
                .FirstOrDefaultAsync(m => m.Torneo_ID == id);

            if (tournamentEntity == null)
            {
                return NotFound();
            }
           // ViewBag.CatNames = catnames;
            var cate = _combos.GetCategoriasPorIds(tournamentEntity.SelectedCategoryIds);
            ViewData["CatName"] = cate.FirstOrDefault().Text;
            return View(tournamentEntity);
        }


        public async Task<IActionResult> AddGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentEntity = await _context.Torneos.FindAsync(id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            var model = new GroupViewModel
            {
                TorneoName = tournamentEntity.Torneo_Name,
                Torneoid = tournamentEntity.Torneo_ID,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGroup(GroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Groups groupEntity = new()
                    {
                        Group_Name = model.Group_Name,
                        torneoId = model.Torneoid,
                    };

                    try
                    {
                        _context.Add(groupEntity);
                        await _context.SaveChangesAsync();
                        TempData["successTorneo"] = "Grupo " + model.Group_Name + " creado!!";
                        return RedirectToAction(nameof(Details), new { Id = model.Torneoid });
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un grupo con el mismo nombre en este torneo");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }

            return View(model);
        }


        public async Task<IActionResult> EditGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Groups groupEntity = await _context.Groups
                .Include(g => g.Torneo)
                .FirstOrDefaultAsync(g => g.Group_ID == id);
            if (groupEntity == null)
            {
                return NotFound();
            }

            GroupViewModel model = new()
            {
             GroupID = groupEntity.Group_ID,
             Group_Name = groupEntity.Group_Name,
             Torneoid   = groupEntity.torneoId,
             TorneoName = groupEntity.Torneo.Torneo_Name,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroup(GroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Groups groupEntity = new()
                    {
                        Group_Name = model.Group_Name,
                        torneoId = model.Torneoid,
                        Group_ID = model.GroupID,
                    };

                    try
                    {
                        _context.Update(groupEntity);
                        await _context.SaveChangesAsync();
                        TempData["successTorneo"] = "Grupo " + model.Group_Name + " creado!!";
                        return RedirectToAction(nameof(Details), new { Id = model.Torneoid });
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un grupo con el mismo nombre en este torneo");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {

                }
            }
                return View(model);
        }


        public async Task<IActionResult> DeleteGroup(int? id)
        {
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.FirstOrDefaultAsync(m => m.Group_ID == id);

            GroupViewModel model = new()
            {
                GroupID = group.Group_ID,
                Group_Name = group.Group_Name,
                Torneoid = group.torneoId,
                TorneoName = _context.Torneos
                                .Where(torneo => torneo.Torneo_ID == group.torneoId)
                                .Select(torneo => torneo.Torneo_Name)
                                .FirstOrDefault(),
        };

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [HttpPost, ActionName("DeleteGroup")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedGroup(int id)
        {
            if (_context.Groups == null)
            {
                return Problem("Entity set 'DataContext.Groups'  is null.");
            }
            var group = await _context.Groups.FindAsync(id);
            if (group != null)
            {
                _context.Groups.Remove(group);
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["successTorneo"] = "Grupo " + group.Group_Name + " Eliminado!!";
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Details), new { Id = group.torneoId });
        }

        #endregion


        #region GroupDetails


        public async Task<IActionResult> AddGroupDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _context.Groups.FindAsync(id);//id de grupo
            if (groupEntity == null)
            {
                return NotFound();
            }

            var model = new GroupDetailViewModel
            {
                //Groups = groupEntity,
                GroupId = groupEntity.Group_ID,
                Teams = _combos.GetCombosEquipos(),
                GroupName = groupEntity.Group_Name,
                
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGroupDetail(GroupDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var groupDetailEntity = await _converterHelper.ToGroupDetailEntityAsync(model, true);
                try
                {
                    GroupDetail groupDetailEntity = new()
                    {
                        GoalsAgainst = model.GoalsAgainst,
                        GoalsFor = model.GoalsFor,
                        MatchesLost = model.MatchesLost,
                        MatchesPlayed = model.MatchesPlayed,
                        MatchesTied = model.MatchesTied,
                        MatchesWon = model.MatchesWon,
                        groupId = model.GroupId,
                        teamId = model.TeamId,
                        
                    };

                    try
                    {
                        _context.Add(groupDetailEntity);
                        await _context.SaveChangesAsync();
                        TempData["successTorneo"] = "Equipos agregados a Grupo " + model.GroupName + " exitosamente!!";
                        return RedirectToAction(nameof(DetailsGroup), new { Id = model.GroupId });
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un equipo con el mismo nombre en este grupo");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            model.Teams = _combos.GetCombosEquipos();
            return View(model);
        }

        public async Task<IActionResult> EditGroupDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupdetEntity = await _context.GroupDetails.FindAsync(id);
            if (groupdetEntity == null)
            {
                return NotFound();
            }

            var model = new GroupDetailViewModel
            {
                GroupDetailID = groupdetEntity.GroupDetail_ID,
                GoalsAgainst = groupdetEntity.GoalsAgainst,
                GoalsFor = groupdetEntity.GoalsFor,
                GroupId = groupdetEntity.groupId,
                MatchesLost = groupdetEntity.MatchesLost,
                MatchesPlayed = groupdetEntity.MatchesPlayed,
                MatchesTied = groupdetEntity.MatchesTied,
                MatchesWon = groupdetEntity.MatchesWon,
                TeamId = groupdetEntity.teamId,
                GroupName = _context.Groups.Where(g => g.Group_ID == groupdetEntity.groupId)
                                    .Select(g => g.Group_Name)
                                    .FirstOrDefault(),
              Teams = _combos.GetCombosEquipos(),
        };
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroupDetail(GroupDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    GroupDetail groupdetEntity = new()
                    {
                        GoalsAgainst = model.GoalsAgainst,
                        GoalsFor = model.GoalsFor,
                        MatchesLost = model.MatchesLost,
                        MatchesPlayed = model.MatchesPlayed,
                        MatchesTied = model.MatchesTied,
                        MatchesWon = model.MatchesWon,
                        groupId = model.GroupId,
                        teamId = model.TeamId,
                        GroupDetail_ID = model.GroupDetailID,
                        
                    };

                    try
                    {
                        _context.Update(groupdetEntity);
                        await _context.SaveChangesAsync();
                        TempData["successTorneo"] = "Equipos editados en " + model.GroupName + " exitosamente!!";
                        return RedirectToAction(nameof(DetailsGroup), new { Id = model.GroupId });
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dbUpdateException)
                {
                   // var sqlException = dbUpdateException.GetBaseException() as SqlException;
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                   // if (sqlException != null && sqlException.Number == 2601)
                        {
                        ModelState.AddModelError(string.Empty, "Ya existe un equipo con el mismo nombre en este grupo");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            model.Teams = _combos.GetCombosEquipos();
            model.GroupName = _context.Groups.Where(g => g.Group_ID == model.GroupId)
                                    .Select(g => g.Group_Name)
                                    .FirstOrDefault();
            return View(model);
        }


        public async Task<IActionResult> DeleteGroupDetail(int? id)
        {
            if (id == null || _context.GroupDetails == null)
            {
                return NotFound();
            }

            var groupDet = await _context.GroupDetails
                .Include(gh=>gh.Team)
                .FirstOrDefaultAsync(m => m.GroupDetail_ID == id);

            GroupDetailViewModel model = new()
            {
                GroupDetailID = groupDet.GroupDetail_ID,
                GoalsAgainst = groupDet.GoalsAgainst,
                GoalsFor = groupDet.GoalsFor,
                GroupId = groupDet.groupId,
                MatchesLost = groupDet.MatchesLost,
                MatchesPlayed = groupDet.MatchesPlayed,
                MatchesTied = groupDet.MatchesTied,
                MatchesWon = groupDet.MatchesWon,
                TeamId = groupDet.teamId,
                GroupName = _context.Groups.Where(g => g.Group_ID == groupDet.groupId)
                                    .Select(g => g.Group_Name)
                                    .FirstOrDefault(),
                TeamName = _context.Teams.Where(g => g.Team_ID == groupDet.teamId).Select(t => t.Team_Name).FirstOrDefault(),
            };

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [HttpPost, ActionName("DeleteGroupDetail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedGroupDetail(int? id)
        {
            if (_context.GroupDetails == null)
            {
                return Problem("Entity set 'DataContext.GroupDetails'  is null.");
            }
           // var groupDet = await _context.GroupDetails.FindAsync(id);
            var groupDet = await _context.GroupDetails
                .Include(gc=>gc.Team)
                .FirstOrDefaultAsync(m => m.GroupDetail_ID == id);
            if (groupDet != null)
            {
                _context.GroupDetails.Remove(groupDet);
            }

            try
            {
                await _context.SaveChangesAsync();
                var namegroup = _context.Groups.Where(g => g.Group_ID == groupDet.groupId)
                                    .Select(g => g.Group_Name)
                                    .FirstOrDefault();
                TempData["successTorneo"] = @$"Equipo ${groupDet.Team.Team_Name} eliminado del grupo  ${namegroup}";
            }
            catch (Exception)
            {
                throw;
            }
           
            return RedirectToAction(nameof(DetailsGroup), new { Id = groupDet.groupId });
        }

        public async Task<IActionResult> DetailsGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _context.Groups
                 .Include(g => g.Matches)
                  .ThenInclude(g => g.PlayerStatistics)
                .Include(g => g.Matches)
                .ThenInclude(g => g.TeamLocal)
                .Include(g => g.Matches)
                .ThenInclude(g => g.TeamVisitor)
                .Include(g => g.Torneo)
                .Include(g => g.GroupDetails)
                .ThenInclude(gd => gd.Team)
                 
                .FirstOrDefaultAsync(g => g.Group_ID == id);

            
            if (groupEntity == null)
            {
                return NotFound();
            }
            ViewData["idgroupdetails"] = id;
            ViewData["idtorneo"] = groupEntity.torneoId;
          
          //  ViewData["numb"] = groupEntity.Matches.Select(g => g.PlayerStatsNumber);
            return View(groupEntity);
        }


        #endregion


        #region Matches

      

        public async Task<IActionResult> AddMatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _context.Groups.FindAsync(id);
            if (groupEntity == null)
            {
                return NotFound();
            }

            var model = new MatchViewModel
            {
             
                GroupName = groupEntity.Group_Name,
                GroupId = groupEntity.Group_ID,
                Teams = _combos.GetCombosEquiposPorIds(groupEntity.Group_ID),
                torneoid = groupEntity.torneoId,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMatch(MatchViewModel model)
        {
            ModelState.Remove("Date");
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.LocalId != model.VisitorId)
                    {
                        //       var matchEntity = await _converterHelper.ToMatchEntityAsync(model, true);
                        var matchEntity = new Matchgame
                        {
                            Date = model.Date.ToUniversalTime(),
                            GoalsLocal = model.GoalsLocal,
                            GoalsVisitor = model.GoalsVisitor,
                            IsClosed = model.IsClosed,
                            TeamLocalId = model.LocalId,
                            TeamVisitorId = model.VisitorId,
                            GroupsrId = model.GroupId,
                            Jornada = model.Jornada,
                            torneoid = model.torneoid,
                            //  Groups = await _context.Groups.FindAsync(model.GroupId),
                            // TeamLocal = await _context.Teams.FindAsync(model.LocalId),
                            // TeamVisitor = await _context.Teams.FindAsync(model.VisitorId),
                        };

                        _context.Add(matchEntity);
                        await _context.SaveChangesAsync();

                        TempData["successTorneo"] = "Jornada agregada al grupo " + model.GroupName + " exitosamente!!";
                        return RedirectToAction(nameof(DetailsGroup), new { Id = model.GroupId });
                    }
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe esta jornada en este grupo");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

              //  ModelState.AddModelError(string.Empty, "The local and visitor must be differents teams.");
            }

            model.GroupId = model.GroupId;
            model.GroupName =  _context.Groups.Where(g => g.Group_ID == model.GroupId)
                                    .Select(g => g.Group_Name)
                                    .FirstOrDefault();
            model.Teams = _combos.GetCombosEquiposPorIds(model.GroupId);
            return View(model);
        }


        public async Task<IActionResult> EditMatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchEntity = await _context.Matches
                .Include(m => m.Groups)
                .Include(m => m.TeamLocal)
                .Include(m => m.TeamVisitor)
                .FirstOrDefaultAsync(m => m.Match_ID == id);
            if (matchEntity == null)
            {
                return NotFound();
            }

            var model = new MatchViewModel
            {
             Date = matchEntity.Date,
             GoalsLocal = matchEntity.GoalsLocal,
             GoalsVisitor = matchEntity.GoalsVisitor,
             GroupId = matchEntity.GroupsrId,
             GroupName = matchEntity.Groups.Group_Name,
             IsClosed = matchEntity.IsClosed,
             Jornada = matchEntity.Jornada,
             LocalId = matchEntity.TeamLocalId,
             VisitorId = matchEntity.TeamVisitorId,
             MatchID = matchEntity.Match_ID,
             Teams = _combos.GetCombosEquiposPorIds(matchEntity.GroupsrId),
            
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMatch(MatchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var matchEntity = new Matchgame
                {
                    Date = model.Date,
                    GoalsLocal = model.GoalsLocal,
                    GoalsVisitor = model.GoalsVisitor,
                    GroupsrId = model.GroupId,
                    IsClosed = model.IsClosed,
                    Jornada = model.Jornada,
                    TeamLocalId = model.LocalId,
                    TeamVisitorId = model.VisitorId,
                    Match_ID = model.MatchID,
                };


                try
                {
                   // _context.Update(matchEntity);
                   // await _context.SaveChangesAsync();
                   //se guarda la edicion del partido y ambien se agregan datos a la tbla ranking
                    SaveMatchgameWithGroupDetail(matchEntity);

                    TempData["successTorneo"] = "Jornada editada al grupo " + model.GroupName + " exitosamente!!";
                    return RedirectToAction(nameof(DetailsGroup), new { Id = model.GroupId });
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return View(model);
        }



        public async Task<IActionResult> DeleteMatch(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }
            var matchEntity = await _context.Matches
             .Include(m => m.Groups)
             .FirstOrDefaultAsync(m => m.Match_ID == id);

            MatchViewModel model = new()
            {
               Date = matchEntity.Date,
               GoalsLocal = matchEntity.GoalsLocal,
               GoalsVisitor = matchEntity.GoalsVisitor,
               GroupId = matchEntity.GroupsrId,
               IsClosed = matchEntity.IsClosed,
               Jornada = matchEntity.Jornada,
               LocalId = matchEntity.TeamLocalId,
               VisitorId = matchEntity.TeamVisitorId,
               GroupName = matchEntity.Groups.Group_Name,
               
            };

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [HttpPost, ActionName("DeleteMatch")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedMatch(int? id)
        {
            if (_context.Matches == null)
            {
                return Problem("Entity set 'DataContext.Matches'  is null.");
            }
            var matchEntity = await _context.Matches
           //   .Include(m => m.Groups)
              .FirstOrDefaultAsync(m => m.Match_ID == id);

            //saber si existen mas de una jornada grabadas en tabla
            var cantidadMachesporGroup = _context.GroupDetails.Where(k => k.groupId == matchEntity.GroupsrId);

            //si existen borrar los datos de partidos y restarlos de la tabla
          
            if (matchEntity != null)
            {
                //borrar datos de tabla
                if (cantidadMachesporGroup.Count() > 1)
                {
                    RestedSaveMatchgameWithGroupDetail(matchEntity);
                }
                else
                {
                    DeleteMatchgameAndRelatedGroupDetails(id);
                }

                //borrar match
                _context.Matches.Remove(matchEntity);
            }

            try
            {
                await _context.SaveChangesAsync();
                var namegroup = _context.Groups.Where(g => g.Group_ID == matchEntity.GroupsrId)
                                    .Select(g => g.Group_Name)
                                    .FirstOrDefault();
                TempData["successTorneo"] = "Jornada Eliminada!!";
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(DetailsGroup), new { Id = matchEntity.GroupsrId });
        }


        #endregion


        #region Stats

        public async Task<IActionResult> ListStats(int DetailsGroup_ID, int Match_ID, int Torneo_ID)
        {
            if (Match_ID == null)
            {
                return NotFound();
            }

            var partido = _context.Matches.Where(m=>m.Match_ID == Match_ID);

            List<PlayerStatistics> statEntity =  _context.PlayerStatistics
                
                .Include(g => g.Player)
                .Where(g => g.MatchId == Match_ID).ToList();

            foreach (var playerStatistic in statEntity)
            {
                playerStatistic.MatchStatus = partido.FirstOrDefault().IsClosed;
            }


            if (statEntity == null)
            {
                return NotFound();
            }
            ViewData["idmach"] = Match_ID;
            ViewData["idgroupdetails"] = DetailsGroup_ID;
            ViewData["idtorneo"] = Torneo_ID;
            ViewData["jornada"] = partido.FirstOrDefault().Jornada;
            ViewData["statusmach"] = partido.FirstOrDefault().IsClosed;
            return View(statEntity); 
        }


        public async Task<IActionResult> AddStats(int DetailsGroup_ID, int Match_ID, int Torneo_ID)
        {
            if (Match_ID == null)
            {
                return NotFound();
            }

            var torneo = _context.Torneos.Where(x => x.Torneo_ID == Torneo_ID).FirstOrDefault();

            /* List<Player> filteredPlayers = _context.Players
               .AsEnumerable()
               .Where(player => player.SelectedCategoryIds.Any(id => torneo.SelectedCategoryIds.Contains(id)))
              .Where(player => player.Team != null && player.Team.Team_Name == "XILOTEPELT FC")
               .ToList();*/
            var match = _context.Matches.FirstOrDefault(m => m.Match_ID == Match_ID); // suponiendo que tienes el Id de la entidad Match
           // var match = _context.Matches.Where(m => m.Match_ID == Match_ID);

            List<Player> filteredPlayers = _context.Players
               .AsEnumerable()
               .Where(player => player.SelectedCategoryIds.Any(id => torneo.SelectedCategoryIds.Contains(id)))
                .Where(player => player.Teamid == match.TeamLocalId || player.Teamid == match.TeamVisitorId) // filtra por equipos cargados en el partido
               .ToList();


            var model = new PlayerStatisticViewModel
            {
                MatchId = Match_ID,
                DetailsGroupId = DetailsGroup_ID,
                Players = filteredPlayers,
                TorneoId = Torneo_ID,
                Jornada = match.Jornada,

            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddStats(PlayerStatisticViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                        var statsEntity = new PlayerStatistics
                        {
                           CornerKicks = model.CornerKicks,
                           Fouls = model.Fouls,
                           GoalkeeperSaves = model.GoalkeeperSaves,
                           Goals = model.Goals,
                           GoalsConceded = model.GoalsConceded,
                           Penalties = model.Penalties,
                           RedCards = model.RedCards,
                           YellowCards = model.YellowCards,
                           PlayerId = model.PlayerId,
                           MatchId = model.MatchId,
                           TorneoId = model.TorneoId,
                           DetailsGroupId = model.DetailsGroupId,
                        };
                        _context.Add(statsEntity);
                        await _context.SaveChangesAsync();
                        TempData["successTorneo"] = "Estadística agregada  exitosamente!!";
                        return RedirectToAction(nameof(ListStats), new { DetailsGroup_ID = model.DetailsGroupId, Match_ID = model.MatchId, Torneo_ID = model.TorneoId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe la estadistica de este jugador");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                //  ModelState.AddModelError(string.Empty, "The local and visitor must be differents teams.");
            }

           // model.Players = _combos.GetCombosPlayers();
            var torneo = _context.Torneos.Where(x => x.Torneo_ID == model.TorneoId).FirstOrDefault();
            List<Player> filteredPlayers = _context.Players
              .AsEnumerable()
              .Where(player => player.SelectedCategoryIds.Any(id => torneo.SelectedCategoryIds.Contains(id)))
              .ToList();
            model.Players = filteredPlayers; 
            return View(model);
        }


        public async Task<IActionResult> EditStats(int? id, int DetailsGroup_ID, int Match_ID, int Torneo_ID)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statEntity = await _context.PlayerStatistics
                .Include(m => m.Player)
                .FirstOrDefaultAsync(m => m.PlayerStatistic_ID == id);

            var torneo = _context.Torneos.Where(x => x.Torneo_ID == Torneo_ID).FirstOrDefault();
            List<Player> filteredPlayers = _context.Players
              .AsEnumerable()
              .Where(player => player.SelectedCategoryIds.Any(id => torneo.SelectedCategoryIds.Contains(id)))
              .ToList();

            if (statEntity == null)
            {
                return NotFound();
            }

            var model = new PlayerStatisticViewModel
            {

                CornerKicks = statEntity.CornerKicks,
                Fouls = statEntity.Fouls,
                GoalkeeperSaves = statEntity.GoalkeeperSaves,
                Goals = statEntity.Goals,
                GoalsConceded = statEntity.GoalsConceded,
                MatchId = statEntity.MatchId,
                Penalties = statEntity.Penalties,
                PlayerId = statEntity.PlayerId,
                RedCards = statEntity.RedCards,
                YellowCards = statEntity.YellowCards,
                PlayerStatistic_ID = statEntity.PlayerStatistic_ID,

                DetailsGroupId = DetailsGroup_ID,
                Players = filteredPlayers,
                TorneoId = Torneo_ID,

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStats(PlayerStatisticViewModel model)
        {
            if (ModelState.IsValid)
            {
                var statEntity = await _context.PlayerStatistics
                   .Include(m => m.Player)
                     .FirstOrDefaultAsync(m => m.PlayerStatistic_ID == model.PlayerStatistic_ID);

                var torneo = _context.Torneos.Where(x => x.Torneo_ID == model.TorneoId).FirstOrDefault();
                List<Player> filteredPlayers = _context.Players
                  .AsEnumerable()
                  .Where(player => player.SelectedCategoryIds.Any(id => torneo.SelectedCategoryIds.Contains(id)))
                  .ToList();

                statEntity = new PlayerStatistics
                {
                    CornerKicks = model.CornerKicks,
                    Fouls = model.Fouls,
                    GoalkeeperSaves = model.GoalkeeperSaves,
                    Goals = model.Goals,
                    GoalsConceded = model.GoalsConceded,
                    MatchId = model.MatchId,
                    Penalties = model.Penalties,
                    PlayerId = model.PlayerId,
                    RedCards = model.RedCards,
                    YellowCards = model.YellowCards,
                    PlayerStatistic_ID = model.PlayerStatistic_ID,

                    DetailsGroupId = model.DetailsGroupId,
                    Players = filteredPlayers,
                    TorneoId = model.TorneoId,
                };

                try
                {
                    _context.Update(statEntity);
                    await _context.SaveChangesAsync();
                    TempData["successTorneo"] = "Estadistica editada exitosamente!!";
                    return RedirectToAction(nameof(ListStats), new { Id = model.PlayerStatistic_ID, DetailsGroup_ID = model.DetailsGroupId, Match_ID = model.MatchId, Torneo_ID = model.TorneoId });
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteStats(int? id, int DetailsGroup_ID)
        {
            if (id == null || _context.PlayerStatistics == null)
            {
                return NotFound();
            }
            var statEntity = await _context.PlayerStatistics
             .Include(m => m.Player)
             .FirstOrDefaultAsync(m => m.PlayerStatistic_ID == id);

            PlayerStatisticViewModel model = new()
            {
                CornerKicks = statEntity.CornerKicks,
                Fouls = statEntity.Fouls,
                GoalkeeperSaves = statEntity.GoalkeeperSaves,
                Goals = statEntity.Goals,
                GoalsConceded = statEntity.GoalsConceded,
                MatchId = statEntity.MatchId,
                Penalties = statEntity.Penalties,
                PlayerId = statEntity.PlayerId,
                RedCards = statEntity.RedCards,
                YellowCards = statEntity.YellowCards,
                PlayerStatistic_ID = statEntity.PlayerStatistic_ID,
                PlayerName = _context.Players.Where(g => g.Player_ID == statEntity.PlayerId)
                                    .Select(g => g.Player_FullName)
                                    .FirstOrDefault(),
                DetailsGroupId = DetailsGroup_ID,
              //  Players = filteredPlayers,
                TorneoId = statEntity.TorneoId,
            };

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [HttpPost, ActionName("DeleteStats")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedStats(int? id)
        {
            if (_context.PlayerStatistics == null)
            {
                return Problem("Entity set 'DataContext.Matches'  is null.");
            }
            var statEntity = await _context.PlayerStatistics
              //   .Include(m => m.Groups)
              .FirstOrDefaultAsync(m => m.PlayerStatistic_ID == id);

            if (statEntity != null)
            {
                _context.PlayerStatistics.Remove(statEntity);
            }

            try
            {
                await _context.SaveChangesAsync();
  
                TempData["successTorneo"] = "Estadistica Eliminada!!";
            }
            catch (Exception)
            {
                throw;
            }
           // return RedirectToAction(nameof(ListStats), new { Id = statEntity.DetailsGroupId, statEntity.PlayerStatistic_ID,  Match_ID = statEntity.MatchId, Torneo_ID =statEntity.TorneoId});
            return RedirectToAction(nameof(ListStats), new { Id = statEntity.PlayerStatistic_ID, DetailsGroup_ID = statEntity.DetailsGroupId, Match_ID = statEntity.MatchId, Torneo_ID = statEntity.TorneoId });
        }


        #endregion


        #region Metodos

         public bool guardarDatosdeTablaRanking(GroupDetailViewModel model)
        {
            //AddGroupDetail
            try
            {
                try
                {
                    _context.SaveChanges();
                    TempData["successTorneo"] = "Equipos agregados a Grupo " + model.GroupName + " exitosamente!!";
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Ya existe un equipo con el mismo nombre en este grupo");
                    return false;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return false;
            }

        }

        public void SaveMatchgameWithGroupDetail(Matchgame matchgame)
        {

            // Obtener los GroupDetails de los equipos involucrados en el partido
            var groupDetailLocal = _context.GroupDetails.FirstOrDefault(gd => gd.teamId == matchgame.TeamLocalId && gd.groupId == matchgame.GroupsrId);
            var groupDetailVisitor = _context.GroupDetails.FirstOrDefault(gd => gd.teamId == matchgame.TeamVisitorId && gd.groupId == matchgame.GroupsrId);

            // Actualizar las estadísticas de los equipos según el resultado del partido
            groupDetailLocal.MatchesPlayed++;
            groupDetailVisitor.MatchesPlayed++;

            if (matchgame.GoalsLocal > matchgame.GoalsVisitor)
            {
                groupDetailLocal.MatchesWon++;
                groupDetailVisitor.MatchesLost++;
                matchgame.IsClosed = false;
            }
            else if (matchgame.GoalsLocal < matchgame.GoalsVisitor)
            {
                groupDetailLocal.MatchesLost++;
                groupDetailVisitor.MatchesWon++;
                matchgame.IsClosed = false;
            }
            else
            {
                groupDetailLocal.MatchesTied++;
                groupDetailVisitor.MatchesTied++;
                matchgame.IsClosed = false;
            }

            groupDetailLocal.GoalsFor += matchgame.GoalsLocal;
            groupDetailLocal.GoalsAgainst += matchgame.GoalsVisitor;
            groupDetailVisitor.GoalsFor += matchgame.GoalsVisitor;
            groupDetailVisitor.GoalsAgainst += matchgame.GoalsLocal;

                _context.GroupDetails.Update(groupDetailLocal);

                _context.GroupDetails.Update(groupDetailVisitor);

                //se guarda el estado del partido
            _context.Matches.Update(matchgame);
            _context.SaveChanges();

        }

        public void RestedSaveMatchgameWithGroupDetail(Matchgame matchgame)
        {

            // Obtener los GroupDetails de los equipos involucrados en el partido
            var groupDetailLocal = _context.GroupDetails.FirstOrDefault(gd => gd.teamId == matchgame.TeamLocalId && gd.groupId == matchgame.GroupsrId);
            var groupDetailVisitor = _context.GroupDetails.FirstOrDefault(gd => gd.teamId == matchgame.TeamVisitorId && gd.groupId == matchgame.GroupsrId);

            // Actualizar las estadísticas de los equipos según el resultado del partido
            groupDetailLocal.MatchesPlayed--;
            groupDetailVisitor.MatchesPlayed--;

            if (matchgame.GoalsLocal > matchgame.GoalsVisitor)
            {
                groupDetailLocal.MatchesWon--;
                groupDetailVisitor.MatchesLost--;
                matchgame.IsClosed = false;
            }
            else if (matchgame.GoalsLocal < matchgame.GoalsVisitor)
            {
                groupDetailLocal.MatchesLost--;
                groupDetailVisitor.MatchesWon--;
                matchgame.IsClosed = false;
            }
            else
            {
                groupDetailLocal.MatchesTied--;
                groupDetailVisitor.MatchesTied--;
                matchgame.IsClosed = false;
            }

            groupDetailLocal.GoalsFor -= matchgame.GoalsLocal;
            groupDetailLocal.GoalsAgainst -= matchgame.GoalsVisitor;
            groupDetailVisitor.GoalsFor -= matchgame.GoalsVisitor;
            groupDetailVisitor.GoalsAgainst -= matchgame.GoalsLocal;

            _context.GroupDetails.Update(groupDetailLocal);

            _context.GroupDetails.Update(groupDetailVisitor);

            _context.SaveChanges();

        }


        public void DeleteMatchgameAndRelatedGroupDetails(int? matchgameId)
        {
            // Buscar el Matchgame a eliminar
            var matchgame = _context.Matches.FirstOrDefault(m => m.Match_ID == matchgameId);

            if (matchgame != null)
            {
                // Buscar los GroupDetails relacionados con los equipos locales y visitantes
                var groupDetailLocal = _context.GroupDetails.FirstOrDefault(gd => gd.teamId == matchgame.TeamLocalId && gd.groupId == matchgame.GroupsrId);
                var groupDetailVisitor = _context.GroupDetails.FirstOrDefault(gd => gd.teamId == matchgame.TeamVisitorId && gd.groupId == matchgame.GroupsrId);

                if (groupDetailLocal != null && groupDetailVisitor != null)
                {
                    // Eliminar los GroupDetails relacionados
                    _context.GroupDetails.Remove(groupDetailLocal);
                    _context.GroupDetails.Remove(groupDetailVisitor);

                    // Eliminar el Matchgame
                    _context.Matches.Remove(matchgame);

                }
            }
        }

        #endregion






    }
}
