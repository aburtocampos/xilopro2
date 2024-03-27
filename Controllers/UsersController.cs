using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Globalization;
using System.Security.Claims;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Enums;
using xilopro2.Helpers.Interfaces;
using xilopro2.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace xilopro2.Controllers
{
    public class UsersController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly ICombos _combos;
        private readonly ISwithUsers _swithUsers;
        private readonly IImageHelper _imageHelper;

        public UsersController(DataContext context, IUserHelper userHelper, ICombos _combosHelper, ISwithUsers _swithUsersHelper, IImageHelper imageHelper/*, UserManager<IdentityUser> userManager*/)
        {
            _dataContext = context;
            _userHelper = userHelper;
            _combos = _combosHelper;
            _swithUsers = _swithUsersHelper;
            _imageHelper = imageHelper;
            // _userManager = userManager;
        }


        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Index()
        {
           // var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _dataContext.Users
                .Where(x => x.UserTypeofRole != "Admin" /*&& x.Id != loggedInUserId*/)
                .OrderBy(x => x.User_LastName)
                .ToListAsync();

            return View(response);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var user = await _userHelper.GetUserAsyncbyGuid(Guid.Parse(id));

            UserViewModel model = _swithUsers.ToUserViewModel(user);

            List<string> catnames = _dataContext.Categories.Where(e => user.SelectedCategoryIds.Contains(e.Category_ID)).Select(e => e.Category_Name).ToList();
            string painame = _dataContext.Countries.Where(c => c.Country_ID == user.Countryid).Select(y => y.Country_Name).FirstOrDefault();
            string depname = _dataContext.States.Where(c => c.State_ID == user.Stateid).Select(y => y.State_Name).FirstOrDefault();
            string munname = _dataContext.Cities.Where(c => c.City_ID == user.Cityid).Select(y => y.City_Name).FirstOrDefault();

            // Calcular la edad
            ViewBag.Edad = DateTime.Today.Year - model.User_FNC.Year -
               (DateTime.Today.Month < model.User_FNC.Month ||
                (DateTime.Today.Month == model.User_FNC.Month && DateTime.Today.Day < model.User_FNC.Day) ? 1 : 0);

            ViewData["RoleName"] = user.UserTypeofRole;
            ViewBag.CatNames = catnames;
            ViewData["PaisName"] = painame;
            ViewData["DepartamentoName"] = depname;
            ViewData["MunicipioName"] = munname;

            return View(model);

        }

        [Authorize(Roles = "Admin,Editor")]
        public IActionResult CreateUser()
        {

            UserViewModel model = new UserViewModel
            {
                UserType = User.IsInRole("Editor") ? _combos.GetCombosRolesunicos() : _combos.GetCombosRoles(),
                Categories = _combos.GetCategorias(),
                Countries = _combos.GetCombosCountries(),
              //  States = _combos.GetCombosStates(),
              //  Cities = _combos.GetCombosCities(),
                Id = Guid.NewGuid().ToString(),
                SelectedCategoryIds = new List<int>(),
            };
            ViewBag.Genero = _combos.GetComboGeneros();
            return View(model);
        }


        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
            //  Data.Entities.User user = await _userHelper.GetUserAsync(model.Email);
                //   nuser = await _swithUsers.ToUserAsync(model, true);

                   user = new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = model.Email,
                        User_FirstName = model.User_FirstName?.Trim().ToUpper(),
                       User_LastName = model.User_LastName?.Trim().ToUpper(),
                       User_Address = model.User_Address,
                        PhoneNumber = model.PhoneNumber,
                        UserTypeofRole = _userHelper.GetRoleNameByID(model.UserTypeof.ToString()),
                        User_Cedula = model.User_Cedula,
                        UserName = model.Email,
                        User_FNC = model.User_FNC,
                        // Id = Guid.NewGuid().ToString(),
                        User_Status = model.Status,

                        User_Image = _imageHelper.UploadImage(model.FotoFile, "Users"),

                        Countryid = model.Countryid,
                        Stateid = model.Stateid,
                        Cityid = model.Cityid,
                        User_Genero = model.User_Genero,
                        /*  Cateroriaid = model.Categoryid,
                          Category = await _context.Categories.FindAsync(model.Categoryid),*/
                        
                        User_CreatedTime = DateTime.Now,
                        SelectedCategoryIds = model.SelectedCategoryIds,
                        /*    UserCategories = model.SelectedCategoryIds
                            .Select(i => new UserCategory { User = user, Category = _dataContext.Categories.Find(i) })
                                .ToList(),

                           UserCategory = new List<UserCategory>
                               {
                                   new UserCategory { User = user, Category = _dataContext.Categories.Find(1) },

                               },*/

                        //  Categories = _context.Categories.Where(c => c.Category_ID == 1).ToList(),
                        //Categories = _dataContext.Categories.AsNoTracking().Where(c => model.SelectedCategoryIds.Contains(c.Category_ID)).ToList(),

                    };

                    // creating user on identity
                    var resultado = await _userHelper.AddUserAsync(user, "123456");
                    if (resultado.Succeeded) {
                        await _userHelper.AddUserToRoleAsync(user, user.UserTypeofRole);

                      //  var categorias = _dataContext.Categories.Where(c => model.SelectedCategoryIds.Contains(c.Category_ID)).ToList();

                     //   if (categorias.Any()){
                            // Crea nuevas entradas en la tabla de relación
                           /* var userCategorias = categorias.Select(categoria => new UserCategoria
                            {
                                UserId = nuser.Id,
                                Category = categorias,
                                User = nuser
                            }).ToList();*/

                        /*    foreach (int categoriaIds in model.SelectedCategoryIds)
                            {
                                //var userCategoria = new UserCategoria
                                //{
                                Category a = _dataContext.Categories.Find(categoriaIds);
                               
                               // };
                               
                            }*/

                            // Agrega las nuevas entradas a la base de datos
                          //  _dataContext.Users.Add(userCategorias);

                          //  await _dataContext.SaveChangesAsync();

                            TempData["successUser"] = "Usuario " + user.User_FullName + " creado!!";
                            return RedirectToAction("Index", "Users");
                      //  }

                    }
                   

                

                /*  user.User_Image = _imageHelper.UploadImage(model.FotoFile, "Users");
                user.UserTypeofRole = _userHelper.GetRoleNameByID(model.UserTypeof.ToString());
                 user.Category = await _dataContext.Categories.FindAsync(model.Categoryid);
                 user.Cateroriaid = model.Categoryid;
                 user.User_CreatedTime = DateTime.Today;*/

                

               
                /*   string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                   string tokenLink = Url.Action("ConfirmEmail", "Account", new
                   {
                       userid = user.Id,
                       token = myToken
                   }, protocol: HttpContext.Request.Scheme);

                   Response response = _mailHelper.SendMail(model.Email, "Vehicles - Confirmación de cuenta", $"<h1>Vehicles - Confirmación de cuenta</h1>" +
                       $"Para habilitar el usuario, " +
                       $"por favor hacer clic en el siguiente enlace: </br></br><a href = \"{tokenLink}\">Confirmar Email</a>");
                   */
                return RedirectToAction(nameof(Index));
            }
            model.UserType = User.IsInRole("Editor") ? _combos.GetCombosRolesunicos() : _combos.GetCombosRoles();
            model.Categories = _combos.GetCategorias();
            model.Countries = _combos.GetCombosCountries();
                 model.States = _combos.GetCombosStates();
                 model.Cities = _combos.GetCombosCities();
            ViewBag.Genero = _combos.GetComboGeneros();


            return View(model);
        }


        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> EditUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var user = await _userHelper.GetUserAsyncbyGuid(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }
            UserViewModel model = _swithUsers.ToUserViewModel(user);
            model.UserType = User.IsInRole("Editor") ? _combos.GetCombosRolesunicos() : _combos.GetCombosRoles();
            ViewBag.Genero = _combos.GetComboGeneros();
            //    model.UserType = roleItems;
            return View(model);


        }


        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userondb = await _userHelper.GetUserAsyncbyEmail(model.Email);
                if (model.FotoFile != null)//revisamos si se cargo foto nueva
                {

                    if (!string.IsNullOrEmpty(userondb.User_Image))//si se cargo foto nueva se procede a identificar la foto antigua para borrarla
                    {
                        if (_imageHelper.DeleteImage(userondb.User_Image, "Users"))//se borra la foto antigua
                        {
                            model.User_Image = _imageHelper.UploadImage(model.FotoFile, "Users");//se sube la foto nuevay se almacena a la entidad
                        }
                        else
                        {
                            return RedirectToAction("Index", "Users");
                        }
                    }
                    else
                    {
                        model.User_Image = _imageHelper.UploadImage(model.FotoFile, "Users");
                    }
                }

                var currentUser = await _swithUsers.ToUserAsync(model, false);
                                 

               // var userOnDB = await _userHelper.GetUserAsync(Guid.Parse(model.Id));
                currentUser.User_Image = userondb.User_Image;
             //   currentUser.Category = await _dataContext.Categories.FindAsync(model.Categoryid);
                string newRoleNAME = _userHelper.GetRoleNameByID(model.UserTypeof);
                //  currentUser.User_CreatedTime = currentUser.User_CreatedTime;
                if (userondb.UserTypeofRole != newRoleNAME)//verificamos si modifico el rol
                {
                    var result = await _userHelper.RemoveFromRoleLAST(currentUser.Id, userondb.UserTypeofRole, newRoleNAME); //_userHelper.UpdateRoleInUser(currentUser, currentUser.UserTypeofRole, newRoleNAME);
                    if (result)
                    {
                        // await _userHelper.AddUserToRoleAsync(currentUser, newRoleNAME); 
                        currentUser.UserTypeofRole = newRoleNAME;// _userHelper.GetRoleNameByID(model.UserTypeof.ToString());
                    }

                }

                await _userHelper.UpdateUserAsync(currentUser);//actualizamos el usuario totalmente

                TempData["successUser"] = "Usuario " + currentUser.User_FullName + " editado!!";

                return RedirectToAction(nameof(Index));
            }
            model.UserType = _combos.GetCombosRoles();
            model.Categories = _combos.GetCategorias();
            model.Countries = _combos.GetCombosCountries();
            model.States = _combos.GetCombosStates();
            model.Cities = _combos.GetCombosCities();
            ViewBag.Genero = _combos.GetComboGeneros();
            return View(model);

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var user = await _userHelper.GetUserAsyncbyGuid(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }
            /*   var userRoles = await _userManager.GetRolesAsync(user);

               var roleItems = _combos.GetComboRoles().Select(role =>
               new SelectListItem(
               role.Text,
               role.Value,
               userRoles.Any(ur => ur.Contains(role.Text)))).ToList();*/

            UserViewModel model = _swithUsers.ToUserViewModel(user);
            //    model.UserType = roleItems;
            return View(model);


        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserAsyncbyGuid(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                var result = await _userHelper.DeleteUserAsync(user);
                if (result.Succeeded)
                {
                    _imageHelper.DeleteImage(user.User_Image, "Users");
                    TempData["successUser"] = "Usuario " + user.User_FullName + " eliminado!!";
                }

            }
            catch (Exception e)
            {
                TempData["errorUser"] = "Error " + e.Message + "";
                throw;
            }

            return RedirectToAction(nameof(Index));
        }


        //perfil de usuarios

        
        public async Task<IActionResult> Profile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var user = await _userHelper.GetUserAsyncbyEmail(id);
            List<string> catnames = _dataContext.Categories.Where(e => user.SelectedCategoryIds.Contains(e.Category_ID)).Select(e => e.Category_Name).ToList();
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.CatNames = catnames;
            UserViewModel model = _swithUsers.ToUserViewModel(user);
            ViewBag.Timesta = model.User_CreatedTime;
            return View(model);
        }


        [Authorize(Roles = "User,Admin,Dt,Editor")]
        public async Task<IActionResult> EditProfile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var user = await _userHelper.GetUserAsyncbyGuid(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }

            UserViewModel model = _swithUsers.ToUserViewModel(user);

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "User,Admin,Dt,Editor")]
        public async Task<IActionResult> EditProfile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userOnDB = await _userHelper.GetUserAsyncbyGuid(Guid.Parse(model.Id));

                if (model.FotoFile != null)//revisamos si se cargo foto nueva
                {
                    if (!string.IsNullOrEmpty(userOnDB.User_Image))//si se cargo foto nueva se procede a identificar la foto antigua para borrarla
                    {
                        if (_imageHelper.DeleteImage(userOnDB.User_Image, "Users"))//se borra la foto antigua
                        {
                            model.User_Image = _imageHelper.UploadImage(model.FotoFile, "Users");//se sube la foto nuevay se almacena a la entidad
                        }
                        else
                        {
                            return RedirectToAction("Profile", "Users");
                        }
                    }
                    else
                    {
                        model.User_Image = _imageHelper.UploadImage(model.FotoFile, "Users");
                    }
                }
               
                string newRoleNAME = _userHelper.GetRoleNameByID(model.UserTypeof);
                var currentUser = await _swithUsers.ToUserAsync(model, false);
                currentUser.SelectedCategoryIds = userOnDB.SelectedCategoryIds;

                if (userOnDB.UserTypeofRole != newRoleNAME)
                {
                    var result = await _userHelper.RemoveFromRoleLAST(currentUser.Id, userOnDB.UserTypeofRole, newRoleNAME); 
                    if (result)
                    {
                        currentUser.UserTypeofRole = newRoleNAME;
                    }

                }

                await _userHelper.UpdateUserAsync(currentUser);

                TempData["successUser"] = "Usuario " + currentUser.User_FullName + " editado!!";

                return RedirectToAction("Profile", "Users", new { id = currentUser.Email });
            }
            return View(model);

        }

        //cambair password
        public IActionResult ChangePassword(string id)
        {
            var userOnDB =  _userHelper.GetUserAsyncbyEmail(id);
            
            ChangePasswordViewModel model = new()
            {
                UserEmail = userOnDB.Result.Email,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userHelper.GetUserAsyncbyEmail(User.Identity.Name);
                IdentityResult result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                  //  return RedirectToAction("ChangeUser");
                    return RedirectToAction("Profile", "Users", new { id = user.Email });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                }
            }

            return View(model);
        }


        public JsonResult GetDepartments(int countryId) {
            Country country = _dataContext.Countries
                .Include(cp => cp.States)
                .FirstOrDefault(cp => cp.Country_ID == countryId);
            if (country == null) {
                return null;
            }
            return Json(country.States.OrderBy(cp=>cp.State_Name));
        }

        public JsonResult GetCities(int departmentId) {
            State state = _dataContext.States
                  .Include(cp => cp.Cities)
                  .FirstOrDefault(cp => cp.State_ID == departmentId);
            if (state == null)
            {
                return null;
            }
            return Json(state.Cities.OrderBy(cp => cp.City_Name));
        }

        public JsonResult CountryDrop()
        {
            var cnt = _dataContext.Countries.ToList();
            return new JsonResult(cnt);
        }

        public JsonResult StateDrop(int id)
        {
            var st = _dataContext.States.Where(e => e.CountryId == id).ToList();
            return new JsonResult(st);
        }

        public JsonResult CityDrop(int id)
        {
            var ct = _dataContext.Cities.Where(e => e.IdState == id).ToList();
            return new JsonResult(ct);
        }




    }
}
