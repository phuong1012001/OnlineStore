using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Cms.Controllers
{
    public class StockEventController : Controller
    {
        // GET: StockEventController
        public ActionResult Index()
        {
            return View();
        }

        // GET: StockEventController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StockEventController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockEventController/Create
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

        // GET: StockEventController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StockEventController/Edit/5
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

        // GET: StockEventController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StockEventController/Delete/5
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
