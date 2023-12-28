using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mailjet.Client.Resources;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Helpers.Interfaces;
using xilopro2.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Identity;
using Azure.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Security.Principal;

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

            usuarioensesion = await _userHelper.GetUserAsync(User.Identity.Name.ToString());
            List<int> filtroIds = usuarioensesion.SelectedCategoryIds;
           
            // var catId = Convert.ToInt32(usuarioensesion.SelectedCategoryIds);
            //Debug.WriteLine($"sesion de: {filtroIds}");

            if (User.IsInRole("Editor")|| User.IsInRole("Dt"))
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
                    .AsEnumerable()
                   // .Where(player => player.SelectedCategoryIds.Intersect(filtroIds).Any())
                   .Where(player => player.SelectedCategoryIds.Any(id => filtroIds.Contains(id)))
                  .ToList();
                if (filteredPlayers == null)
                {
                    Problem("Entity set 'DataContext.Players'  is null.");
                }
                else
                {
                    return View(filteredPlayers);
                }

                // Pasar los jugadores filtrados a la vista
               
            }
           



            IActionResult response = _context.Categories != null ?
                    View(await _context.Players
                    .ToListAsync()) :
                    Problem("Entity set 'DataContext.Categories'  is null.");
            return response;

        }


        // GET: Players/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
          //  Data.Entities.User user = await _userHelper.GetUserAsync(Guid.Parse(id));
     

           Data.Entities.AppUser player = await _context.Users
              //  .Include(c => c.Category)
               // .Include(c => c.PlayerFiles)
               // .Include(c => c.Parents)
              //  .Include(c => c.Team)
              //  .Include(c => c.Position)
               // .Include(c => c.Country)
              /*  .Include(c => c.City)
                .Include(s => s.State)*/
                .FirstOrDefaultAsync(m => m.Id == id);

            if (player == null)
            {
                return NotFound();
            }

          //  PlayerViewModel model = _swithUsers.ToPlayerViewModel(player);

            /*  string catname = (from obj0 in _context.Players
                                join categoria in _context.Categories on obj0.Category.Category_ID equals categoria.Category_ID
                                select categoria.Category_Name).FirstOrDefault();

          //  string catname = _context.Categories.Where(c => c.Category_ID == model.Categoryid).Select(y => y.Category_Name).FirstOrDefault();
            string painame = _context.Countries.Where(c => c.Country_ID == model.CountryID).Select(y => y.Country_Name).FirstOrDefault();
            string depname = _context.States.Where(c => c.State_ID == model.StateID).Select(y => y.State_Name).FirstOrDefault();
            string munname = _context.Cities.Where(c => c.City_ID == model.CityID).Select(y => y.City_Name).FirstOrDefault();
            string pos = _context.Positions.Where(c => c.Position_ID == model.Positionid).Select(y => y.Position_Name).FirstOrDefault();
            string team = _context.Teams.Where(c => c.Team_ID == model.Teamid).Select(y => y.Team_Name).FirstOrDefault();

            ViewData["team"] = team;
          //  ViewData["CatName"] = catname;
            ViewData["Pos"] = pos;
            ViewData["PaisName"] = painame;
            ViewData["DepartamentoName"] = depname;
            ViewData["MunicipioName"] = munname;*/

            return View(player);
        }

        // GET: Players/Create
        public async Task<IActionResult> Create()
        {
            usuarioensesion = await _userHelper.GetUserAsync(User.Identity.Name.ToString());
            List<int> filtroIdsCateg = usuarioensesion.SelectedCategoryIds;

            List<string> miListaDeStrings = new List<string>
                {
                    "Seleccionar",
                    "Masculino",
                    "Femenino"
                };

            PlayerViewModel model = new PlayerViewModel
            {
                //Teamid = 1,
                //  UserType = _combos.GetCombosRoles(),
                States = _combos.GetCombosStates(),
                Countries = _combos.GetCombosCountries(),
                Cities = _combos.GetCombosCities(),

                Categories = _combos.GetCategoriasPorIds(filtroIdsCateg),
                Teams = _combos.GetCombosEquipos(),
                Positions = _combos.GetCombosPosiciones(),
                

               // Player_ID = "null",
                //Player_Genero =  _combos.GetComboGeneros(),
               // Player_ID = Guid.NewGuid().ToString(),
            };
            ViewBag.Genero = miListaDeStrings;
            return View(model); ;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerViewModel model)
        {
            // player.Teamid = 1;
            if (ModelState.IsValid)
            {
              //pasamos lso datos del modelo al User entity
                                                                                 //subimos imagen de user
              //  AppUser useridentity =  _swithUsers.FromPlayerToAppUser(newuserobj,true);
               // useridentity.Email = newuserobj.Player_ID;

              //  var resultado = await _userHelper.AddUserAsync(useridentity, "123456");//creamos usaurio identity
                if (/*resultado.Succeeded*/true)
                {
                  //  await _userHelper.AddUserToRoleAsync(useridentity, newuserobj.Player_UserRol);//asignamos el rol User


                    /*    AppUser newPlayerObj = new AppUser
                        {
                            Id = user.Id,
                            User_FirstName = model.Player_FirstName,
                            User_Address = model.Player_Address,
                            User_LastName = model.Player_LastName,
                            User_Status = model.Player_Status,
                            Player_Dorsal = model.Player_Dorsal,
                            User_FNC = model.Player_FNC,
                            PhoneNumber = model.PhoneNumber,
                            Email = model.Player_Email,
                            User_Cedula = model.Player_Cedula ?? "0000000000000L",
                            Player_fifaid = model.Player_fifaid ?? "00000",
                            User_Genero = model.Player_Genero,
                            User_Image = null,
                            Player_Image = _imageHelper.UploadImage(model.FotoFile, "Players"),
                            UserTypeofRole = "User",
                            SelectedCategoryIds = model.SelectedCategoryIds,

                            Countryid = model.CountryID,
                            Stateid = model.StateID,
                            Cityid = model.CityID,
                            Position = await _context.Positions.FindAsync(model.Positionid),
                             Team = await _context.Teams.FindAsync(model.Teamid),

                        };*/

                    //  Player newPlayerObj = await _swithUsers.ModelToPlayer(model,true);//pasamos lso datos del modelo a la entidad Player
                   // newuserobj.Player_ID = user.Id;
                      /*   newPlayerObj.Player_Image = _imageHelper.UploadImage(model.FotoFile, "Players");//subimos imagen a player*/
                    //   _context.Players.Add(newPlayerObj);

                    try
                    {
                        // Player newuserobj = await _swithUsers.ModelToPlayer(model, true);
                        Player newuserobj = new Player
                        {
                            // Player_ID = isNew ? Guid.NewGuid().ToString() : model.Player_ID,
                            Player_FirstName = model.Player_FirstName,
                            Player_Address = model.Player_Address,
                            Player_LastName = model.Player_LastName,
                            Player_Status = model.Player_Status,
                            Player_Dorsal = model.Player_Dorsal,
                            Player_FNC = model.Player_FNC,
                            Player_PhoneNumber = model.Player_PhoneNumber,
                            Player_Email = model.Player_Email,
                            Player_Cedula = model.Player_Cedula,
                            Player_fifaid = model.Player_fifaid,
                            Player_Genero = model.Player_Genero,

                            Player_Image = _imageHelper.UploadImage(model.FotoFile, "Players"),
                            SelectedCategoryIds = model.SelectedCategoryIds,

                            Countryid = model.CountryID,
                            Stateid = model.StateID,
                            Cityid = model.CityID,
                            
                            Country = await _context.Countries.FindAsync(model.CountryID),
                            Position = await _context.Positions.FindAsync(model.Positionid),
                            Team = await _context.Teams.FindAsync(model.Teamid),

                        };
                        _context.Add(newuserobj);
                            await _context.SaveChangesAsync();

                        TempData["successUser"] = "Jugador " + newuserobj.Player_FullName + " creado!!";
                        return RedirectToAction("Index", "Players");
                    }
                    catch (DbUpdateException ex)
                    {
                        if (ex.InnerException.Message.Contains("duplicate"))
                        {
                            ModelState.AddModelError(string.Empty, "Jugador Ya existe");
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

            }
            model.Teams = _combos.GetCombosEquipos();
            model.Positions = _combos.GetCombosPosiciones();
            model.Categories = _combos.GetComboCategorias();

            ViewBag.Opciones = _combos.GetComboGeneros();

            model.States = _combos.GetCombosStates();
            model.Countries = _combos.GetCombosCountries();
            model.Cities = _combos.GetCombosCities();

            return View(model);
        }

        // GET: Players/Edit/5
      /*  public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

         //   Player player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewBag.Opciones = _combos.GetComboGeneros();
         //   PlayerViewModel model =  _swithUsers.ToPlayerViewModel(player);
            return View(model);
        }*/

       [HttpPost]
        [ValidateAntiForgeryToken]
   /*      public async Task<IActionResult> Edit(PlayerViewModel model)
        {
           if (id != model.Player_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                 //   Player playerondb = new Player();
                    Player playerondb = await _context.Players.FindAsync(model.Player_ID);
                   
                    if (model.FotoFile != null)
                    {//si carga archivo
                        
                        if (!string.IsNullOrEmpty(playerondb.Player_Image))
                        {
                            if (_imageHelper.DeleteImage(playerondb.Player_Image, "Players"))
                            {
                                model.Player_Image = _imageHelper.UploadImage(model.FotoFile, "Players");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Players");
                            }
                        }
                        else
                        {
                            model.Player_Image = _imageHelper.UploadImage(model.FotoFile, "Players");
                        }
                    }

                    var player = await _swithUsers.ModelToPlayer(model, false);
                    player.Player_Image = playerondb.Player_Image;

                    _context.Update(player);
                    await _context.SaveChangesAsync();
                    TempData["successPlayer"] = "Jugador " + player.Player_FullName + " editado!!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(model.Player_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               // return RedirectToAction(nameof(Index));
            }
            ViewBag.Opciones = _combos.GetComboGeneros();
            return View(model);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(string id)
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

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
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
            TempData["successPlayer"] = "Jugador " + player.Player_FullName + " eliminado!!";
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(string id)
        {
          return (_context.Players?.Any(e => e.Player_ID == id)).GetValueOrDefault();


        }*/


        #endregion


        #region PARENTS

        public async Task<IActionResult> CreateParent(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Player oparent=null; /*= await _context.Players.FindAsync(id);*/
            if (oparent == null)
            {
                return NotFound();
            }

            ParentViewModel model = new ParentViewModel();

            model.Countries = _combos.GetCombosCountries();
          //  model.CountryID = oparent.Country.Country_ID;


            model.States = _combos.GetCombosStates();
          //  model.StateID = oparent.State.State_ID;

            model.Cities = _combos.GetCombosCities();
            //   model.CityID = oparent.City.City_ID;
            model.PlayerId = id;
            model.PlayerName = oparent.Player_FullName;
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateParent(ParentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Parent parent = new()
                    {
                        Parent_ID = Guid.NewGuid().ToString(),
                      //  Player = await _context.Players.FindAsync(model.ID),
                        Parent_FirstName = model.Parent_FirstName,
                        Parent_LastName = model.Parent_LastName,
                        Parent_Cedula = model.Parent_Cedula,
                        PhoneNumber = model.PhoneNumber,
                        Parent_Address = model.Parent_Address,
                        Parent_Image = _imageHelper.UploadImage(model.FotoFile, "Parents"),
                        Parent_ImageCedula = _imageHelper.UploadImage(model.FotoFileCedula, "Files"),
                        
                        CityID = model.CityID,/* await _context.Cities.FindAsync(model.CityID),*/
                        CountryID = model.CountryID, /*await _context.Countries.FindAsync(model.CountryID),*/
                        StateID = model.StateID /*await _context.States.FindAsync(model.StateID),*/

                        

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
            return View(model);

        }

        #endregion






    }
}
