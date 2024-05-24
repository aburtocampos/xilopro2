using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace xilopro2.Controllers
{
    public class PlayerStatisticsController : Controller
    {
        // GET: PlayerStatisticsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PlayerStatisticsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlayerStatisticsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlayerStatisticsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlayerStatisticsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlayerStatisticsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlayerStatisticsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlayerStatisticsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
