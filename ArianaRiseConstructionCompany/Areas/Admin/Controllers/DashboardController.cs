using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArianaRiseConstructionCompany.Helpers;
using ArianaRiseConstructionCompany.Models.Db;

namespace ArianaRiseConstructionCompany.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private ArianaRiseCCEntities db = new ArianaRiseCCEntities();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.TotalFormsCount = db.tbl_shawahed.GroupBy(s => s.A10FormId).Count();
            ViewBag.TotalDailyFormsCount = db.tbl_shawahed.ToList().Where(d => d.CreatedDate.Value.Date.Equals(DateTime.Today.Date)).Count();
            List<Guid> totalFormsIds = db.tbl_shawahed.ToList().Where(d => d.CreatedDate.Value.Date.Equals(DateTime.Today.Date)).Select(e=>e.A10FormId).ToList();
            double sum = 0;
            foreach (Guid formId in totalFormsIds)
            {
                sum += ConversionValidation.GetTotalById(formId);
            }
            ViewBag.TotalDailyFormsTotalPrice = sum;
            return View();
        }

        // GET: Admin/Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Dashboard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Dashboard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Dashboard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
