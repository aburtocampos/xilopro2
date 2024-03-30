using Microsoft.AspNetCore.Mvc.Rendering;
using xilopro2.Data.Entities;

namespace xilopro2.Helpers.Interfaces
{
    public interface ICombos
    {
        IEnumerable<SelectListItem> GetComboCategorias();

        IEnumerable<SelectListItem> GetCategorias();
        IEnumerable<SelectListItem> GetCategoriasPorIds(List<int> ids);
        List<Player> GetCombosPlayersbyCat(List<int> ids);

        string GetCategoriaPorId(List<int> id);

        IEnumerable<SelectListItem> GetCombosPosiciones();

        IEnumerable<SelectListItem> GetCombosEquipos();

        List<Team> GetCombosEquiposPorIds(int id);

        List<Player> GetCombosPlayers();

        List<Player> GetCombosPlayersPorIds(int id);

        IEnumerable<SelectListItem> GetCombosCountries();

        IEnumerable<SelectListItem> GetCombosStatesByCountry(int countryid = 1);
        IEnumerable<SelectListItem> GetCombosStates();

        IEnumerable<SelectListItem> GetCombosCitiesByState(int stateid = 1);
        IEnumerable<SelectListItem> GetCombosCities();

        IEnumerable<SelectListItem> GetCombosRoles();

        IEnumerable<SelectListItem> GetCombosRolesEnum();

        IEnumerable<SelectListItem> GetCombosRolesunicos();

        List<SelectListItem> GetComboGeneros();

        IEnumerable<SelectListItem> GetComboGenerosEnum();


    }
}
