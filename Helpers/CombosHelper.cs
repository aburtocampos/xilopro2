using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using xilopro2.Data;
using xilopro2.Enums;
using xilopro2.Helpers.Interfaces;

namespace xilopro2.Helpers
{
    public class CombosHelper:ICombos
    {
        private readonly DataContext _dataContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CombosHelper(DataContext context, RoleManager<IdentityRole> roleManager)
        {
            _dataContext = context;
            _roleManager = roleManager;
        }

        public List<SelectListItem> GetComboGeneros()
        {
            var list = new List<SelectListItem> { 
               new SelectListItem{
                    Text = "[Seleccionar...]",
                    Value = "null", // Valor vacío o puedes establecerlo a null
               // Selected = true // Establecer como seleccionado por defecto
                },
                new SelectListItem
                {
                    Text = "Masculino",
                    Value = "Masculino",
                },
                new SelectListItem
                {
                    Text = "Femenino",
                    Value = "Femenino",
                }
            };
            /*     new SelectListItem
                   {
                       Text = "[Seleccionar...]",
                       Value = null, // Valor vacío o puedes establecerlo a null
                       Selected = true // Establecer como seleccionado por defecto
                   }
           };

           list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un género...]",
                Value = ""
            });*/
            return list;
          //  return list.OrderBy();
        }

        public IEnumerable<SelectListItem> GetCategorias()
        {
            List<SelectListItem> list = _dataContext.Categories.Select(x => new SelectListItem
            {
                Text = x.Category_Name,
                Value = $"{x.Category_ID}"
            })
                .OrderBy(x => x.Text)
                .ToList();
           // list.Insert(0, new SelectListItem{ Text = "[Seleccione una categoria...]", Value = "0" });
            return list;
        }

        public IEnumerable<SelectListItem> GetCategoriasPorIds(List<int> ids)
        {
            List<SelectListItem> list = _dataContext.Categories
                 .Where(c => ids.Contains(c.Category_ID))
                .Select(x => new SelectListItem
            {
                Text = x.Category_Name,
                Value = $"{x.Category_ID}"
            })
                .OrderBy(x => x.Text)
                .ToList();
            // list.Insert(0, new SelectListItem{ Text = "[Seleccione una categoria...]", Value = "0" });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboCategorias()
        {
            List<SelectListItem> list = _dataContext.Categories.Select(x => new SelectListItem
            {
                Text = x.Category_Name,
                Value = $"{x.Category_ID}"
            })
                .OrderBy(x => x.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una categoria...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetCombosEquipos()
        {
            List<SelectListItem> list = _dataContext.Teams.Select(x => new SelectListItem
            {
                Text = x.Team_Name,
                Value = $"{x.Team_ID}"
            })
                .OrderBy(x => x.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un equipo...]",
                Value = "0"
            });
            return list;
        }
        public IEnumerable<SelectListItem> GetCombosPosiciones()
        {
            List<SelectListItem> list = _dataContext.Positions.Select(x => new SelectListItem
            {
                Text = x.Position_Name,
                Value = $"{x.Position_ID}"
            })
                .OrderBy(x => x.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una posición...]",
                Value = "0"
            });
            return list;
        }


        public IEnumerable<SelectListItem> GetCombosRolesunicos()
        {
            // Obtener solo dos roles específicos
            var rolesFiltrados = _roleManager.Roles
                .Where(x => x.Name == "Editor" || x.Name == "Dt")
                .OrderBy(x => x.Name)
                .ToList();

            // Crear la lista de SelectListItem basada en los roles filtrados
            List<SelectListItem> list = rolesFiltrados
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = $"{x.Id}"
                })
                .ToList();

            // Agregar la opción por defecto
            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un rol...]",
                Value = "0"
            });

            return list;
        }

        //desde identity
        public IEnumerable<SelectListItem> GetCombosRoles()
        {
            // var roles = _roleManager.Roles;
            List<SelectListItem> list = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            })
               .OrderBy(x => x.Text)
               .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un rol...]",
                Value = "0"
            });
            return list;
        }

        //desde enum
        public IEnumerable<SelectListItem> GetComboRoles()
        {

            List<SelectListItem> list = Enum.GetValues(typeof(UserType)).Cast<UserType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            })
                 .OrderBy(x => x.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un rol...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetCombosCountries()
        {
            List<SelectListItem> list = _dataContext.Countries.Select(x => new SelectListItem
            {
                Text = x.Country_Name,
                Value = $"{x.Country_ID}"
            })
                 .OrderBy(x => x.Text)
                 .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un pais...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetCombosStates()
        {
            List<SelectListItem> list = _dataContext.States
                .Where(x => x.Country.Country_Name == "Nicaragua")
                .Select(x => new SelectListItem
                {
                    Text = x.State_Name,
                    Value = $"{x.State_ID}"
                })

                .OrderBy(x => x.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un departamento...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetCombosCities()
        {
            List<SelectListItem> list = _dataContext.Cities.Select(x => new SelectListItem
            {
                Text = x.City_Name,
                Value = $"{x.City_ID}"
            })
                .OrderBy(x => x.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un municipio...]",
                Value = "0"
            });
            return list;
        }


    }
}
