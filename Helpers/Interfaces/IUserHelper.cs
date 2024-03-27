using Microsoft.AspNetCore.Identity;
using xilopro2.Data.Entities;
using xilopro2.Models;

namespace xilopro2.Helpers.Interfaces
{
    public interface IUserHelper
    {

        Task<AppUser> GetUserAsyncbyEmail(string email);
        Task<AppUser> GetUserAsyncbyGuid(Guid id);

        Task<AppUser> GetUserManager(string userId);
        Task<bool> RemoveFromRoleLAST(string userID, string roleName, string newRole);

        Task<IdentityResult> AddUserAsync(AppUser user, string password);

        Task AddUserAsync2(AppUser user, string password);

        Task<IdentityResult> UpdateUserAsync(AppUser user);

        Task<IdentityResult> DeleteUserAsync(AppUser user);
        Task CheckRoleAsync(string roleName);

        Task<bool> UpdateRoleInUser(AppUser user, string oldRol, string newRol);

        Task AddUserToRoleAsync(AppUser user, string roleName);

        Task<bool> IsUserInRoleAsync(AppUser user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        string GetRoleNameByID(string id);

        string GetRoleIdByNAME(string roleName);

        Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);

        Task LogoutAsync();


        Task<IdentityResult> ChangePasswordAsync(AppUser user, string oldpass, string newpass);

    }
}
