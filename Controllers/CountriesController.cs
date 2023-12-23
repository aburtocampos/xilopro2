using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Models;

namespace xilopro2.Controllers
{
    public class CountriesController : Controller
    {

        private readonly DataContext _context;
        public CountriesController(DataContext context)
        {
            _context = context;
        }

        #region Country

        public async Task<IActionResult> Index(string message)
        {

            IActionResult response = _context.Countries != null ?
                 View(await _context.Countries
                     .Include(c => c.States)
                     .OrderBy(c => c.Country_Name) // Agregar esta línea para ordenar por nombre
                     .ToListAsync()) :
                  Problem("Entity set 'DataContext.Countries' is null.");

            return response;
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Country country = await _context.Countries
                .Include(c => c.States)
                .ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(m => m.Country_ID == id);

            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            Country country = new() { States = new List<State>() };
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Country obj = new()
                    {
                        Country_ID = country.Country_ID,
                        Country_Name = country.Country_Name.ToUpper(),
                    };
                    _context.Add(obj);
                    await _context.SaveChangesAsync();
                    TempData["successCountry"] = "Pais " + country.Country_Name + " creado!!";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(String.Empty, "Ya existe un país con el mismo nombre");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Country country)
        {
            if (id != country.Country_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    TempData["successCountry"] = "País " + country.Country_Name + " Editado!!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(String.Empty, "Ya existe un país con el mismo nombre");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }

            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Country_ID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'DataContext.Countries'  is null.");
            }
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["successCountry"] = "País " + country.Country_Name + " Eliminado!!";
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return (_context.Countries?.Any(e => e.Country_ID == id)).GetValueOrDefault();
        }

        #endregion


        #region States

        public async Task<IActionResult> AddState(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Country country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            StateViewModel model = new()
            {
                CountryId = country.Country_ID,
                CountryName = country.Country_Name,
            };



            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddState(StateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    State state = new()
                    {
                        Cities = new List<City>(),
                        Country = await _context.Countries.FindAsync(model.CountryId),
                        State_Name = model.State_Name.ToUpper(),
                    };
                    _context.Add(state);
                    try
                    {
                        await _context.SaveChangesAsync();
                        TempData["successState"] = "Departamento " + state.State_Name + " creado!!";
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    Country country = await _context.Countries
                        .Include(c => c.States)
                        .ThenInclude(s => s.Cities)
                        .FirstOrDefaultAsync(c => c.Country_ID == model.CountryId);
                    return RedirectToAction(nameof(Details), new { Id = model.CountryId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(String.Empty, "Ya existe un departamento con el mismo nombre en este país");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> EditState(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            State state = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(s => s.State_ID == id);

            if (state == null)
            {
                return NotFound();
            }

            StateViewModel model = new()
            {
                CountryId = state.Country.Country_ID,
                State_Name = state.State_Name.ToUpper(),
                State_ID = state.State_ID,
                CountryName = state.Country.Country_Name,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditState(int id, StateViewModel model)
        {
            //  ModelState.Remove("Cities");
            // ModelState.Remove("Country");
            if (id != model.State_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    State state = new()
                    {
                        State_ID = model.State_ID,
                        State_Name = model.State_Name.ToUpper(),
                    };

                    _context.Update(state);
                    await _context.SaveChangesAsync();
                    TempData["successState"] = "Departamento " + state.State_Name + " editado!!";
                    return RedirectToAction(nameof(Details), new { Id = model.CountryId });

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(String.Empty, "Ya existe un departamento con el mismo nombre");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }

            }
            return View(model);

        }


        public async Task<IActionResult> DetailsState(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            State state = await _context.States
                .Include(s => s.Country)
                .Include(s => s.Cities)
                 .OrderBy(c => c.State_Name)
                .FirstOrDefaultAsync(m => m.State_ID == id);

            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }


        public async Task<IActionResult> DeleteState(int? id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            State state = await _context.States
              .Include(s => s.Country)
              .Include(s => s.Cities)
              .FirstOrDefaultAsync(m => m.State_ID == id);

            if (state == null)
            {
                return NotFound();
            }
            return View(state);
            // return RedirectToAction(nameof(Details), new { id = state.Country.Country_ID });
        }

        [HttpPost, ActionName("DeleteState")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStateConfirmed(int id)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'DataContext.States'  is null.");
            }
            var country = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.State_ID == id);

            var state = await _context.States.FindAsync(id);
            if (state != null)
            {
                _context.States.Remove(state);
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["successState"] = "Departamento " + state.State_Name + " eliminado!!";
            }
            catch (Exception)
            {
                throw;
            }

            // return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Details), new { id = country.Country.Country_ID });
        }


        #endregion


        #region City

        public async Task<IActionResult> AddCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            State state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            CityViewModel model = new()
            {
                StateId = state.State_ID,
                StateName = state.State_Name,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCity(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    City city = new()
                    {
                        City_ID = model.City_ID,
                        City_Name = model.City_Name.ToUpper(),
                        State = await _context.States.FindAsync(model.StateId),
                        
                    };
                    _context.Add(city);
                    try
                    {
                        await _context.SaveChangesAsync();
                        TempData["successCity"] = "Municipio " + city.City_Name + " creado!!";
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    /*    Country country = await _context.Countries
                            .Include(c => c.States)
                            .ThenInclude(s => s.Cities)
                            .FirstOrDefaultAsync(c => c.Country_ID == model.CountryId);*/

                    return RedirectToAction(nameof(DetailsState), new { Id = model.StateId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(String.Empty, "Ya existe un departamento con el mismo nombre en este país");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditCity(int? id)
        {
            City city = await _context.Cities
                 .Include(s => s.State)
                 .FirstOrDefaultAsync(s => s.City_ID == id);

            if (city == null)
            {
                return NotFound();
            }

            CityViewModel model = new()
            {
                StateId = city.State.State_ID,
                StateName = city.State.State_Name,
                City_ID = city.City_ID,
                City_Name = city.City_Name,
                //  CountryName = state.Country.Country_Name,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCity(int id, CityViewModel model)
        {
            if (id != model.City_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // State state = await _context.States.FindAsync(id);

                    City city = new()
                    {
                        City_ID = model.City_ID,
                        City_Name = model.City_Name.ToUpper(),
                        State = await _context.States.FindAsync(model.StateId),
                    };

                    try
                    {
                        _context.Cities.Update(city);
                        await _context.SaveChangesAsync();
                        TempData["successCity"] = "Municipio " + city.City_Name + " editado!!";
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    /*    State state = await _context.States
                            .Include(s => s.Cities)
                            .FirstOrDefaultAsync(c => c.State_ID == model.StateId);

                          Country country = await _context.Countries
                              .Include(c => c.States)
                              .ThenInclude(s => s.Cities)
                              .FirstOrDefaultAsync(c => c.Country_ID == model.CountryId);*/

                    return RedirectToAction(nameof(DetailsState), new { Id = model.StateId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(String.Empty, "Ya existe un departamento con el mismo nombre en este país");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            City city = await _context.Cities
              .Include(s => s.State)
              .FirstOrDefaultAsync(m => m.City_ID == id);

            if (city == null)
            {
                return NotFound();
            }
            return View(city);
            // return RedirectToAction(nameof(Details), new { id = state.Country.Country_ID });
        }

        [HttpPost, ActionName("DeleteCity")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCityConfirmed(int id)
        {
            if (_context.States == null)
            {
                return Problem("Entity set 'DataContext.States'  is null.");
            }
            var state = await _context.States
                .Include(s => s.Cities)
                .FirstOrDefaultAsync(m => m.State_ID == id);

            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["successCity"] = "Municipio " + city.City_Name + " eliminado!!";
            }
            catch (Exception)
            {
                throw;
            }

            // return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(DetailsState), new { id = state.State_ID });
        }

        #endregion


    }
}
