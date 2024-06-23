using Microsoft.EntityFrameworkCore;
using System;
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
            await checkRolesAsync();
            await checkPlayers();
            //creacion de usuarios con roles
            await checkUserAsync("super", "man", "eabucam@gmail.com", "eabucam@gmail.com", "88503574", "calle luna", "Admin", "0411206860007T");
            await checkUserAsync("Editor1", "Editor1", "pekiabu@gmail.com", "pekiabu@gmail.com", "76092930", "calle sol", "Editor", "0411206860007T");
            await feedTorneo();
        }

        //generador de texto
        static string GenerateLoremIpsum(int numberOfWords)
        {
            string[] loremIpsumWords = new string[]
            {
            "lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipiscing", "elit", "sed", "do",
            "eiusmod", "tempor", "incididunt", "ut", "labore", "et", "dolore", "magna", "aliqua", "ut",
            "enim", "ad", "minim", "veniam", "quis", "nostrud", "exercitation", "ullamco", "laboris",
            "nisi", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis", "aute", "irure",
            "dolor", "in", "reprehenderit", "in", "voluptate", "velit", "esse", "cillum", "dolore",
            "eu", "fugiat", "nulla", "pariatur", "excepteur", "sint", "occaecat", "cupidatat",
            "non", "proident", "sunt", "in", "culpa", "qui", "officia", "deserunt", "mollit", "anim",
            "id", "est", "laborum"
            };

            Random random = new Random();
            string loremIpsum = string.Join(" ", Enumerable.Range(0, numberOfWords).Select(_ => loremIpsumWords[random.Next(loremIpsumWords.Length)]));
            return loremIpsum;
        }

        static string GenerateRandomNumbersandLetter(Random random, int length, bool includeLetter)
        {
            string numberorletter = string.Concat(Enumerable.Range(0, length)
                                      .Select(_ => random.Next(0, 10).ToString()));

            if (includeLetter)
            {
                char randomLetter = (char)random.Next(65, 91);
                 numberorletter += randomLetter;
            }

            return numberorletter;
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
                //C
                _context.Teams.Add(new Team
                {
                    Team_Name = "REAL GRANADA FC",
                    Team_Estadio = "LAS COCECHAS",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "MASAYA FC",
                    Team_Estadio = "",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "COFRADIA FC",
                    Team_Estadio = "",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "CD NANDASMO",
                    Team_Estadio = "NINDIRI",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "XILOTEPELT FC",
                    Team_Estadio = "JUAN JOSE RODRIGUEZ",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "MINSA FC",
                    Team_Estadio = "ESCUELA DE TALENTOS DIRIAMBA",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "SAN MARCOS FC",
                    Team_Estadio = "EL COMAL",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "ATLETICO NACIONAL",
                    Team_Estadio = "RIVAS",
                    Team_Image = "",

                });

                //B
                _context.Teams.Add(new Team
                {
                    Team_Name = "MINA FC",
                    Team_Estadio = "CAMPO SANTA LUCIA LARREYNAGA",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "EL SAUCE FC",
                    Team_Estadio = "SAUCE",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "CD CHINANDEGA",
                    Team_Estadio = "",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "CHICHIGALPA FC",
                    Team_Estadio = "CHICHIGALPA",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "CD SANTO TOMAS",
                    Team_Estadio = "SANTO TOMAS DEL NORTE",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "PUMAS FC",
                    Team_Estadio = "CIUDAD DARIO",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "DM SEBACO",
                    Team_Estadio = "SEBACO",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "AQMERICA OAR",
                    Team_Estadio = "ESTELI",
                    Team_Image = "",

                });


                //A
                _context.Teams.Add(new Team
                {
                    Team_Name = "DEPORTIVO LAS SABANAS FC",
                    Team_Estadio = "MADRIZ",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "MUNICIPAL SAN LUCAS FC",
                    Team_Estadio = "SANTA LUCAS",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "ATLETICO PIOLIN",
                    Team_Estadio = "SOMOTO",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "SAN BARTOLO FC",
                    Team_Estadio = "QUILALI",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "FC BRUMAS",
                    Team_Estadio = "JINOTEGA",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "FC YALI",
                    Team_Estadio = "SAN SEBASTIAN DE YALI",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "DEPORTIVO EL CUA",
                    Team_Estadio = "EL CUA",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "FC BOCAY",
                    Team_Estadio = "SAN JOSE DE BOCAY",
                    Team_Image = "",

                });

                //D
                _context.Teams.Add(new Team
                {
                    Team_Name = "AMERICA FC",
                    Team_Estadio = "MANAGUA",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "FC BAMBINOS",
                    Team_Estadio = "MANAGUA",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "DEPORTIVO VILLA",
                    Team_Estadio = "MANAGUA",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "CIUDAD SANDINO SLIDER FC",
                    Team_Estadio = "CIUDAD SANDINO",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "ACD REAL XOLOTLAN",
                    Team_Estadio = "SAN RAFAEL DEL SUR",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "FC TIPITAPA",
                    Team_Estadio = "TIPITAPA",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "BLUEFIELDS FC",
                    Team_Estadio = "BLUEFIELDS",
                    Team_Image = "",

                });
                _context.Teams.Add(new Team
                {
                    Team_Name = "RIO SAN JUAN FC",
                    Team_Estadio = "SAN CARLOS",
                    Team_Image = "",

                });


                await _context.SaveChangesAsync();
            }

        }


        private async Task checkUserAsync(string firstName, string lastName, string username, string email, string phone, string address, string userType, string cedula)
        {
            AppUser user = await _userHelper.GetUserAsyncbyEmail(email);
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
                    User_Cedula = cedula,
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
               _context.Countries.Add(new Country
                 {
                     Country_Name = "COLOMBIA",
                     States = new List<State>()
                     {
                         new State()
                         {
                             State_Name = "ANTIOQUIA",
                             Cities = new List<City>() {
                                 new City() { City_Name = "MEDELLIN" },
                                 new City() { City_Name = "ITAGUI" }
                             }
                         },
                         new State()
                         {
                             State_Name = "BOGOTA",
                             Cities = new List<City>() {
                                 new City() { City_Name = "USAQUEN" },
                                 new City() { City_Name = "CHAMPINERO" },
                                 new City() { City_Name = "SANTA FE" },
                                 new City() { City_Name = "USEME" },
                                 new City() { City_Name = "BOSA" },
                             }
                         },
                     }
                 });
                 

                _context.Countries.Add(new Country
                {
                    Country_Name = "NICARAGUA",
                    States = new List<State>()
                    {
                         new State()
                        {
                            State_Name = "BOACO",
                            Cities = new List<City>() {
                                new City() { City_Name = "BOACO" },
                                new City() { City_Name = "CAMOAPA" },
                                new City() { City_Name = "SAN JOSE DE LOS REMATES" },
                                new City() { City_Name = "SAN LORENZO" },
                                new City() { City_Name = "SANTA LUCIA" },
                                new City() { City_Name = "TEUSTEPE" },
                            }
                        },
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
                        new State()
                        {
                            State_Name = "CHINANDEGA",
                            Cities = new List<City>() {
                                new City() { City_Name = "CHICHIGALPA" },
                                new City() { City_Name = "CHINANDEGA" },
                                new City() { City_Name = "CINCO PINOS" },
                                new City() { City_Name = "CORINTO" },
                                new City() { City_Name = "EL REALEJO" },
                                new City() { City_Name = "EL VIEJO" },
                                new City() { City_Name = "POSOLTEGA" },
                                new City() { City_Name = "PUERTO MORAZAN" },
                                new City() { City_Name = "SAN FRANCISO DEL NORTE" },
                                new City() { City_Name = "SAN PEDRO DEL NORTE" },
                                new City() { City_Name = "SANT TOMAS DEL NORTE" },
                                new City() { City_Name = "SOMOTILLO" },
                                new City() { City_Name = "VILLANUEVA" },
                            }
                        },
                        new State()
                        {
                            State_Name = "CHONTALES",
                            Cities = new List<City>() {
                                new City() { City_Name = "ACOYAPA" },
                                new City() { City_Name = "COMALAPA" },
                                new City() { City_Name = "CUAPA" },
                                new City() { City_Name = "EL CORAL" },
                                new City() { City_Name = "JUIGALPA" },
                                new City() { City_Name = "LA LIBERTAD" },
                                new City() { City_Name = "SAN PEDRO DE LOVAGO" },
                                new City() { City_Name = "SANTO DOMINGO" },
                                new City() { City_Name = "SANTO TOMAS" },
                                new City() { City_Name = "VILLA SANDINO" },
                            }
                        },
                         new State()
                        {
                            State_Name = "CARIBE NORTE",
                            Cities = new List<City>() {
                                new City() { City_Name = "BONANZA" },
                                new City() { City_Name = "MULUKUKU" },
                                new City() { City_Name = "PRINZAPOLKA" },
                                new City() { City_Name = "PUERTO CABEZAS" },
                                new City() { City_Name = "ROSITA" },
                                new City() { City_Name = "SIUNA" },
                                new City() { City_Name = "WASLALA" },
                                new City() { City_Name = "WASPAN" },
                            }
                        },
                          new State()
                        {
                            State_Name = "CARIBE SUR",
                            Cities = new List<City>() {
                                new City() { City_Name = "BLUEFIELDS" },
                                new City() { City_Name = "CORN ISLAND" },
                                new City() { City_Name = "DESEMBOCADURA DE RIO GRANDE" },
                                new City() { City_Name = "EL AYOTE" },
                                new City() { City_Name = "EL RAMA" },
                                new City() { City_Name = "EL TORTUGUERO" },
                                new City() { City_Name = "KUKRA HILL" },
                                new City() { City_Name = "LA CRIUZ DE RIO GANDE" },
                                new City() { City_Name = "LAGUNA DE PERLAS" },
                                new City() { City_Name = "MUELLE DE LOS BUEYES" },
                                new City() { City_Name = "NUEVA GUINEA" },
                                new City() { City_Name = "PAIWAS" },
                            }
                        },
                         new State()
                        {
                            State_Name = "ESTELI",
                            Cities = new List<City>() {
                                new City() { City_Name = "CONDEGA" },
                                new City() { City_Name = "ESTELI" },
                                new City() { City_Name = "LA TRINIDAD" },
                                new City() { City_Name = "PUEBLO NUEVO" },
                                new City() { City_Name = "SAN JUAN DE LIMAY" },
                                new City() { City_Name = "SAN NICOLAS" },
                            }
                        },
                          new State()
                        {
                            State_Name = "GRANADA",
                            Cities = new List<City>() {
                                new City() { City_Name = "DIRIA" },
                                new City() { City_Name = "DIRIOMO" },
                                new City() { City_Name = "GRANADA" },
                                new City() { City_Name = "NANDAIME" },
                            }
                        },
                            new State()
                        {
                            State_Name = "JINOTEGA",
                            Cities = new List<City>() {
                                new City() { City_Name = "EL CUA" },
                                new City() { City_Name = "JINOTEGA" },
                                new City() { City_Name = "LA CONCONRDIA" },
                                new City() { City_Name = "SAN JOSE DE BOCAY" },
                                new City() { City_Name = "SAN RAFAEL DEL NORTE" },
                                new City() { City_Name = "SAN SEBASTIAN DE YALI" },
                                new City() { City_Name = "SANTA MARIA DE PANTASMA" },
                                new City() { City_Name = "WIWILI DE JINOTEGA" },
                            }
                        },
                              new State()
                        {
                            State_Name = "LEON",
                            Cities = new List<City>() {
                                new City() { City_Name = "ACHUAPA" },
                                new City() { City_Name = "EL JICARAL" },
                                new City() { City_Name = "EL SAUCE" },
                                new City() { City_Name = "LA PAZ CENTRO" },
                                new City() { City_Name = "LARREYNAGA" },
                                new City() { City_Name = "LEON" },
                                new City() { City_Name = "NAGAROTE" },
                                new City() { City_Name = "QUEZALGUAQUE" },
                                new City() { City_Name = "SANTA ROSA DEL PEÑON" },
                                new City() { City_Name = "TELICA" },
                            }
                        },
                                new State()
                        {
                            State_Name = "MADRIZ",
                            Cities = new List<City>() {
                                new City() { City_Name = "LAS SABANAS" },
                                new City() { City_Name = "PALACAGUINA" },
                                new City() { City_Name = "SAN JOSE DE CUSMAPA" },
                                new City() { City_Name = "SAN JUAN DE RIO COCO" },
                                new City() { City_Name = "SAN LUCAS" },
                                new City() { City_Name = "SOMOTO" },
                                new City() { City_Name = "TELPANECA" },
                                new City() { City_Name = "TOTOGALPA" },
                                new City() { City_Name = "YALAGUINA" },
                            }
                        },
                          new State()
                        {
                            State_Name = "MANAGUA",
                            Cities = new List<City>() {
                                new City() { City_Name = "SAN FRANCISCO LIBRE" },
                                new City() { City_Name = "TIPITAPA" },
                                new City() { City_Name = "MATEARE" },
                                new City() { City_Name = "MANAGUA" },
                                new City() { City_Name = "CIUDAD SANDINO" },
                                new City() { City_Name = "TICUANTEPE" },
                                new City() { City_Name = "EL CRUCERO" },
                                new City() { City_Name = "SAN RAFAEL DEL SUR" },
                                new City() { City_Name = "VILLA EL CARMEN" },
                            }
                        },
                         new State()
                        {
                            State_Name = "MASAYA",
                            Cities = new List<City>() {
                                new City() { City_Name = "CATARINA" },
                                new City() { City_Name = "LA CONCEPCION" },
                                new City() { City_Name = "MASATEPE" },
                                new City() { City_Name = "MASAYA" },
                                new City() { City_Name = "NANDASMO" },
                                new City() { City_Name = "NINDIRI" },
                                new City() { City_Name = "NIQUINOHOMO" },
                                new City() { City_Name = "SAN JUAN DE ORIENTE" },
                                new City() { City_Name = "TISMA" },
                            }
                        },
                           new State()
                        {
                            State_Name = "MATAGALPA",
                            Cities = new List<City>() {
                                new City() { City_Name = "CIUDAD DARIO" },
                                new City() { City_Name = "EL TUMA LA DALIA" },
                                new City() { City_Name = "ESQUIPULAS" },
                                new City() { City_Name = "MATAGALPA" },
                                new City() { City_Name = "MATIGUAS" },
                                new City() { City_Name = "MUY MUY" },
                                new City() { City_Name = "RANCHO GRANDE" },
                                new City() { City_Name = "RIO BLANCO" },
                                new City() { City_Name = "SAN DIONISIO" },
                                new City() { City_Name = "SAN ISIDRO" },
                                new City() { City_Name = "SAN RAMON" },
                                new City() { City_Name = "SEBACO" },
                                new City() { City_Name = "TERRABONA" },
                            }
                        },
                             new State()
                        {
                            State_Name = "NUEVA SEGOVIA",
                            Cities = new List<City>() {
                                new City() { City_Name = "CIUDAD ANTIGUA" },
                                new City() { City_Name = "DIPILTO" },
                                new City() { City_Name = "EL JICARO" },
                                new City() { City_Name = "JALAPA" },
                                new City() { City_Name = "MACUELIZO" },
                                new City() { City_Name = "MOZONTE" },
                                new City() { City_Name = "MURRA" },
                                new City() { City_Name = "OCOTAL" },
                                new City() { City_Name = "QUILALI" },
                                new City() { City_Name = "SAN FERNANDO" },
                                new City() { City_Name = "SANTA MARIA" },
                                new City() { City_Name = "WIWILI" },
                            }
                        },
                               new State()
                        {
                            State_Name = "RIO SAN JUAN",
                            Cities = new List<City>() {
                                new City() { City_Name = "EL ALMENDRO" },
                                new City() { City_Name = "EL CASTILLO" },
                                new City() { City_Name = "MORRITO" },
                                new City() { City_Name = "SAN CARLOS" },
                                new City() { City_Name = "SAN JUAN DEL NORTE" },
                                new City() { City_Name = "SAN MIGUELITO" },
                            }
                        },
                                 new State()
                        {
                            State_Name = "RIVAS",
                            Cities = new List<City>() {
                                new City() { City_Name = "ALTAGRACIA" },
                                new City() { City_Name = "BELEN" },
                                new City() { City_Name = "BUENOS AIRES" },
                                new City() { City_Name = "CARDENAS" },
                                new City() { City_Name = "MOYOGALPA" },
                                new City() { City_Name = "POTOSI" },
                                new City() { City_Name = "RIVAS" },
                                new City() { City_Name = "SAN JORGE" },
                                new City() { City_Name = "SAN JUAN DEL SUR" },
                                new City() { City_Name = "TOLA" },
                            }
                        },


                    }
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task feedTorneo()
        {
            if (!_context.Torneos.Any())
            {
                List<int> lsCatids = new List<int>() { 1, 2, 3, 4, 5, 6 };

                _context.Torneos.Add(new Torneo
                {
                    Torneo_Name = "Liga 2",
                    Torneo_Season = "2024",
                    Torneo_SeasonType = "APERTURA",
                    SelectedCategoryIds = new List<int> { lsCatids[0] },
                    Torneo_Status = true,
                    Torneo_StartDate = DateTime.Now,
                    Torneo_EndDate = DateTime.Now,
                    Groups = new List<Groups>()
                     {
                         new Groups()
                         {
                             Group_Name = "A",
                             torneoId = 1,
                         },
                          new Groups()
                         {
                             Group_Name = "B",
                              torneoId = 1,
                         },
                     }
                });

              _context.Torneos.Add(new Torneo
                {
                    Torneo_Name = "COPA PRIMERA",
                    Torneo_Season = "2024",
                    Torneo_SeasonType = null,
                    SelectedCategoryIds = new List<int> { lsCatids[0] },
                    Torneo_Status = true,
                    Torneo_StartDate = DateTime.Now,
                    Torneo_EndDate = DateTime.Now,
                    Groups = new List<Groups>()
                     {
                         new Groups()
                         {
                             Group_Name = "DIECISEISAVOS DE FINAL",
                              torneoId = 2,
                         },
                          new Groups()
                         {
                             Group_Name = "OCTAVOS DE FINAL",
                              torneoId = 2,
                         },
                          new Groups()
                         {
                             Group_Name = "CUARTOS DE FINAL",
                              torneoId = 2,
                         },
                           new Groups()
                         {
                             Group_Name = "SEMIFINAL",
                              torneoId = 2,
                         },
                            new Groups()
                         {
                             Group_Name = "FINAL",
                              torneoId = 2,
                         },
                     }
                });

                  _context.Torneos.Add(new Torneo
                  {
                      Torneo_Name = "campeonato ligas menores",
                      Torneo_Season = "2024",
                      Torneo_SeasonType = null,
                      SelectedCategoryIds = new List<int> { lsCatids[1] },
                      Torneo_Status = true,
                      Torneo_StartDate = DateTime.Now,
                      Torneo_EndDate = DateTime.Now,
                      Groups = new List<Groups>()
                       {
                           new Groups()
                           {
                               Group_Name = "FASE DE GRUPOS",
                                torneoId = 3,
                           },
                            new Groups()
                           {
                               Group_Name = "CUARTOS DE FINAL",
                                torneoId = 3,
                           },
                             new Groups()
                           {
                               Group_Name = "SEMIFINAL",
                                torneoId = 3,
                           },
                              new Groups()
                           {
                               Group_Name = "FINAL",
                                torneoId = 3,
                           },
                       }
                  });

                  _context.Torneos.Add(new Torneo
                   {
                       Torneo_Name = "campeonato ligas menores",
                       Torneo_Season = "2024",
                       Torneo_SeasonType = null,
                       SelectedCategoryIds = new List<int> { lsCatids[2] },
                       Torneo_Status = true,
                       Torneo_StartDate = DateTime.Now,
                       Torneo_EndDate = DateTime.Now,
                       Groups = new List<Groups>()
                        {
                            new Groups()
                            {
                                Group_Name = "FASE DE GRUPOS",
                                 torneoId = 4,
                            },
                             new Groups()
                            {
                                Group_Name = "CUARTOS DE FINAL",
                                 torneoId = 4,
                            },
                              new Groups()
                            {
                                Group_Name = "SEMIFINAL",
                                 torneoId = 4,
                            },
                               new Groups()
                            {
                                Group_Name = "FINAL",
                                 torneoId = 4,
                            },
                        }
                   });

                  _context.Torneos.Add(new Torneo
                   {
                       Torneo_Name = "campeonato ligas menores",
                       Torneo_Season = "2024",
                       Torneo_SeasonType = null,
                       SelectedCategoryIds = new List<int> { lsCatids[3] },
                       Torneo_Status = true,
                       Torneo_StartDate = DateTime.Now,
                       Torneo_EndDate = DateTime.Now,
                       Groups = new List<Groups>()
                        {
                            new Groups()
                            {
                                Group_Name = "FASE DE GRUPOS",
                                 torneoId = 5,
                            },
                             new Groups()
                            {
                                Group_Name = "CUARTOS DE FINAL",
                                  torneoId = 5,
                            },
                              new Groups()
                            {
                                Group_Name = "SEMIFINAL",
                                  torneoId = 5,
                            },
                               new Groups()
                            {
                                Group_Name = "FINAL",
                                  torneoId = 5,
                            },
                        }
                   });

                   _context.Torneos.Add(new Torneo
                   {
                       Torneo_Name = "campeonato ligas menores",
                       Torneo_Season = "2024",
                       Torneo_SeasonType = null,
                       SelectedCategoryIds = new List<int> { lsCatids[4] },
                       Torneo_Status = true,
                       Torneo_StartDate = DateTime.Now,
                       Torneo_EndDate = DateTime.Now,
                       Groups = new List<Groups>()
                        {
                            new Groups()
                            {
                                Group_Name = "FASE DE GRUPOS",
                                  torneoId = 6,
                            },
                             new Groups()
                            {
                                Group_Name = "CUARTOS DE FINAL",
                                 torneoId = 6,
                            },
                              new Groups()
                            {
                                Group_Name = "SEMIFINAL",
                                 torneoId = 6,
                            },
                               new Groups()
                            {
                                Group_Name = "FINAL",
                                 torneoId = 6,
                            },
                        }
                   });

                   _context.Torneos.Add(new Torneo
                   {
                       Torneo_Name = "campeonato ligas menores",
                       Torneo_Season = "2024",
                       Torneo_SeasonType = null,
                       SelectedCategoryIds = new List<int> { lsCatids[5] },
                       Torneo_Status = true,
                       Torneo_StartDate = DateTime.Now,
                       Torneo_EndDate = DateTime.Now,
                       Groups = new List<Groups>()
                        {
                            new Groups()
                            {
                                Group_Name = "FASE DE GRUPOS",
                                 torneoId = 7,
                            },
                             new Groups()
                            {
                                Group_Name = "CUARTOS DE FINAL",
                                 torneoId = 7,
                            },
                              new Groups()
                            {
                                Group_Name = "SEMIFINAL",
                                 torneoId = 7,
                            },
                               new Groups()
                            {
                                Group_Name = "FINAL",
                                 torneoId = 7,
                            },
                        }
                   });      

                await _context.SaveChangesAsync();
            }
        }

        private async Task checkPlayers()
        {
            if (!_context.Players.Any())
            {
                Random random = new Random();
                List<int> lsCatids = new List<int>() { 1, 2, 3, 4, 5, 6 };
                _context.Players.Add(new Player
                {
                    Player_FirstName = "PRIMER",
                    Player_LastName = "JUGADOR",
                    Player_Email = "primerjugador@gmail.com",
                    Player_Dorsal = 1,
                    Player_PhoneNumber = GenerateRandomNumbersandLetter(random, 8,false),
                    Player_Address = GenerateLoremIpsum(20),
                    Player_Genero = "Masculino",
                    Player_fifaid = GenerateRandomNumbersandLetter(random, 5,false),
                    Player_Cedula = GenerateRandomNumbersandLetter(random, 13, true),
                    SelectedCategoryIds = new List<int> { lsCatids[0] },
                    Teamid = 5,
                    Positionid = 1,
                    Countryid = 2,
                    Stateid = 4,
                    Cityid = 17,
                    Player_Status = true,
                    Player_FNC = new DateTime(1990, 5, 15),
                });
                _context.Players.Add(new Player
                {
                    Player_FirstName = "SEGUNDO",
                    Player_LastName = "JUGADOR",
                    Player_Email = "segundojugador@gmail.com",
                    Player_Dorsal = 2,
                    Player_PhoneNumber = GenerateRandomNumbersandLetter(random, 8, false),
                    Player_Address = GenerateLoremIpsum(20),
                    Player_Genero = "Masculino",
                    Player_fifaid = GenerateRandomNumbersandLetter(random, 5, false),
                    Player_Cedula = GenerateRandomNumbersandLetter(random, 13, true),
                    SelectedCategoryIds = new List<int> { lsCatids[0] },
                    Teamid = 5,
                    Positionid = 1,
                    Countryid = 2,
                    Stateid = 4,
                    Cityid = 17,
                    Player_Status = true,
                    Player_FNC = new DateTime(1990, 5, 15),
                });
                _context.Players.Add(new Player
                {
                    Player_FirstName = "TERCER",
                    Player_LastName = "JUGADOR",
                    Player_Email = "tercerjugador@gmail.com",
                    Player_Dorsal = 3,
                    Player_PhoneNumber = GenerateRandomNumbersandLetter(random, 8, false),
                    Player_Address = GenerateLoremIpsum(20),
                    Player_Genero = "Masculino",
                    Player_fifaid = GenerateRandomNumbersandLetter(random, 5, false),
                    Player_Cedula = GenerateRandomNumbersandLetter(random, 13, true),
                    SelectedCategoryIds = new List<int> { lsCatids[0] },
                    Teamid = 5,
                    Positionid = 1,
                    Countryid = 2,
                    Stateid = 4,
                    Cityid = 17,
                    Player_Status = true,
                    Player_FNC = new DateTime(1990, 5, 15),
                });
                _context.Players.Add(new Player
                {
                    Player_FirstName = "CUARTO",
                    Player_LastName = "JUGADOR",
                    Player_Email = "cuartojugador@gmail.com",
                    Player_Dorsal = 4,
                    Player_PhoneNumber = GenerateRandomNumbersandLetter(random, 8, false),
                    Player_Address = GenerateLoremIpsum(20),
                    Player_Genero = "Masculino",
                    Player_fifaid = GenerateRandomNumbersandLetter(random, 5, false),
                    Player_Cedula = GenerateRandomNumbersandLetter(random, 13, true),
                    SelectedCategoryIds = new List<int> { lsCatids[0] },
                    Teamid = 5,
                    Positionid = 1,
                    Countryid = 2,
                    Stateid = 4,
                    Cityid = 17,
                    Player_Status = true,
                    Player_FNC = new DateTime(1990, 5, 15),
                });
                _context.Players.Add(new Player
                {
                    Player_FirstName = "QUINTO",
                    Player_LastName = "JUGADOR",
                    Player_Email = "quintojugador@gmail.com",
                    Player_Dorsal = 5,
                    Player_PhoneNumber = GenerateRandomNumbersandLetter(random, 8, false),
                    Player_Address = GenerateLoremIpsum(20),
                    Player_Genero = "Masculino",
                    Player_fifaid = GenerateRandomNumbersandLetter(random, 5, false),
                    Player_Cedula = GenerateRandomNumbersandLetter(random, 13, true),
                    SelectedCategoryIds = new List<int> { lsCatids[0] },
                    Teamid = 5,
                    Positionid = 1,
                    Countryid = 2,
                    Stateid = 4,
                    Cityid = 17,
                    Player_Status = true,
                    Player_FNC = new DateTime(1990, 5, 15),
                });
                await _context.SaveChangesAsync();
            }
        }



    }
}
