using Microsoft.EntityFrameworkCore;
using xilopro2.Data.Entities;
using xilopro2.Helpers.Interfaces;

namespace xilopro2.Data
{
    public class SeederDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeederDB(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task FeederAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await checkCountriesAsync();
            await checkCategoriesAsync();
            await checkPositionsAsync();
            await checkTeamsAsync();
            await checkCorrectionActionAsync();
            await checkRolesAsync();
            //creacion de usuarios con roles
            await checkUserAsync("super", "man", "eabucam@gmail.com", "eabucam@gmail.com", "88503574", "calle luna", "Admin");
            //  await checkUserAsync( "jugador1", "primer jugador", "raburto1510@gmail.com", "88503574", "calle luna", UserType.User);
            //  await checkUserAsync( "Eduardo",  "Correa",         "pekiabu@gmail.com",     "88503574", "calle luna", UserType.Editor);
            // await checkUserAsync( "Tecnico",  "Tecnico",        "pruetesjuan@gmail.com", "88503574", "calle luna", UserType.Dt);
        }

        private async Task checkPositionsAsync()
        {
            if (!_context.Positions.Any())
            {
                _context.Positions.Add(new Position
                {
                    Position_Name = "DELANTERO",
                    Position_Status = true,

                });
                _context.Positions.Add(new Position
                {
                    Position_Name = "MEDIOCAMPO",
                    Position_Status = true,

                });
                _context.Positions.Add(new Position
                {
                    Position_Name = "DEFENSA",
                    Position_Status = true,

                });
                _context.Positions.Add(new Position
                {
                    Position_Name = "PORTERO",
                    Position_Status = true,

                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task checkCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category
                {
                    Category_Name = "MAYOR",
                    Category_Status = true,

                });
                _context.Categories.Add(new Category
                {
                    Category_Name = "U13 MASCULINO",
                    Category_Status = true,

                });
                _context.Categories.Add(new Category
                {
                    Category_Name = "U15 MASCULINO",
                    Category_Status = true,

                });
                _context.Categories.Add(new Category
                {
                    Category_Name = "U17 MASCULINO",
                    Category_Status = true,

                });
                _context.Categories.Add(new Category
                {
                    Category_Name = "U15 FEMENINO",
                    Category_Status = true,

                });
                _context.Categories.Add(new Category
                {
                    Category_Name = "U17 FEMENINO",
                    Category_Status = true,

                });
                await _context.SaveChangesAsync();
            }

        }

        private async Task checkTeamsAsync()
        {
            if (!_context.Teams.Any())
            {
                _context.Teams.Add(new Team
                {
                    Team_Name = "XILOTEPELT FC",
                    Team_Estadio = "Juan José Rodriguez",
                    Team_Image = "",

                });


                await _context.SaveChangesAsync();
            }

        }

        private async Task checkCorrectionActionAsync()
        {
            if (!_context.CorrectionActions.Any())
            {
                _context.CorrectionActions.Add(new CorrectionAction
                {
                    CorrectionAction_Name = "VERBAL",
                    CorrectionAction_Status = true,

                });
                _context.CorrectionActions.Add(new CorrectionAction
                {
                    CorrectionAction_Name = "ESCRITO",
                    CorrectionAction_Status = true,

                });
                _context.CorrectionActions.Add(new CorrectionAction
                {
                    CorrectionAction_Name = "DE BAJA",
                    CorrectionAction_Status = true,

                });
                await _context.SaveChangesAsync();
            }

        }

        private async Task checkUserAsync(string firstName, string lastName, string username, string email, string phone, string address, string userType)
        {
            AppUser user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                
                List<int> listaDeIds = new List<int> { 1, 2, 3, 4, 5, 6 };
              
                user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    User_FirstName = firstName,
                    User_LastName = lastName,
                    User_Address = address,
                    PhoneNumber = phone,
                    UserTypeofRole = userType,
                    User_Cedula = "041120686007T",
                    UserName = username,
                    User_Status = true,
                    User_CreatedTime = DateTime.Now,
                    Countryid = 1,
                    Stateid = 1,
                    Cityid = 1,
                    SelectedCategoryIds = listaDeIds,
                    User_FNC = DateTime.Now,
                    User_Genero = "Masculino",

                  };
          
                await _userHelper.AddUserAsync(user, "123456");

                await _userHelper.AddUserToRoleAsync(user, userType.ToString());


                //  string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                //  await _userHelper.ConfirmEmailAsync(user, token);
            }

            //return user;
        }


        private async Task checkRolesAsync()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("User");
            await _userHelper.CheckRoleAsync("Editor");
            await _userHelper.CheckRoleAsync("Dt");
        }

        //crear paises, departamentos y municipios por defecto
        private async Task checkCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                /* _context.Countries.Add(new Country
                 {
                     Country_Name = "Colombia",
                     States = new List<State>()
                     {
                         new State()
                         {
                             State_Name = "Antioquia",
                             Cities = new List<City>() {
                                 new City() { City_Name = "Medellín" },
                                 new City() { City_Name = "Itagüí" }
                             }
                         },
                         new State()
                         {
                             State_Name = "Bogotá",
                             Cities = new List<City>() {
                                 new City() { City_Name = "Usaquen" },
                                 new City() { City_Name = "Champinero" },
                                 new City() { City_Name = "Santa fe" },
                                 new City() { City_Name = "Useme" },
                                 new City() { City_Name = "Bosa" },
                             }
                         },
                     }
                 });
                 */

                _context.Countries.Add(new Country
                {
                    Country_Name = "Nicaragua",
                    States = new List<State>()
                    {
                       /* new State()
                        {
                            State_Name = "Managua",
                            Cities = new List<City>() {
                                new City() { City_Name = "Orlando" },
                                new City() { City_Name = "Miami" },
                                new City() { City_Name = "Tampa" },
                                new City() { City_Name = "Fort Lauderdale" },
                                new City() { City_Name = "Key West" },
                            }
                        },*/
                        new State()
                        {
                            State_Name = "CARAZO",
                            Cities = new List<City>() {
                                new City() { City_Name = "DIRIAMBA" },
                                new City() { City_Name = "DOLORES" },
                                new City() { City_Name = "EL ROSARIO" },
                                new City() { City_Name = "JINOTEPE" },
                                new City() { City_Name = "LA CONQUISTA" },
                                new City() { City_Name = "LA PAZ DE CARAZO" },
                                new City() { City_Name = "SAN MARCOS" },
                                new City() { City_Name = "SANTA TERESA" },
                            }
                        },
                    }
                });

                await _context.SaveChangesAsync();
            }
        }



    }
}
