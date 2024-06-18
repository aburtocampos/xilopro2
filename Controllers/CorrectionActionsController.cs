using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using xilopro2.Data;
using xilopro2.Data.Entities;

namespace xilopro2.Controllers
{
    public class CorrectionActionsController : Controller
    {
        private readonly DataContext _context;

        public CorrectionActionsController(DataContext context)
        {
            _context = context;
        }

        // GET: CorrectionActions
        public async Task<IActionResult> Index()
        {
            IActionResult response = null;
            if (_context.CorrectionActions != null)
            {
                // Fetch the list of CorrectionActions and include related entities
                var correctionActions = await _context.CorrectionActions.Include(cp => cp.Player).ToListAsync();
                response = View(correctionActions); // Assuming you have a corresponding view for CorrectionActions
            }
            else
            {
                // Return a problem detail if _context.CorrectionActions is null
                response = Problem("Entity set 'DataContext.CorrectionActions' is null.", statusCode: 500);
            }
            return response;
        }

        // GET: CorrectionActions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CorrectionActions == null)
            {
                return NotFound();
            }

            CorrectionAction correctionAction = await _context.CorrectionActions
                .FirstOrDefaultAsync(m => m.CorrectionAction_ID == id);
            if (correctionAction == null)
            {
                return NotFound();
            }

            return View(correctionAction);
        }

        // GET: CorrectionActions/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CorrectionAction_ID,CorrectionAction_Name,CorrectionAction_Status")] CorrectionAction correctionAction)
        {
            ModelState.Remove("CorrectionAction_ID");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(correctionAction);
                    await _context.SaveChangesAsync();
                    TempData["successCorrectionActions"] = "Accion Correctiva " + correctionAction.CorrectionAction_Name + " creada!!";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(String.Empty, "Ya existe una Accion Correctiva con el mismo nombre");
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
            return View(correctionAction);
        }

        // GET: CorrectionActions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CorrectionActions == null)
            {
                return NotFound();
            }

            var correctionAction = await _context.CorrectionActions.FindAsync(id);
            if (correctionAction == null)
            {
                return NotFound();
            }
            return View(correctionAction);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CorrectionAction_ID,CorrectionAction_Name,CorrectionAction_Status")] CorrectionAction correctionAction)
        {
            if (id != correctionAction.CorrectionAction_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(correctionAction);
                    await _context.SaveChangesAsync();
                    TempData["successCorrectionActions"] = "Accion Correctiva " + correctionAction.CorrectionAction_Name + " Editada!!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(String.Empty, "Ya existe una Accion Correctiva con el mismo nombre");
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
            return View(correctionAction);
        }

        // GET: CorrectionActions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CorrectionActions == null)
            {
                return NotFound();
            }

            var correctionAction = await _context.CorrectionActions
                .FirstOrDefaultAsync(m => m.CorrectionAction_ID == id);
            if (correctionAction == null)
            {
                return NotFound();
            }

            return View(correctionAction);
        }

        // POST: CorrectionActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CorrectionActions == null)
            {
                return Problem("Entity set 'DataContext.CorrectionActions'  is null.");
            }
            var correctionAction = await _context.CorrectionActions.FindAsync(id);
            if (correctionAction != null)
            {
                _context.CorrectionActions.Remove(correctionAction);
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["successCorrectionActions"] = "Accion Correctiva " + correctionAction.CorrectionAction_Name + " Eliminada!!";
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CorrectionActionExists(int id)
        {
          return (_context.CorrectionActions?.Any(e => e.CorrectionAction_ID == id)).GetValueOrDefault();
        }



    }
}
