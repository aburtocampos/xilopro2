using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Enums;
using xilopro2.Helpers.Interfaces;
using xilopro2.Models;

namespace xilopro2.Helpers
{
    public class CombosHelper: ICombos
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
              /* new SelectListItem{
                    Text = "[Seleccionar...]",
                    Value = "null", // Valor vacío o puedes establecerlo a null
               // Selected = true // Establecer como seleccionado por defecto
                },*/
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
            list.Insert(0, new SelectListItem
            {
                Text = "Seleccionar Género...",
                Value = ""
            });
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

        public IEnumerable<SelectListItem> GetJornadaas()
        {
            List<SelectListItem> list = _dataContext.Matches.Select(x => new SelectListItem
            {
                Text = x.Jornada,
                Value = $"{x.Match_ID}"
            })
                .OrderBy(x => x.Text)
                .ToList();
            return list;
        }

      /*  public IEnumerable<IGrouping<string, SelectListItem>> GetJornadas(int? torneoid)
        {
            var list = _dataContext.Matches
             .Include(m => m.Groups)
             .Where(t => t.torneoid == torneoid)
             .Select(x => new SelectListItem
             {
                 Text = x.Jornada,
                 Value = $"{x.Match_ID}",
                 Group = new SelectListGroup { Name = x.Groups.Group_Name } // Suponiendo que hay una propiedad Name en Groups
             })
             .OrderBy(x => x.Group.Name) // Ordenar por grupo
             .ThenBy(x => x.Text) // Luego ordenar por el texto del item
             .ToList();

            return list.GroupBy(x => x.Group.Name);
        }*/

        public List<GroupedMatchViewModel> GetJornadas(int? torneoid)
        {
            var list = _dataContext.Matches
                .Include(m => m.Groups)
                .Where(t => t.torneoid == torneoid)
                .Select(x => new SelectListItem
                {
                    Text = x.Jornada,
                    Value = $"{x.Match_ID}",
                    Group = new SelectListGroup { Name =  x.Groups.Group_Name } // Suponiendo que hay una propiedad Name en Groups
                })
                .OrderBy(x => x.Group.Name) // Ordenar por grupo
                .ThenBy(x => x.Text) // Luego ordenar por el texto del item
                .ToList();

               var groupedMatches = list.GroupBy(x => x.Group.Name)
                .Select(group => new GroupedMatchViewModel
                {
                    GroupName = group.Key,
                    Matches = group.ToList(),
                    
                })
                .OrderBy(x => x.GroupName)
                .ToList();

            return groupedMatches;
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


        public string GetCategoriaPorId(List<int> id)
        {
            var categoriaName = _dataContext.Categories
            .Where(c => id.Contains(c.Category_ID))
            .Select(c => c.Category_Name)
            .SingleOrDefault();

            return categoriaName;
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
                Text = "Seleccione una categoria...",
                Value = "0"
            });
            return list;
        }

        public List<Team> GetCombosEquipos()
        {
            List<Team> list = new List<Team>();
            try
            {
                list = _dataContext.Teams
                    .Select(t => new Team
                    {
                        Team_ID = t.Team_ID,
                        Team_Name = t.Team_Name,
                        Team_Image = t.Team_Image,
                    })
                    .OrderBy(t => t.Team_Name)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }


        public List<Team> GetCombosEquiposPorIds(int id)
        {
            List<Team> list = new List<Team>();
            try
            {
                list = _dataContext.GroupDetails
                 .Include(gd => gd.Team)
                 .Where(gd => gd.Groups.Group_ID == id)
                 .Select(gd => new Team
                 {
                     Team_ID = gd.Team.Team_ID,
                     Team_Name = gd.Team.Team_Name , 
                     Team_Image = gd.Team.Team_Image,
                 })
                 .OrderBy(t => t.Team_Name)
                 .ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }


       /* public List<Player> GetCombosPlayersPorIds(int id)
        {
            List<Player> list = new List<Player>();
            try
            {
                list = _dataContext.PlayerStatistics
                 .Include(gd => gd.Player)
                 .Where(gd => gd.Matchgames.Match_ID == id)
                 .Select(gd => new Player
                 {
                     Player_ID = gd.Player.Player_ID,
                     Player_FirstName = gd.Player.Player_FirstName,
                     Player_Image = gd.Player.Player_Image,
                 })
                 .OrderBy(t => t.Player_Dorsal)
                 .ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }*/

        public List<Player> GetCombosPlayers()
        {
            List<Player> list = new List<Player>();
            try
            {
                list = _dataContext.Players
                 .Where(gd => gd.Player_Status == true)
                 .OrderBy(t => t.Player_LastName)
                 .ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public List<Player> GetCombosPlayersbyCat(List<int> ids)
        {
            List<Player> list = new List<Player>();
            try
            {
                /*  list = _dataContext.Players
                   .Where(gd => gd.Player_Status == true && gd.SelectedCategoryIds == ids)
                   .OrderBy(t => t.Player_LastName)
                   .ToList();*/
                var sortedIds = ids.OrderBy(id => id).ToList();

                list = _dataContext.Players
                    .Where(player =>
                        player.SelectedCategoryIds != null &&
                        player.SelectedCategoryIds.OrderBy(id => id).SequenceEqual(sortedIds)
                    )
                    .OrderBy(player => player.Player_LastName)
                        .ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        /*  public IEnumerable<SelectListItem> GetCombosEquiposPorIds(int id)
          {
              List<SelectListItem> list = new List<SelectListItem>();
              try
              {
                  list = _dataContext.GroupDetails
                   .Include(gd => gd.Team)
                   .Where(gd => gd.Groups.Group_ID == id)
                   .Select(gd => new SelectListItem
                   {
                       Text = gd.Team.Team_Name, // Usar directamente sin string.Format
                       Value = $"{gd.Team.Team_ID}|{gd.Team.Team_Image}",
                       //  Value = gd.Team.Team_ID.ToString() + "|" + gd.Team.Team_Image, 


                   })
                   .OrderBy(t => t.Text) // Esto ya ordena por el nombre del equipo directamente
                   .ToList();
              }
              catch (Exception)
              {
                  throw;
              }

              list.Insert(0, new SelectListItem
              {
                  Text = "Seleccionar equipo...",
                  Value = "0"
              });

              return list;
          }*/



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
                Text = "Seleccione una posición...",
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
                Text = "Seleccione un rol...",
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
                Text = "Seleccione un rol...",
                Value = "0"
            });
            return list;
        }

        //desde enum
        public IEnumerable<SelectListItem> GetCombosRolesEnum()
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

        public IEnumerable<SelectListItem> GetComboGenerosEnum()
        {

            List<SelectListItem> list = Enum.GetValues(typeof(Genders)).Cast<Genders>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            })
                 .OrderBy(x => x.Value)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccionar genero...]",
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
                Text = "Seleccione un pais...",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetCombosStates()
        {
            List<SelectListItem> list = _dataContext.States
               // .Where(x => x.Country.Country_Name == "Nicaragua")
                .Select(x => new SelectListItem
                {
                    Text = x.State_Name,
                    Value = $"{x.State_ID}"
                })

                .OrderBy(x => x.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione un departamento...",
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
                Text = "Seleccione un municipio...",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetCombosCitiesByState(int stateid=1)
        {
            List<SelectListItem> list = _dataContext.Cities
              .Where(c => c.IdState == stateid)
             .Select(x => new SelectListItem
            {
                Text = x.City_Name,
                Value = $"{x.City_ID}"
            })
                .OrderBy(x => x.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "Seleccione un municipio...",
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetCombosStatesByCountry(int countryid = 1)
        {
            List<SelectListItem> list = _dataContext.States
              .Where(c => c.Country.Country_ID == countryid)
             .Select(x => new SelectListItem
             {
                 Text = x.State_Name,
                 Value = $"{x.State_ID}"
             })
                .OrderBy(x => x.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "Seleccione un departamento...",
            });
            return list;
        }

       
    }
}
