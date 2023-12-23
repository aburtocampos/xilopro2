using xilopro2.Data.Entities;
using xilopro2.Models;

namespace xilopro2.Helpers.Interfaces
{
    public interface ISwithUsers
    {
        Task<AppUser> ToUserAsync(UserViewModel model, bool isNew);

        UserViewModel ToUserViewModel(AppUser user);

        //  Task<AppUser> ToPlayerAsync(PlayerViewModel model, bool isNew);

        AppUser FromPlayerToAppUser(Player player, bool isNew);

        Task<Player> ModelToPlayer(PlayerViewModel model, bool isNew);

        PlayerViewModel ToPlayerViewModel(Player player);
    }
}
