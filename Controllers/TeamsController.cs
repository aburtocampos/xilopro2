using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Helpers.Interfaces;
using xilopro2.Models;

namespace xilopro2.Controllers
{
    public class TeamsController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IEntityModelConverter _entityModelConverter;

        public TeamsController(DataContext context, IImageHelper imageHelper, IEntityModelConverter entityModelConverter)
        {
            _context = context;
            _imageHelper = imageHelper;
            _entityModelConverter = entityModelConverter;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            IActionResult response = _context.Teams != null ? 
                          View(await _context.Teams.ToListAsync()) :
                          Problem("Entity set 'DataContext.Teams'  is null.");
            return response;
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            Team team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Team_ID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View(new TeamViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamViewModel model)
        {
            if (ModelState.IsValid)
            {
                var team = _entityModelConverter.ToTeamEntity(model, true);
                team.Team_Image = _imageHelper.UploadImage(model.LogoFile, "Teams");
                try
                {
                    _context.Add(team);
                    await _context.SaveChangesAsync();
                    TempData["successTeam"] = "Equipo " + team.Team_Name + " creado!!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, $"Already there is a record with the same name {team.Team_Name}.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }

            return View(model);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            TeamViewModel model = _entityModelConverter.ToTeamViewModel(team);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeamViewModel model)
        {
            if (ModelState.IsValid)
            {
                Team team = new Team();
                team = _entityModelConverter.ToTeamEntity(model, false);
                if (model.LogoFile != null)
                {//si carga archivo
                    if (!string.IsNullOrEmpty(team.Team_Image))
                    {
                        if (_imageHelper.DeleteImage(team.Team_Image, "Teams"))
                        {
                            team.Team_Image = _imageHelper.UploadImage(model.LogoFile, "Teams");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Teams");
                        }
                    }
                    else
                    {
                        team.Team_Image = _imageHelper.UploadImage(model.LogoFile, "Teams");
                    }
                }
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                    TempData["successTeam"] = "Equipo " + team.Team_Name + " editado!!";
                    return RedirectToAction(nameof(Index));
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

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Team_ID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teams == null)
            {
                return Problem("Entity set 'DataContext.Teams'  is null.");
            }
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _imageHelper.DeleteImage(team.Team_Image, "Teams");
                _context.Teams.Remove(team);
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["successTeam"] = "Equipo " + team.Team_Name + " Eliminado!!";
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
          return (_context.Teams?.Any(e => e.Team_ID == id)).GetValueOrDefault();
        }



    }
}
