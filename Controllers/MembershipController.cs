using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Helpers.Interfaces;
using xilopro2.Models;
using xilopro2.Models.toCharts;

namespace xilopro2.Controllers
{
    public class MembershipController : Controller
    {
        private readonly DataContext _context;

        public MembershipController(DataContext context)
        {
            _context = context;
        }

        #region Membresia

        public async Task<IActionResult> Index()
        {
            membresiascant();
           // var response = _context.Memberships.ToList();
            IActionResult response = _context.Memberships != null ?
               View(await _context.Memberships
                   .Include(c => c.Payments)
                   .OrderBy(c => c.MemberLastName) // Agregar esta línea para ordenar por nombre
                   .ToListAsync()) :
                Problem("Entity set 'DataContext.Countries' is null.");
            return response;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Membership membership = await _context.Memberships
                .Include(c => c.Payments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        public IActionResult Create()
        {
          //  Membership membership = new() { Payments = new List<Payments>() };
          //  return View(membership);
            return View(new MembershipViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,MemberFirstName,MemberLastName, MembershipType,StartDate,EndDate,Status")] MembershipViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     Membership obj = new()
                    {
                        Id = model.Id,
                        EndDate = model.EndDate,
                        StartDate = model.StartDate,
                        MemberFirstName = model.MemberFirstName?.Trim().ToUpper(),
                         MemberLastName = model.MemberLastName?.Trim().ToUpper(),
                         MembershipType = model.MembershipType,
                         Status = model.Status,
                         Membership_FullName = $@"{model.MemberFirstName?.Trim().ToUpper()} {model.MemberLastName?.Trim().ToUpper()}",
                    };
                    _context.Add(obj);
                    _context.SaveChanges();
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(String.Empty, "Ya existe una membresia con esos nombres");
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
                TempData["successMem"] = "Se creo membresia de " + model.Membership_FullName + " exitosamente!!";
              //  return RedirectToAction(nameof(DetailsGroup), new { Id = model.GroupId });
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Memberships == null)
            {
                return NotFound();
            }
            var membership = await _context.Memberships.FindAsync(id);
            if (membership == null)
            {
                return NotFound();
            }
            MembershipViewModel model = new()
            {
                Id = membership.Id,
                StartDate = membership.StartDate,
                EndDate = membership.EndDate,
                MemberFirstName = membership.MemberFirstName?.Trim().ToUpper(),
                MemberLastName = membership.MemberLastName?.Trim().ToUpper(),
                MembershipType = membership.MembershipType,
                Status = membership.Status,
                Membership_FullName = $@"{membership.MemberFirstName?.Trim().ToUpper()} {membership.MemberLastName?.Trim().ToUpper()}",
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,MemberFirstName,MemberLastName,MembershipType,StartDate,EndDate,Status,Membership_FullName")] Membership membership)
        {
            if (ModelState.IsValid)
            {
               
                try
                {
                   /* Country countryobj = new()
                    {
                        Country_ID = country.Country_ID,
                        Country_Name = country.Country_Name?.Trim().ToUpper(),
                    };*/
                    _context.Update(membership);
                    _context.SaveChanges();
                    TempData["successMem"] = "Se edito membresia exitosamente!!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(String.Empty, "Ya existe una membresia con el mismo nombre");
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

            return View(membership);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Memberships == null)
            {
                return NotFound();
            }

            var membership = await _context.Memberships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membership = await _context.Memberships.FindAsync(id);

            if (membership == null)
            {
                return NotFound();
            }

            try
            {
                _context.Memberships.Remove(membership);
                await _context.SaveChangesAsync();
                TempData["successMem"] = "Membresia eliminada!!";
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction(nameof(Index));
        }



        #endregion


        #region Payments

        public async Task<IActionResult> AddPayment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Membership membership = await _context.Memberships.FindAsync(id);
            if (membership == null)
            {
                return NotFound();
            }

            PaymentViewModel model = new()
            {
               MembersId = membership.Id,
               MembershipFullName = membership.Membership_FullName,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPayment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Payments pay = new()
                    {
                      //  Id = model.ID,
                        PaymentMethod = model.PaymentMethod,
                        PaymentAmount = model.PaymentAmount,
                        PaymentDate = model.PaymentDate,
                        PaymentStatus = model.PaymentStatus,
                        MembershipId = model.MembersId, //await _context.Countries.MaxAsync(c => c.Country_ID) + 1, // manually set the CountryId value
                        
                    };
                    _context.Add(pay);

                    try
                    {
                        await _context.SaveChangesAsync();
                       // TempData["successState"] = "Pago " + state.State_Name + " creado!!";
                        TempData["successMem"] = "Pago agregado a la membresia de " + model.MembershipFullName + " exitosamente!!";
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                  /*  Country country = await _context.Countries
                        .Include(c => c.States)
                        .ThenInclude(s => s.Cities)
                        .FirstOrDefaultAsync(c => c.Country_ID == model.CountryId);*/

                    return RedirectToAction(nameof(Details), new { Id = model.MembersId });
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

        public async Task<IActionResult> EditPayment(int? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var pay = await _context.Payments.FindAsync(id);
            if (pay == null)
            {
                return NotFound();
            }
            PaymentViewModel model = new()
            {
                ID = pay.Id,
                MembersId = pay.MembershipId,
                PaymentAmount = pay.PaymentAmount,
                PaymentDate = pay.PaymentDate,
                PaymentMethod = pay.PaymentMethod,
                PaymentStatus = pay.PaymentStatus,
                MembershipFullName = _context.Memberships
                                .Where(torneo => torneo.Id == pay.Id)
                                .Select(torneo => torneo.Membership_FullName)
                                .FirstOrDefault(),
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPayment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Payments pay = new() {
                Id = model.ID,
                PaymentAmount = model.PaymentAmount,
                PaymentDate = model.PaymentDate,
                PaymentMethod = model.PaymentMethod,
                PaymentStatus = model.PaymentStatus,
                MembershipId = model.MembersId,
               
                };
               
                try
                {
                    _context.Update(pay);
                    await _context.SaveChangesAsync();
                    TempData["successMem"] = "Pago de " + model.MembershipFullName + " editado!!";
                    // return RedirectToAction(nameof(Details));
                    return RedirectToAction(nameof(Details), new { Id = model.MembersId });
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Already there is a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }

            return View(model);
        }


        public async Task<IActionResult> DeletePayment(int? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var pay = await _context.Payments.FirstOrDefaultAsync(m => m.Id == id);

            PaymentViewModel model = new()
            {
                ID = pay.Id,
                MembersId = pay.MembershipId,
                PaymentAmount = pay.PaymentAmount,
                PaymentDate = pay.PaymentDate,
                PaymentMethod = pay.PaymentMethod,
                PaymentStatus = pay.PaymentStatus,
                MembershipFullName = _context.Memberships
                                .Where(torneo => torneo.Id == pay.Id)
                                .Select(torneo => torneo.Membership_FullName)
                                .FirstOrDefault(),
            };

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [HttpPost, ActionName("DeletePayment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedPayment(int id)
        {
            if (_context.Payments == null)
            {
                return Problem("Entity set 'DataContext.Payments'  is null.");
            }
            var pay = await _context.Payments.FindAsync(id);
            if (pay != null)
            {
                _context.Payments.Remove(pay);
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["successMem"] = "Pago eliminado!!";
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Details), new { Id = pay.MembershipId });
        }

        public async Task<IActionResult> DetailsPayment(int? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var pay = await _context.Payments.FindAsync(id);
            if (pay == null)
            {
                return NotFound();
            }
            PaymentViewModel model = new()
            {
                ID = pay.Id,
                MembersId = pay.MembershipId,
                PaymentAmount = pay.PaymentAmount,
                PaymentDate = pay.PaymentDate,
                PaymentMethod = pay.PaymentMethod,
                PaymentStatus = pay.PaymentStatus,
                MembershipFullName = _context.Memberships
                                 .Where(torneo => torneo.Id == pay.Id)
                                 .Select(torneo => torneo.Membership_FullName)
                                 .FirstOrDefault(),
            };


            return View(model);
        }


        #endregion


        #region Charts
        public IActionResult membresiascant()
        {
            var Lista = _context.Memberships
                     .GroupBy(m => m.Status)
                     .Select(group => new
                     {
                         Status = group.Key,
                         Count = group.Count()
                     })
                     .ToList();

            // List<JugadoresxCatViewModel> Lista = _dataContext.Torneos.ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }


        #endregion

    }
}
