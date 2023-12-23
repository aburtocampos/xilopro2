using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xilopro2.Data;
using xilopro2.Data.Entities;
using xilopro2.Helpers.Interfaces;
using xilopro2.Models;

namespace xilopro2.Controllers
{
    public class TorneosController : Controller
    {

        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IEntityModelConverter _entityModelConverter;

        public TorneosController(IUserHelper userHelper, DataContext context, IImageHelper imageHelper, IEntityModelConverter entityModelConverter)
        {
            _userHelper = userHelper;
            _context = context;
            _imageHelper = imageHelper;
            _entityModelConverter = entityModelConverter;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context
                .Torneos
                .Include(t=>t.Groups)
                .OrderBy(t=>t.Torneo_StartDate)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new TorneoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TorneoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tournament = _entityModelConverter.ToTournamentEntity(model, true);
                tournament.Torneo_Image =  _imageHelper.UploadImage(model.LogoFile, "Tournaments");
                try
                {
                    _context.Add(tournament);
                    await _context.SaveChangesAsync();
                    TempData["successTorneo"] = "Torneo " + tournament.Torneo_Name + " creado!!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, $"Already there is a record with the same name {tournament.Torneo_Name}.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Torneo tournamentEntity = await _context.Torneos.FindAsync(id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            TorneoViewModel model = _entityModelConverter.ToTournamentViewModel(tournamentEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TorneoViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = model.Torneo_Image;

                if (model.LogoFile != null)
                {
                    path =  _imageHelper.UploadImage(model.LogoFile, "Tournaments");
                }

                Torneo tournamentEntity = _entityModelConverter.ToTournamentEntity(model, false);
                _context.Update(tournamentEntity);
                await _context.SaveChangesAsync();
                TempData["successTorneo"] = "Torneo " + tournamentEntity.Torneo_Name + " editado!!";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentEntity = await _context.Torneos
                .FirstOrDefaultAsync(m => m.Torneo_ID == id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            _context.Torneos.Remove(tournamentEntity);
            await _context.SaveChangesAsync();
            TempData["successTorneo"] = "Torneo " + tournamentEntity.Torneo_Name + " eliminado!!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentEntity = await _context.Torneos
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Local)
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Visitor)
                .Include(t => t.Groups)
                .ThenInclude(t => t.GroupDetails)
                .FirstOrDefaultAsync(m => m.Torneo_ID == id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            return View(tournamentEntity);
        }





    }
}
