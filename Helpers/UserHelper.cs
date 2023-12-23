using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Helpers.Interfaces;
using xilopro2.Models;

namespace xilopro2.Helpers
{
    public class UserHelper : IUserHelper
    {
        private  DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserHelper(DataContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

   

        public async Task AddUserAsync2(AppUser user, string password)
        {


            /* IdentityResult result = */

            try
            {
                await _userManager.CreateAsync(user, password);
            }
            catch (Exception)
            {

                throw;
            }
            

          /*  if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    //ModelState.TryAddModelError(error.Code, error.Description);
                    Console.WriteLine(error.Code, error.Description);
                }
                // return result;
            }
            return result;*/
        }

        public async Task<IdentityResult> AddUserAsync(AppUser user, string password)
        {


            IdentityResult result = await _userManager.CreateAsync(user, password);


             if (!result.Succeeded)
              {
                  foreach (var error in result.Errors)
                  {
                      //ModelState.TryAddModelError(error.Code, error.Description);
                      Console.WriteLine(error.Code, error.Description);
                  }
                  // return result;
              }
              return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(AppUser user)
        {
            AppUser currentUser = await _userManager.FindByEmailAsync(user.Email);

             currentUser.User_FirstName = user.User_FirstName;
             currentUser.User_LastName = user.User_LastName;
             currentUser.User_Address = user.User_Address;
             currentUser.PhoneNumber = user.PhoneNumber;
             currentUser.UserTypeofRole = user.UserTypeofRole;
             currentUser.User_Cedula = user.User_Cedula;
             currentUser.UserName = user.UserName;
             currentUser.User_FNC = user.User_FNC;
             currentUser.Email = user.Email;
             currentUser.Id = user.Id;
             currentUser.User_Status = user.User_Status;
             currentUser.User_Image = user.User_Image;
            currentUser.Countryid = user.Countryid;
            currentUser.Stateid = user.Stateid;
            currentUser.Cityid = user.Cityid;
            currentUser.SelectedCategoryIds = user.SelectedCategoryIds;

            IdentityResult result = await _userManager.UpdateAsync(currentUser);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    //ModelState.TryAddModelError(error.Code, error.Description);
                    Console.WriteLine(error.Code, error.Description);
                }
            }
            return result;
        }

        public async Task<bool> UpdateRoleInUser(AppUser user, string oldRol, string newRol)
        {
            if (newRol != oldRol)
            {
                var isInRole = await _userManager.IsInRoleAsync(user, oldRol);
                var oldRoles = await _userManager.GetRolesAsync(user);
                // await _userManager.RemoveFromRoleAsync(user, oldRol);

                if (isInRole)
                {
                    //removemos el role anterior en el usuario

                    for (int i = 0; i < oldRoles.Count; i++)
                    {
                        await _userManager.RemoveFromRolesAsync(user, oldRoles);
                    }
                    //actualizamos el rol al usuario
                    await AddUserToRoleAsync(user, newRol);
                }


            }
            return true;
        }

        public String GetRoleNameByID(string roleId)
        {
            string roleNamed = string.Empty;
            var roles = _roleManager.Roles.ToList();
            var roleDb = roles.FirstOrDefault(r => r.Id == roleId);
            if (roleDb != null)
            {
                roleNamed = roleDb.Name;
            }

            return roleNamed;
        }

        public String GetRoleIdByNAME(string roleName)
        {
            string idRole = string.Empty;
            var roles = _roleManager.Roles.ToList();
            var roleDb = roles.FirstOrDefault(r => r.Name == roleName);
            if (roleDb != null)
            {
                idRole = roleDb.Id;
            }

            return idRole;
        }

        public async Task AddUserToRoleAsync(AppUser user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName,
                });
            }
        }

        public async Task<AppUser> GetUserAsync(string email)
        {
            AppUser response = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return response;
        }
        public async Task<AppUser> GetUserAsync(Guid id)
        {
            var response = await _context.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());
            return response;
        }

        public Task<AppUser> GetUserManager(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> IsUserInRoleAsync(AppUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<IdentityResult> DeleteUserAsync(AppUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<bool> RemoveFromRoleLAST(string userID, string roleName, string newRole)
        {
            AppUser userInDb = await _context.Users.FirstOrDefaultAsync(user => user.Id == userID);
            if (userInDb == null)
                return false;

            //get user's assigned roles
            IList<string> userRoles = await _userManager.GetRolesAsync(userInDb);

            //check for role to be removed
            var roleToRemove = userRoles.FirstOrDefault(role => role.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));
            if (roleToRemove == null)
                return false;

            var result = await _userManager.RemoveFromRoleAsync(userInDb, roleToRemove);
            if (result.Succeeded)
            {
                //agregar nuevo rol
                await _userManager.AddToRoleAsync(userInDb, newRole);
                return true;
            }


            return false;
        }

       
    }
}
