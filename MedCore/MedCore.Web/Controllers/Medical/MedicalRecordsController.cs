using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedCore.Web.Controllers.Medical
{
    public class MedicalRecordsController : Controller
    {
        // GET: MedicalRecordsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MedicalRecordsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MedicalRecordsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicalRecordsController/Create
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

        // GET: MedicalRecordsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MedicalRecordsController/Edit/5
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

        // GET: MedicalRecordsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MedicalRecordsController/Delete/5
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
