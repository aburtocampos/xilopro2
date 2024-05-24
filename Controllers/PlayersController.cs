using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Helpers.Interfaces;
using xilopro2.Migrations;
using xilopro2.Models;

namespace xilopro2.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly ICombos _combos;
        private readonly ISwithUsers _swithUsers;
        private readonly IImageHelper _imageHelper;

        AppUser usuarioensesion = null;

        public PlayersController(DataContext context, ICombos _combosHelper, ISwithUsers _swithUsersHelper, IUserHelper userHelper, IImageHelper imageHelper)
        {
            _context = context;
            _combos = _combosHelper;
            _swithUsers = _swithUsersHelper;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
        }

        #region PLAYERS

        // GET: Players
        public async Task<IActionResult> Index()
        {

           // var usuarioLogueado = _userHelper.GetUserAsync(User.Identity.Name);
           // ViewBag.ProfileImage = usuarioActual.Result.User_Image;

            usuarioensesion = await _userHelper.GetUserAsyncbyEmail(User.Identity.Name.ToString());
            List<int> filtroIdsCategories = usuarioensesion.SelectedCategoryIds;
           
            // var catId = Convert.ToInt32(usuarioensesion.SelectedCategoryIds);
            //Debug.WriteLine($"sesion de: {filtroIds}");

            if (User.IsInRole("Editor") || User.IsInRole("Dt"))
            {
                /* IActionResult usuariosFiltrados = await _context.Players
                 .Include(c => c.Country)
                 .Include(c => c.Parents)
                 .Include(c => c.Position)
                 .Include(c => c.Team)
                // .Where(p => p.SelectedCategoryIds.Any(id => filtroIds.Contains(id)))
                // .Where(p => p.SelectedCategoryIds.Intersect(filtroIds).Any())
                  .Where(p => filtroIds.Any() && p.SelectedCategoryIds.Intersect(filtroIds).Any())
                 .ToListAsync();*/

               /* IActionResult usuariosFiltrados = _context.Players != null ?
                      View(  await _context.Players.ToListAsync())
                         
                       // .Where(p => p.SelectedCategoryIds.Intersect(filtroIds).Any())
                      //  .ToList().Where(player => player.SelectedCategoryIds.Intersect(filtroIds).Any()) 
                        :
                        Problem("Entity set 'DataContext.Players'  is null.");


                return usuariosFiltrados;*/
                List<Player> filteredPlayers =  _context.Players
                     .Include(cp => cp.Position)
                      .Include(cp => cp.Team)
                      .Include(cp => cp.PlayerFiles)
                      .Include(cp => cp.Parents)
                    .AsEnumerable()
                   // .Where(player => player.SelectedCategoryIds.Intersect(filtroIds).Any())
                   .Where(player => player.SelectedCategoryIds.Any(id => filtroIdsCategories.Contains(id)))
                  .ToList();
                if (filteredPlayers == null)
                {
                    Problem("Entity set 'DataContext.Players'  is null.");
                }
                else
                {
                    ViewBag.Cats = _combos.GetCategoriasPorIds(filtroIdsCategories);
                    return View(filteredPlayers);
                }

                // Pasar los jugadores filtrados a la vista
               
            }

            ViewBag.Cats = _combos.GetCategoriasPorIds(filtroIdsCategories);


            IActionResult response = View(await _context.Players
                     .Include(cp => cp.Position)
                     .Include(cp => cp.PlayerFiles)
                     .Include(cp => cp.Parents)
                    .ToListAsync());
            return response;

        }


     
        public async Task<IActionResult> Details(int? id)
        {
            usuarioensesion = await _userHelper.GetUserAsyncbyEmail(User.Identity.Name.ToString());
            List<int> filtroIdsCateg = usuarioensesion.SelectedCategoryIds;

            if (id == null || _context.Players == null)
            {
                return NotFound();
            }

            Player player = await _context.Players
                 .Include(c => c.CorrectionActions)
                 .Include(c => c.Parents)
                 .Include(c => c.PlayerFiles)
                 .FirstOrDefaultAsync(m => m.Player_ID == id);

            //consulta que devuelve las etadsiticas registradas en un partido de jugador
            /*   var listplayerStats = _context.PlayerStatistics
               .Where(ps => ps.PlayerId == id)
               .Join(_context.Matches, 
                   ps => ps.MatchId, 
                   m => m.Match_ID, 
                   (ps, m) => new 
                   {
                       PlayerStatistic = ps,
                       Match = m
                   })
               .ToList();*/

            var listplayerStats = _context.PlayerStatistics
            .Where(ps => ps.PlayerId == id)
            .Join(_context.Matches,
                ps => ps.MatchId,
                m => m.Match_ID,
                (ps, m) => new
                {
                    PlayerStatistic = ps,
                    Match = m
                })
            .Join(_context.Torneos, // Aquí se agrega la unión con la entidad Torneo
                ps => ps.PlayerStatistic.TorneoId, // Suponiendo que Match tiene una propiedad TorneoId que corresponde a Torneo_ID
                t => t.Torneo_ID,
                (psm, t) => new
                {
                    PlayerStatistic = psm.PlayerStatistic,
                    Match = psm.Match,
                    Torneo = t
                })
            .OrderByDescending(d => d.Match.Jornada)
            .ToList();


            if (player == null)
            {
                return NotFound();
            }

           var cate = _combos.GetCategoriasPorIds(player.SelectedCategoryIds);
            ViewBag.CatName = cate.FirstOrDefault().Text;

            string painame = _context.Countries.Where(c => c.Country_ID == player.Countryid).Select(y => y.Country_Name).FirstOrDefault();
            string depname = _context.States.Where(c => c.State_ID == player.Stateid).Select(y => y.State_Name).FirstOrDefault();
            string munname = _context.Cities.Where(c => c.City_ID == player.Cityid).Select(y => y.City_Name).FirstOrDefault();
            string pos = _context.Positions.Where(c => c.Position_ID == player.Positionid).Select(y => y.Position_Name).FirstOrDefault();
            string team = _context.Teams.Where(c => c.Team_ID == player.Teamid).Select(y => y.Team_Name).FirstOrDefault();

            // string teamjornadas = _context.Matches.Where(c => c.Match_ID == listplayerStats.FirstOrDefault().Match.Match_ID).FirstOrDefault();
             ViewBag.teamnames = _context.Teams.ToList();
      


            ViewData["team"] = team;
            ViewData["Pos"] = pos;
            ViewData["PaisName"] = painame;
            ViewData["DepartamentoName"] = depname;
            ViewData["MunicipioName"] = munname;

            List<SelectListItem> listDepas = _context.States
               // .Where(x => x.Country.Country_Name == "Nicaragua")
                .Select(x => new SelectListItem
                {
                    Text = x.State_Name,
                    Value = $"{x.State_ID}"
                })
                .ToList();

            List<SelectListItem> listMuni = _context.Cities
                // .Where(x => x.Country.Country_Name == "Nicaragua")
                .Select(x => new SelectListItem
                {
                    Text = x.City_Name,
                    Value = $"{x.City_ID}"
                })
                .ToList();

            ViewBag.DepartamentoList = listDepas;
            ViewBag.MunicipioList = listMuni;
            ViewBag.StatList = listplayerStats;


            return View(player);
        }

        // GET: Players/Create
        public async Task<IActionResult> Create()
        {
            usuarioensesion = await _userHelper.GetUserAsyncbyEmail(User.Identity.Name.ToString());
           List<int> filtroIdsCateg = usuarioensesion.SelectedCategoryIds;

            //  List<string> miListaDeStrings = new List<string> { "Seleccionar", "Masculino", "Femenino" };
           /*  var depas = new List<State>();
            var munis = new List<City>();

            depas.Add(new State()
            {
                State_ID = 0,
                State_Name = "Seleccionar Departamento.."
            });
            munis.Add(new City()
            {
                City_ID = 0,
                City_Name = "Seleccionar Municipio..."
            });*/

            PlayerViewModel model = new PlayerViewModel
            {
                //Teamid = 1,
                //  UserType = _combos.GetCombosRoles(),
                Countries = _combos.GetCombosCountries(),

              //  States = _combos.GetCombosStates(),
             //   Cities = _combos.GetCombosCities(),

                Categories = _combos.GetCategoriasPorIds(filtroIdsCateg),
                Teams = _combos.GetCombosEquipos(),
                Positions = _combos.GetCombosPosiciones(),
               // Player_Cedula = @$"${1++}XXXXXXXXXXXXX",

               // Player_ID = "null",
              //  Player_GenerosEnum =  _combos.GetComboGenerosEnum(),
               // Player_ID = Guid.NewGuid().ToString(),
            };
            ViewBag.Genero = _combos.GetComboGeneros();
         //   ViewBag.State = new SelectList(depas, "State_ID", "State_Name");
           // ViewBag.City = new SelectList(munis, "City_ID", "City_Name");
            return View(model); ;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerViewModel model)
        {
            // player.Teamid = 1;
                Player newuserobj = new Player();
            ModelState.Remove("SelectedCategoryIds");
            if (ModelState.IsValid)
            {
                    try
                    {
                    
                        if (model.SelectedCategoryIdss.Contains(1) && _context.Players.Any(p => p.Player_Cedula == model.Player_Cedula))
                        {
                            var nombrecat = _context.Categories
                               .Where(c => model.SelectedCategoryIds.Contains(c.Category_ID))
                               .Select(c => c.Category_Name)
                               .ToList();
                            throw new InvalidOperationException(@$"Ya existe un jugador con número de cedula {model.Player_Cedula} en esta categoría {string.Join(", ", nombrecat)}");
                        }
                     newuserobj = new Player
                        {
                            // Player_ID = isNew ? Guid.NewGuid().ToString() : model.Player_ID,
                            Player_FirstName = model.Player_FirstName?.Trim().ToUpper(),
                            Player_Address = model.Player_Address,
                            Player_LastName = model.Player_LastName?.Trim().ToUpper(),
                            Player_Status = model.Player_Status,
                            Player_Dorsal = model.Player_Dorsal,
                            Player_FNC = model.Player_FNC,
                            Player_PhoneNumber = model.Player_PhoneNumber,
                            Player_Email = model.Player_Email,
                            Player_Cedula = model.Player_Cedula,
                            Player_fifaid = model.Player_fifaid,
                            Player_Genero = model.Player_Genero,
                            Player_Image = _imageHelper.UploadImage(model.FotoFile, "Players"),
                            SelectedCategoryIds = model.SelectedCategoryIdss,

                            Countryid = model.Countryid,
                            Stateid = model.Stateid,
                            Cityid = model.Cityid,

                            Positionid = model.Positionid,
                            Teamid = model.Teamid,

                           // ID de cat agregado para test
                          //  CategoryId = model.SelectedCategoryIds != null && model.SelectedCategoryIds.Any() ? model.SelectedCategoryIds.First() : 0,


                         //  Country = await _context.Countries.FindAsync(model.CountryID),
                         //  Position = await _context.Positions.FindAsync(model.Positionid),
                         //  Team = await _context.Teams.FindAsync(model.Teamid),

                     };

                        _context.Players.Add(newuserobj);
                        await _context.SaveChangesAsync();

                        TempData["successUser"] = "Jugador " + newuserobj.Player_FullName + " creado!!";
                        return RedirectToAction("Index", "Players");
                    }
                    catch (DbUpdateException ex)
                    {
                    /* if (ex.InnerException.Message.Contains("duplicate"))
                     {
                         ModelState.AddModelError(string.Empty, "Jugador Ya existe");
                     }*/
                   
                          if (ex.InnerException.Message.Contains("Players_SelectedCategoryIds_Player_Dorsal"))
                        {
                        var nombrecat = _context.Categories
                        .Where(c => model.SelectedCategoryIdss.Contains(c.Category_ID))
                        .Select(c => c.Category_Name)
                        .ToList();
                        ModelState.AddModelError(string.Empty, $"El dorsal {model.Player_Dorsal} ya existe en la categoria {string.Join(", ", nombrecat)}");
                        }
                        else
                            {
                                ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                            }
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
            }

            _imageHelper.DeleteImage(newuserobj.Player_Image, "Players");

            usuarioensesion = await _userHelper.GetUserAsyncbyEmail(User.Identity.Name.ToString());
            List<int> filtroIdsCateg = usuarioensesion.SelectedCategoryIds;
            model.Teams = _combos.GetCombosEquipos();
            model.Positions = _combos.GetCombosPosiciones();
            model.Categories = _combos.GetCategoriasPorIds(filtroIdsCateg);

            ViewBag.Genero = _combos.GetComboGeneros();

            model.States = _combos.GetCombosStates();
            model.Countries = _combos.GetCombosCountries();
            model.Cities = _combos.GetCombosCities();

            return View(model);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            usuarioensesion = await _userHelper.GetUserAsyncbyEmail(User.Identity.Name.ToString());
            List<int> filtroIdsCateg = usuarioensesion.SelectedCategoryIds;

           // List<string> miListaDeStrings = new List<string> {   "Seleccionar",   "Masculino",  "Femenino"  };

            Player player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            PlayerViewModel model =  _swithUsers.ToPlayerViewModel(player);
            ViewBag.Genero = _combos.GetComboGeneros();
            //  PlayerViewModel model = new PlayerViewModel
            //   {
            //Teamid = 1,
            //  UserType = _combos.GetCombosRoles(),
          //  model.Teams = _combos.GetCombosEquipos();
         //   model.States = _combos.GetCombosStates();
         //   model.Countries = _combos.GetCombosCountries();
          //  model.Cities = _combos.GetCombosCities();

            model.Categories = _combos.GetCategoriasPorIds(filtroIdsCateg);
          //  model.Positions = _combos.GetCombosPosiciones();


                // Player_ID = "null",
                //Player_Genero =  _combos.GetComboGeneros(),
                // Player_ID = Guid.NewGuid().ToString(),
          //  };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(PlayerViewModel model)
        {
           if (model.Player_ID == null)
            {
                return NotFound();
            }
            ModelState.Remove("SelectedCategoryIds");
            if (ModelState.IsValid)
            {
                try
                {

                 //   Player playerondb = new Player();
                    Player playerondb = await _context.Players.FindAsync(model.Player_ID);
                    var player = (dynamic)null; 

                    if (model.FotoFile != null)
                    {//si carga archivo
                        
                        if (!string.IsNullOrEmpty(playerondb.Player_Image))
                        {
                            if (_imageHelper.DeleteImage(playerondb.Player_Image, "Players"))
                            {
                                model.Player_Image = _imageHelper.UploadImage(model.FotoFile, "Players");
                                player = await _swithUsers.ModelToPlayer(model, false);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Players");
                            }
                        }
                        else
                        {
                            model.Player_Image = _imageHelper.UploadImage(model.FotoFile, "Players");
                            player = await _swithUsers.ModelToPlayer(model, false);
                        }
                    }
                    else
                    {
                        player = await _swithUsers.ModelToPlayer(model, false);
                        player.Player_Image = playerondb.Player_Image;
                    }

                    //      

                    _context.Update(player);
                    await _context.SaveChangesAsync();
                        
                    TempData["successPlayer"] = "Jugador " + player.Player_FullName + " editado!!";
                    return RedirectToAction("Index", "Players");
                }
                catch (DbUpdateException ex)
                {
                    /* if (ex.InnerException.Message.Contains("duplicate"))
                     {
                         ModelState.AddModelError(string.Empty, "Jugador Ya existe");
                     }*/
                    if (ex.InnerException.Message.Contains("SelectedCategoryIds"))
                    {
                        var nombrecat = _context.Categories
                            .Where(c => model.SelectedCategoryIds.Contains(c.Category_ID))
                            .Select(c => c.Category_Name)
                            .ToList();
                        ModelState.AddModelError(string.Empty, $"El dorsal {model.Player_Dorsal} ya existe en la categoria {string.Join(", ", nombrecat)}");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                // return RedirectToAction(nameof(Index));
            }
           // ViewBag.Genero = _combos.GetComboGeneros();
            return View(model);
        }


        public async Task<IActionResult> Delete(int? id)
             {
                 if (id == null || _context.Players == null)
                 {
                     return NotFound();
                 }

            var player = await _context.Players.FindAsync(id);
            player.Position = _context.Positions.FirstOrDefault(cp => cp.Position_ID == player.Positionid);

            List<string> catnames = _context.Categories.Where(e => player.SelectedCategoryIds.Contains(e.Category_ID)).Select(e => e.Category_Name).ToList();

            if (player == null)
                 {
                     return NotFound();
                 }
            ViewBag.CatNames = catnames;
            return View(player);
         }


        [HttpPost, ActionName("Delete")]
             [ValidateAntiForgeryToken]
             public async Task<IActionResult> DeleteConfirmed(int? id)
             {
                 if (_context.Players == null)
                 {
                     return Problem("Entity set 'DataContext.Players'  is null.");
                 }
                 var player = await _context.Players.FindAsync(id);
                 if (player != null)
                 {
                     _context.Players.Remove(player);
                 }

                 await _context.SaveChangesAsync();
            _imageHelper.DeleteImage(player.Player_Image, "Players");
            TempData["successPlayer"] = "Jugador " + player.Player_FullName + " eliminado!!";
                 return RedirectToAction(nameof(Index));
             }



        public async Task<IActionResult> DarBaja(int id)
        {
            if (id == null || _context.Players == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .FirstOrDefaultAsync(m => m.Player_ID == id);
            if (player == null)
            {
                return NotFound();
            }
            try
            {
                player.Player_Status = false;

                _context.Update(player);
                await _context.SaveChangesAsync();

                TempData["successPlayer"] = "Jugador " + player.Player_FullName + " de baja!!";
                return RedirectToAction("Index", "Players");
            }
            catch (Exception)
            {
                return View(player);
            }

            //
        }

        public async Task<IActionResult> DarAlta(int id)
        {
            if (id == null || _context.Players == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .FirstOrDefaultAsync(m => m.Player_ID == id);
            if (player == null)
            {
                return NotFound();
            }
            try
            {
                player.Player_Status = true;

                _context.Update(player);
                await _context.SaveChangesAsync();

                TempData["successPlayer"] = "Jugador " + player.Player_FullName + " de alta!!";
                return RedirectToAction("Index", "Players");
            }
            catch (Exception)
            {
                return View(player);
            }
        }

      

        public JsonResult CountryDrop()
        {
            var cnt = _context.Countries.ToList();
            return new JsonResult(cnt);
        }

        public JsonResult StateDrop(int id)
        {
            var st = _context.States.Where(e => e.CountryId == id).ToList();
            return new JsonResult(st);
        }

        public JsonResult CityDrop(int id)
        {
            var ct = _context.Cities.Where(e => e.IdState == id).ToList();
            return new JsonResult(ct);
        }




        #endregion


        #region PLAYERFILES

        public async Task<IActionResult> AddPlayerFiles(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                Player player = await _context.Players.FindAsync(id);
                if (player == null)
                {
                    return NotFound();
                }

                PlayerFilesViewModel model = new()
                {
             
                    PlayerName = player.Player_FullName,
                    PlayerId = player.Player_ID,
                };
                ViewBag.EditFlag = "";
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AddPlayerFiles(PlayerFilesViewModel model)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        PlayerFiles pfiles = new()
                        {
                          //  Cities = new List<City>(),
                          //  Player = await _context.Players.FindAsync(model.PlayerId),
                            PlayerFiles_Name = model.PlayerFiles_Name.ToUpper(),
                            PlayerFiles_Image = _imageHelper.UploadImage(model.FotoFile, "Files"),
                            PlayerId = model.PlayerId,
                        };
                        _context.PlayerFiles.Add(pfiles);
                        try
                        {
                            await _context.SaveChangesAsync();
                            TempData["successState"] = "Archivo " + pfiles.PlayerFiles_Name + " creado!!";
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        Player player = await _context.Players
                            .Include(c => c.PlayerFiles)
                          //  .ThenInclude(s => s.Cities)
                            .FirstOrDefaultAsync(c => c.Player_ID == model.PlayerId);
                        return RedirectToAction(nameof(Details), new { Id = model.PlayerId });
                    }
                 /*   catch (DbUpdateException dbUpdateException)
                    {
                        if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                        {
                            ModelState.AddModelError(String.Empty, "Ya existe un departamento con el mismo nombre en este país");
                        }
                        else
                        {
                            ModelState.AddModelError(String.Empty, dbUpdateException.InnerException.Message);
                        }
                    }*/
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",ex.Message);
                    }
                }
                return View(model);
            }


            public async Task<IActionResult> DetailsPlayerFiles(int? id) {

                if (id == null || _context.PlayerFiles == null)
                {
                    return NotFound();
                }

                PlayerFiles pfiles = await _context.PlayerFiles
                    .Include(cp=>cp.Player)
                    .FirstOrDefaultAsync(m => m.PlayerFiles_ID == id);
                if (pfiles == null)
                {
                    return NotFound();
                }

                return View(pfiles);

            }


            public async Task<IActionResult> EditPlayerFiles(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                PlayerFiles pfiles = await _context.PlayerFiles.FindAsync(id);
                pfiles.Player = _context.Players.FirstOrDefault(cp => cp.Player_ID == pfiles.PlayerId);
                if (pfiles == null)
                {
                    return NotFound();
                }
                PlayerFilesViewModel model = new PlayerFilesViewModel
                {
                    PlayerFiles_Image = pfiles.PlayerFiles_Image,
                    PlayerFiles_Name = pfiles.PlayerFiles_Name,
                    PlayerName = pfiles.Player.Player_FullName,
                    PlayerId = pfiles.PlayerId,
                    PlayerFiles_ID = pfiles.PlayerFiles_ID,
                };
                ViewBag.EditFlag = "flag";
                return View(model);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> EditPlayerFiles(PlayerFilesViewModel model)
            {
                if (model.PlayerFiles_ID == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        try
                        {
                            PlayerFiles pfilesondb = await _context.PlayerFiles.FindAsync(model.PlayerFiles_ID);
                       
                            if (pfilesondb != null)
                            {
                                if (model.FotoFile != null)//si carga archivo
                                {
                                    if (!string.IsNullOrEmpty(pfilesondb.PlayerFiles_Image))
                                    {
                                        if (_imageHelper.DeleteImage(pfilesondb.PlayerFiles_Image, "Files"))
                                        {
                                            model.PlayerFiles_Image = _imageHelper.UploadImage(model.FotoFile, "Files");
                                        }
                                        else
                                        {
                                            return RedirectToAction(nameof(Details), new { Id = model.PlayerId });
                                        }
                                    }
                                    else
                                    {
                                        model.PlayerFiles_Image = _imageHelper.UploadImage(model.FotoFile, "Files");
                                    }
                                }
                                PlayerFiles files = new()
                                {
                                    PlayerFiles_Name = model.PlayerFiles_Name,
                                    PlayerFiles_Image = model.PlayerFiles_Image,
                                    PlayerId = model.PlayerId,
                                    PlayerFiles_ID = model.PlayerFiles_ID,                           
                                };
                                _context.Update(files);
                                await _context.SaveChangesAsync();

                                TempData["successPlayer"] = "Archivo " + files.PlayerFiles_Name + " editado!!";
                                return RedirectToAction(nameof(Details), new { Id = model.PlayerId });
                            }
                            else
                            {
                                // El objeto PlayerFiles no fue encontrado en la base de datos
                                // Puedes manejar esta situación según tus necesidades
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al buscar PlayerFiles: {ex.Message}");
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                    // return RedirectToAction(nameof(Index));
                }
                // ViewBag.Genero = _combos.GetComboGeneros();
                return View(model);
            }

            public async Task<IActionResult> DeletePlayerFiles(int? id)
            {
                if (id == null || _context.PlayerFiles == null)
                {
                    return NotFound();
                }

                var pfiles = await _context.PlayerFiles.FindAsync(id);
                pfiles.Player = _context.Players.FirstOrDefault(cp => cp.Player_ID == pfiles.PlayerId);
            


                if (pfiles == null)
                {
                    return NotFound();
                }
     
                return View(pfiles);
            }


            [HttpPost, ActionName("DeletePlayerFiles")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmedPlayerFiles(int? id)
            {
                if (_context.PlayerFiles == null)
                {
                    return Problem("Entity set 'DataContext.PlayersFiles'  is null.");
                }
                var pfiles = await _context.PlayerFiles.FindAsync(id);
                if (pfiles != null)
                {
                    _context.PlayerFiles.Remove(pfiles);
               
                }

                await _context.SaveChangesAsync();
                _imageHelper.DeleteImage(pfiles.PlayerFiles_Image, "Files");
                TempData["successPlayer"] = "Archivo " + pfiles.PlayerFiles_Name + " eliminado!!";
                return RedirectToAction(nameof(Details), new { Id = pfiles.PlayerId });
            }



        #endregion


        #region PARENTS


        public async Task<IActionResult> IndexParents() {

            IActionResult response = View(await _context
             .Parents
             .Include(t => t.Player)
             .OrderBy(t => t.Parent_LastName)
             .ToListAsync());

            return response;
        }


        public async Task<IActionResult> AddParent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Player player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            ParentViewModel model = new()
            {
             PlayerId = player.Player_ID,
             PlayerName = player.Player_FullName
            };

            model.Countries = _combos.GetCombosCountries();
          //  model.CountryID = oparent.Country.Country_ID;

          //  model.States = _combos.GetCombosStates();
          //  model.StateID = oparent.State.State_ID;

         //   model.Cities = _combos.GetCombosCities();
            //   model.CityID = oparent.City.City_ID;
            //  model.PlayerId = id;
            //  model.PlayerName = oparent.Player_FullName;
            ViewBag.EditFlag = "";
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddParent(ParentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Parent parent = new Parent()
                    {
                      //  Parent_ID = Guid.NewGuid().ToString(),
                        Player = await _context.Players.FindAsync(model.Parent_ID),
                        Parent_FirstName = model.Parent_FirstName,
                        Parent_LastName = model.Parent_LastName,
                        Parent_Cedula = model.Parent_Cedula,
                        PhoneNumber = model.PhoneNumber,
                        Parent_Address = model.Parent_Address,
                        Parent_Image = _imageHelper.UploadImage(model.FotoFile, "Parents"),
                        Parent_ImageCedula = _imageHelper.UploadImage(model.FotoFileCedula, "Files"),
                        
                        CityID = model.CityID,/* await _context.Cities.FindAsync(model.CityID),*/
                        CountryID = model.CountryID, /*await _context.Countries.FindAsync(model.CountryID),*/
                        StateID = model.StateID, /*await _context.States.FindAsync(model.StateID),*/
                        
                        PlayerId = model.PlayerId,
                    };

                    _context.Add(parent);
                    try
                    {
                        await _context.SaveChangesAsync();
                        TempData["successPlayer"] = "Tutor " + parent.Parent_FullName + " creado!!";
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                   /* Player player = await _context.Players
                        .Include(c => c.State)
                        .ThenInclude(s => s.Cities)
                        .FirstOrDefaultAsync(c => c. == model.CountryID);*/
                    return RedirectToAction(nameof(Details), new { Id = model.PlayerId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un tutor con el mismo nombre en este jugador");
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
            model.Countries = _combos.GetCombosCountries();
            //  model.CountryID = oparent.Country.Country_ID;

            model.States = _combos.GetCombosStates();
            //  model.StateID = oparent.State.State_ID;

            model.Cities = _combos.GetCombosCities();
            return View(model);

        }


        public async Task<IActionResult> DetailsParent(int? id)
        {

            if (id == null || _context.Parents == null)
            {
                return NotFound();
            }

            Parent parent = await _context.Parents
                .Include(cp => cp.Player)
                .FirstOrDefaultAsync(m => m.Parent_ID == id);

            if (parent == null)
            {
                return NotFound();
            }

            string depname = _context.States.Where(c => c.State_ID == parent.StateID).Select(y => y.State_Name).FirstOrDefault();
            string munname = _context.Cities.Where(c => c.City_ID == parent.CityID).Select(y => y.City_Name).FirstOrDefault();

            ViewBag.Depa = depname;
            ViewBag.Muni = munname;

            return View(parent);

        }


        public async Task<IActionResult> EditParent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Parent parent = await _context.Parents.FindAsync(id);
            parent.Player = _context.Players.FirstOrDefault(cp => cp.Player_ID == parent.PlayerId);
            if (parent == null)
            {
                return NotFound();
            }
            ParentViewModel model = new()
            {
                Parent_FirstName = parent.Parent_FirstName,
                Parent_LastName = parent.Parent_LastName,
                Parent_Address = parent.Parent_Address,
                Parent_Cedula = parent.Parent_Cedula,
                PlayerName = parent.Player.Player_FullName,
                PlayerId = parent.PlayerId,
                Parent_ID = parent.Parent_ID,
                Parent_Image = parent.Parent_Image,
                Parent_ImageCedula = parent.Parent_ImageCedula,


            };

            model.Countries = _combos.GetCombosCountries();
            model.States = _combos.GetCombosStates();
            model.Cities = _combos.GetCombosCities();

            model.CountryID = parent.CountryID;
            model.StateID = parent.StateID;
            model.CityID = parent.CityID;

            ViewBag.EditFlag = "flag";
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditParent(ParentViewModel model)
        {
            if (model.Parent_ID == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    try
                    {
                    Parent parentondb = await _context.Parents.FindAsync(model.Parent_ID);

                        if (parentondb != null)
                        {
                            if (model.FotoFile != null)//si carga archivo
                            {
                                if (!string.IsNullOrEmpty(parentondb.Parent_Image))
                                {
                                    if (_imageHelper.DeleteImage(parentondb.Parent_Image, "Parent"))
                                    {
                                        model.Parent_Image = _imageHelper.UploadImage(model.FotoFile, "Parent");
                                    }
                                    else
                                    {
                                        return RedirectToAction(nameof(Details), new { Id = model.PlayerId });
                                    }
                                }
                                else
                                {
                                    model.Parent_Image = _imageHelper.UploadImage(model.FotoFile, "Parent");
                                }
                            }
                        if (model.FotoFileCedula != null)//si carga archivo
                        {
                            if (!string.IsNullOrEmpty(parentondb.Parent_ImageCedula))
                            {
                                if (_imageHelper.DeleteImage(parentondb.Parent_ImageCedula, "Files"))
                                {
                                    model.Parent_ImageCedula = _imageHelper.UploadImage(model.FotoFile, "Files");
                                }
                                else
                                {
                                    return RedirectToAction(nameof(Details), new { Id = model.PlayerId });
                                }
                            }
                            else
                            {
                                model.Parent_ImageCedula = _imageHelper.UploadImage(model.FotoFile, "Files");
                            }
                        }
                        Parent parent = new()
                            {
                                Parent_FirstName = model.Parent_FirstName,
                                Parent_LastName = model.Parent_LastName,
                                Parent_Cedula = model.Parent_Cedula,
                                Parent_Address = model.Parent_Address,
                                PlayerId = model.PlayerId,
                                Parent_ID = model.Parent_ID,
                                PhoneNumber = model.PhoneNumber,
                                CountryID = model.CountryID,
                                StateID = model.StateID,
                                CityID = model.CityID,
                            };
                            _context.Update(parent);
                            await _context.SaveChangesAsync();

                            TempData["successPlayer"] = "Tutor " + parent.Parent_FullName + " editado!!";
                            return RedirectToAction(nameof(Details), new { Id = model.PlayerId });
                        }
                        else
                        {
                            // El objeto PlayerFiles no fue encontrado en la base de datos
                            // Puedes manejar esta situación según tus necesidades
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al buscar Parent: {ex.Message}");
                    }
                // return RedirectToAction(nameof(Index));
            }
            // ViewBag.Genero = _combos.GetComboGeneros();
            return View(model);
        }


        public async Task<IActionResult> DeleteParent(int? id)
        {
            if (id == null || _context.Parents == null)
            {
                return NotFound();
            }

            var parent = await _context.Parents.FindAsync(id);
            parent.Player = _context.Players.FirstOrDefault(cp => cp.Player_ID == parent.PlayerId);
            // parent.Par = _context.Parents.FirstOrDefault(cp => cp.Parent_ID == parent.PlayerId);

            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }


        [HttpPost, ActionName("DeleteParent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedParent(int? id)
        {
            if (_context.Parents == null)
            {
                return Problem("Entity set 'DataContext.Parents'  is null.");
            }
            var parent = await _context.Parents.FindAsync(id);
            if (parent != null)
            {
                _context.Parents.Remove(parent);

            }

            await _context.SaveChangesAsync();
            _imageHelper.DeleteImage(parent.Parent_Image, "Parents");
            _imageHelper.DeleteImage(parent.Parent_ImageCedula, "Files");
            TempData["successPlayer"] = "Tutor " + parent.Parent_FullName + " eliminado!!";
            return RedirectToAction(nameof(Details), new { Id = parent.PlayerId });
        }



        #endregion


        #region CorrectActions


        public async Task<IActionResult> AddCorrectActions(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Player player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            CorrectActionViewModel model = new()
            {
                PlayerId = player.Player_ID,
                PlayerName = player.Player_FullName
            };

           // model.Countries = _combos.GetCombosCountries();
            //  model.CountryID = oparent.Country.Country_ID;

            //  model.States = _combos.GetCombosStates();
            //  model.StateID = oparent.State.State_ID;

            //   model.Cities = _combos.GetCombosCities();
            //   model.CityID = oparent.City.City_ID;
            //  model.PlayerId = id;
            //  model.PlayerName = oparent.Player_FullName;
            ViewBag.EditFlag = "";
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCorrectActions(CorrectActionViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CorrectionAction ca = new()
                    {
                        //  Parent_ID = Guid.NewGuid().ToString(),
                       // Player = await _context.Players.FindAsync(model.PlayerId),
                        PlayerId = model.PlayerId,
                        CorrectionAction_Name = model.CorrectionAction_Name,
                        Description = model.Description,
                        Fecha = model.Fecha,
                        CorrectionAction_Status = model.CorrectionAction_Status,
                        PlayerName = model.PlayerName,

                    };

                    _context.Add(ca);
                    try
                    {
                        await _context.SaveChangesAsync();
                        TempData["successPlayer"] = "Sanción agregada a " + ca.PlayerName + " !!";
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    /* Player player = await _context.Players
                         .Include(c => c.State)
                         .ThenInclude(s => s.Cities)
                         .FirstOrDefaultAsync(c => c. == model.CountryID);*/
                    return RedirectToAction(nameof(Details), new { Id = model.PlayerId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un tutor con el mismo nombre en este jugador");
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


        public async Task<IActionResult> EditCorrectAction(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CorrectionAction ca = await _context.CorrectionActions.FindAsync(id);
            ca.Player = _context.Players.FirstOrDefault(cp => cp.Player_ID == ca.PlayerId);
            if (ca == null)
            {
                return NotFound();
            }
            CorrectActionViewModel model = new()
            {
                //  Parent_ID = Guid.NewGuid().ToString(),
                // Player = await _context.Players.FindAsync(model.PlayerId),
                PlayerId = ca.PlayerId,
                CorrectionAction_Name = ca.CorrectionAction_Name,
                Description = ca.Description,
                Fecha = ca.Fecha,
                CorrectionAction_Status = ca.CorrectionAction_Status,
                PlayerName = ca.PlayerName,

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCorrectAction(CorrectActionViewModel model)
        {
            if (model.CorrectionAction_ID == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                      CorrectionAction ca = new()
                        {
                          PlayerId = model.PlayerId,
                          CorrectionAction_Name = model.CorrectionAction_Name,
                          Description = model.Description,
                          Fecha = model.Fecha,
                          CorrectionAction_Status = model.CorrectionAction_Status,
                          PlayerName = model.PlayerName,
                      };
                        _context.Update(ca);
                        await _context.SaveChangesAsync();

                        TempData["successPlayer"] = "Tutor " + ca.CorrectionAction_Name + " editado!!";
                        return RedirectToAction(nameof(Details), new { Id = model.PlayerId });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al buscar CA: {ex.Message}");
                }
                // return RedirectToAction(nameof(Index));
            }
            // ViewBag.Genero = _combos.GetComboGeneros();
            return View(model);
        }


        public async Task<IActionResult> DeleteCorrectAction(int? id)
        {
            if (id == null || _context.CorrectionActions == null)
            {
                return NotFound();
            }

            var ca = await _context.CorrectionActions.FindAsync(id);
            ca.Player = _context.Players.FirstOrDefault(cp => cp.Player_ID == ca.PlayerId);
            // parent.Par = _context.Parents.FirstOrDefault(cp => cp.Parent_ID == parent.PlayerId);

            if (ca == null)
            {
                return NotFound();
            }

            return View(ca);
        }


        [HttpPost, ActionName("DeleteCorrectAction")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedCorrectAction(int? id)
        {
            if (_context.CorrectionActions == null)
            {
                return Problem("Entity set 'DataContext.CorrectionActions'  is null.");
            }
            var ca = await _context.CorrectionActions.FindAsync(id);
            if (ca != null)
            {
                _context.CorrectionActions.Remove(ca);

            }

            await _context.SaveChangesAsync();
            TempData["successPlayer"] = "Tutor " + ca.CorrectionAction_Name + " eliminado!!";
            return RedirectToAction(nameof(Details), new { Id = ca.PlayerId });
        }

        public async Task<IActionResult> DetailsCorrectAction(int? id)
        {

            if (id == null || _context.CorrectionActions == null)
            {
                return NotFound();
            }

            CorrectionAction ca = await _context.CorrectionActions
                .Include(cp => cp.Player)
                .FirstOrDefaultAsync(m => m.CorrectionAction_ID == id);

            if (ca == null)
            {
                return NotFound();
            }

            /* string depname = _context.States.Where(c => c.State_ID == parent.StateID).Select(y => y.State_Name).FirstOrDefault();
             string munname = _context.Cities.Where(c => c.City_ID == parent.CityID).Select(y => y.City_Name).FirstOrDefault();

             ViewBag.Depa = depname;
             ViewBag.Muni = munname;*/
            ViewBag.Cats = _combos.GetCategorias();
            return View(ca);

        }

        #endregion






    }
}
