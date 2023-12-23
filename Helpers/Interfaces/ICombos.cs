using Microsoft.AspNetCore.Mvc.Rendering;

namespace xilopro2.Helpers.Interfaces
{
    public interface ICombos
    {
        IEnumerable<SelectListItem> GetComboCategorias();

        IEnumerable<SelectListItem> GetCategorias();
        IEnumerable<SelectListItem> GetCategoriasPorIds(List<int> ids);

        IEnumerable<SelectListItem> GetCombosPosiciones();

        IEnumerable<SelectListItem> GetCombosEquipos();

        IEnumerable<SelectListItem> GetCombosCountries();
        IEnumerable<SelectListItem> GetCombosStates();
        IEnumerable<SelectListItem> GetCombosCities();

        IEnumerable<SelectListItem> GetComboRoles();

        IEnumerable<SelectListItem> GetCombosRoles();

        IEnumerable<SelectListItem> GetCombosRolesunicos();

        List<SelectListItem> GetComboGeneros();
    }
}
