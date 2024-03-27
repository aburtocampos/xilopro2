using xilopro2.Data.Entities;
using xilopro2.Data;
using xilopro2.Helpers.Interfaces;
using xilopro2.Models;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Mailjet.Client.Resources;
using AppUser = xilopro2.Data.Entities.AppUser;
using NuGet.Packaging.Signing;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace xilopro2.Helpers
{
    public class SwitchUserHelper : ISwithUsers
    {
        private readonly DataContext _context;
        private readonly ICombos _combos;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;

        public SwitchUserHelper(DataContext dataContext, ICombos combitos, IUserHelper userHelper,IImageHelper imageHelper)
        {
            _context = dataContext;
            _combos = combitos;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
        }

        public async Task<AppUser> ToUserAsync(UserViewModel model, bool isNew)
        {
            return new AppUser
            {
                Email = model.Email,
                User_FirstName = model.User_FirstName?.Trim().ToUpper(),
                User_LastName = model.User_LastName?.Trim().ToUpper(),
                User_Address = model.User_Address,
                PhoneNumber = model.PhoneNumber,
                UserTypeofRole = _userHelper.GetRoleNameByID(model.UserTypeof.ToString()),
                User_Cedula = model.User_Cedula,
                UserName = model.Email,
                Id = isNew ? Guid.NewGuid().ToString() : model.Id,
                User_Status = model.Status,
                User_FNC = model.User_FNC,
               // User_Image = isNew ? _imageHelper.UploadImage(model.FotoFile, "Users") : null,

                Countryid = model.Countryid,
                Stateid = model.Stateid,
                Cityid = model.Cityid,
                SelectedCategoryIds = model.SelectedCategoryIds,
              /*  Cateroriaid = model.Categoryid,
                Category = await _context.Categories.FindAsync(model.Categoryid),*/

                User_CreatedTime = DateTime.Today,


            };
        }


     /*   public async Task<AppUser> ToPlayerAsync(PlayerViewModel model, bool isNew)
        {
           // List<int> selectedCategories = new List<int> { model.Categoryid };

            return new AppUser
            {
               // Id = isNew ? Guid.NewGuid().ToString() : model.Player_ID,
                Email = model.Player_Email,
                User_Status = model.Player_Status,

                User_FirstName = model.Player_FirstName,
                User_LastName = model.Player_LastName,
                UserTypeofRole = model.Player_UserRol,
                UserName = model.Player_Email,
                PhoneNumber = model.PhoneNumber,
                User_Address = model.Player_Address,
                Player_Dorsal = model.Player_Dorsal,
                User_CreatedTime = DateTime.Now,
                User_Cedula = model.Player_Cedula ?? "0000000000000L",
                User_FNC = model.Player_FNC,
                Player_fifaid = model.Player_fifaid ?? "00000",
                User_Genero = model.Player_Genero,
                User_Image = null,
                Player_Image = _imageHelper.UploadImage(model.FotoFile, "Players"),

                SelectedCategoryIds = model.SelectedCategoryIds,

                Countryid = model.CountryID,
                Stateid = model.StateID,
                Cityid = model.CityID,

                Position_ID = model.Positionid,
                //Position =  _context.Positions.FirstOrDefault(cp=>cp.Position_ID == model.Positionid),
                Team_ID = model.Teamid,
               // Team = await _context.Teams.FindAsync(model.Teamid),
                //  Category = _context.Categories.FirstOrDefault(x => x.Category_ID == 1),
                // Category = _context.Categories.FindAsync(model.Categoryid),
            };
        }*/

        public async Task<Player> ModelToPlayer(PlayerViewModel model, bool isNew)
        {
            return new Player
            {
                Player_ID = isNew ? 0 : model.Player_ID,
                Player_FirstName = model.Player_FirstName?.Trim().ToUpper(),
                Player_Address = model.Player_Address,
                Player_LastName = model.Player_LastName?.Trim().ToUpper(),
                Player_Status = model.Player_Status,
                Player_Dorsal = model.Player_Dorsal,
                Player_FNC = model.Player_FNC,
                Player_PhoneNumber = model.Player_PhoneNumber,
                Player_Email = model.Player_Email,
                Player_Cedula = model.Player_Cedula ?? "0000000000000L",
                Player_fifaid = model.Player_fifaid ?? "00000",
                Player_Genero = model.Player_Genero,
                Player_Image = model.Player_Image,
           //     Player_Image = isNew ? _imageHelper.UploadImage(model.FotoFile, "Players") : null,
                //Player_UserRol = model.Player_UserRol,
                SelectedCategoryIds = model.SelectedCategoryIdss,

                Countryid = model.Countryid,
                Stateid = model.Stateid,
                Cityid = model.Cityid,

                Positionid = model.Positionid,
                Teamid = model.Teamid,

                

              //  Country = await _context.Countries.FindAsync(model.Countryid),
             //   Position = await _context.Positions.FindAsync(model.Positionid),
             //   Team = await _context.Teams.FindAsync(model.Teamid),
                //TeamId = model.Teamid,
               // PositionId = model.Positionid,

            };
        }



       

        public PlayerViewModel ToPlayerViewModel(Player player)
        {
             PlayerViewModel resp = new PlayerViewModel
            {
                Player_Dorsal = player.Player_Dorsal,
                Player_Cedula = player.Player_Cedula,
                Player_FirstName = player.Player_FirstName,
                Player_LastName = player.Player_LastName,
                Player_Address = player.Player_Address,
                 Player_PhoneNumber = player.Player_PhoneNumber,
                Player_FNC = player.Player_FNC,
                Player_Email = player.Player_Email,
                
              //  UserTypeof = _userHelper.GetRoleIdByNAME(user.UserTypeofRole),
              //  UserType = _combos.GetCombosRoles(),
                Player_Image = player.Player_Image,
                Player_ID = player.Player_ID,
                Player_Status = player.Player_Status,
                Player_fifaid = player.Player_fifaid,
                Player_Genero = player.Player_Genero,
                SelectedCategoryIdss = player.SelectedCategoryIds,
                Categories = _combos.GetComboCategorias(),
                Countries = _combos.GetCombosCountries(),
                States = _combos.GetCombosStates(),
                Cities = _combos.GetCombosCities(),
                Teams = _combos.GetCombosEquipos(),
                Positions = _combos.GetCombosPosiciones(),
                /*    Categoryid = player.Category.Category_ID,
                Positionid = player.Position.Position_ID,
               // Categoryid = player.Category.Category_ID,
                Teamid = player.Team.Team_ID,
                CountryID = player.Country.Country_ID,
                StateID = player.Stateid,
                CityID = player.Cityid,*/
               /* StateID = player.State.State_ID,
                CityID = player.City.City_ID,*/

                 Positionid = (from player0 in _context.Players
                           join position in _context.Positions on player0.Position.Position_ID equals position.Position_ID
                           select position.Position_ID).FirstOrDefault(),

                 Teamid = (from player2 in _context.Players
                           join teams in _context.Teams on player2.Team.Team_ID equals teams.Team_ID
                           select teams.Team_ID).FirstOrDefault(),

                 Countryid = (from player3 in _context.Players
                              join country in _context.Countries on player3.Countryid equals country.Country_ID
                              //    where usuario.Id == player.Player_ID
                              select country.Country_ID).FirstOrDefault(),

                 Stateid = (from player4 in _context.Players
                            join state in _context.States on player4.Stateid equals state.State_ID
                            //  where usuario.Id == player.Player_ID
                            select state.State_ID).FirstOrDefault(),

                 Cityid = (from player5 in _context.Players
                           join city in _context.Cities on player5.Cityid equals city.City_ID
                           //  where usuario.Id == player.Player_ID
                           select city.City_ID).FirstOrDefault(),

                 /*  Categoryid = (from obj0 in _context.Players
                                 join categoria in _context.Categories on obj0.Category.Category_ID equals categoria.Category_ID
                                 select categoria.Category_ID).FirstOrDefault(),

          */

             };
            return resp;


        }

        public  UserViewModel ToUserViewModel(AppUser user)
        {
            UserViewModel response = new UserViewModel
            {
                Email = user.Email,
                User_Cedula = user.User_Cedula,
                User_FirstName = user.User_FirstName,
                User_LastName = user.User_LastName,
                User_Address = user.User_Address,
                PhoneNumber = user.PhoneNumber,
                UserTypeof = _userHelper.GetRoleIdByNAME(user.UserTypeofRole),
               // UserType = _combos.GetCombosRoles(),
               User_CreatedTime = user.User_CreatedTime,
                User_Image = user.User_Image,
                User_FNC = user.User_FNC,
                Id = user.Id,
                User_Genero = user.User_Genero,
                
                Status = user.User_Status,
                SelectedCategoryIds = user.SelectedCategoryIds,
                Categories = _combos.GetCategorias(),/* PorIds(user.SelectedCategoryIds),*/

                Countries = _combos.GetCombosCountries(),
                States = _combos.GetCombosStates(),
                Cities = _combos.GetCombosCities(),
            /*    Categoryid = (from categoria in _context.Categories
                              join usuario in _context.Users on categoria.Category_ID equals usuario.Category.Category_ID
                              where usuario.Id == user.Id
                              select categoria.Category_ID).FirstOrDefault(),*/
                Countryid = (from Country in _context.Countries
                             join usuario in _context.Users on Country.Country_ID equals usuario.Countryid
                             where usuario.Id == user.Id
                             select Country.Country_ID).FirstOrDefault(),

                Stateid = (from State in _context.States
                           join usuario in _context.Users on State.State_ID equals usuario.Stateid
                           where usuario.Id == user.Id
                           select State.State_ID).FirstOrDefault(),

                Cityid = (from City in _context.Cities
                          join usuario in _context.Users on City.City_ID equals usuario.Cityid
                          where usuario.Id == user.Id
                          select City.City_ID).FirstOrDefault(),

            };
            return response;
        }

        public AppUser FromPlayerToAppUser(Player player, bool isNew)
        {
            return new AppUser
            {
               // Id = isNew ? Guid.NewGuid().ToString() : player.Player_ID,
                User_FirstName = player.Player_FirstName,
                User_LastName = player.Player_LastName,
                User_Address = player.Player_Address,
                User_Cedula = player.Player_Cedula,
                User_Genero = player.Player_Genero,
                User_Status = player.Player_Status,
                User_CreatedTime = DateTime.Today,
                User_FNC = player.Player_FNC,
                PhoneNumber = player.Player_PhoneNumber,
               // User_Image =  _imageHelper.UploadImage(player.FotoFile, "Players"),
                UserTypeofRole = "User",
                SelectedCategoryIds = player.SelectedCategoryIds,
                Countryid = player.Countryid,
                Stateid = player.Stateid,
                Cityid = player.Cityid,

                UserName = player.Player_Email,
                Email = player.Player_Email

            };
        }
    }
}
