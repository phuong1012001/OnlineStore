using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Cms.Controllers
{
    public class StockController : Controller
    {
        // GET: StoclController
        public ActionResult Index()
        {
            return View();
        }

        // GET: StoclController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StoclController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoclController/Create
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

        // GET: StoclController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StoclController/Edit/5
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

        // GET: StoclController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoclController/Delete/5
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
