using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArianaRiseConstructionCompany;
using ArianaRiseConstructionCompany.Models.Db;

namespace ArianaRiseConstructionCompany.Areas.Admin.Controllers
{
    public class tbl_sakhtamane_mawadController : Controller
    {
        private ArianaRiseCCEntities db = new ArianaRiseCCEntities();

        // GET: Admin/tbl_sakhtamane_mawad
        public ActionResult Index()
        {
            return View(db.tbl_sakhtamane_mawad.ToList());
        }

        // GET: Admin/tbl_sakhtamane_mawad/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_sakhtamane_mawad tbl_sakhtamane_mawad = db.tbl_sakhtamane_mawad.Find(id);
            if (tbl_sakhtamane_mawad == null)
            {
                return HttpNotFound();
            }
            return View(tbl_sakhtamane_mawad);
        }

        // GET: Admin/tbl_sakhtamane_mawad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/tbl_sakhtamane_mawad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Unit,Quantity,PricePerUnit,TotalPrice,Comment,CreatedDate")] tbl_sakhtamane_mawad tbl_sakhtamane_mawad)
        {
            if (ModelState.IsValid)
            {
                tbl_sakhtamane_mawad.Id = Guid.NewGuid();
                db.tbl_sakhtamane_mawad.Add(tbl_sakhtamane_mawad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_sakhtamane_mawad);
        }

        // GET: Admin/tbl_sakhtamane_mawad/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_sakhtamane_mawad tbl_sakhtamane_mawad = db.tbl_sakhtamane_mawad.Find(id);
            if (tbl_sakhtamane_mawad == null)
            {
                return HttpNotFound();
            }
            return View(tbl_sakhtamane_mawad);
        }

        // POST: Admin/tbl_sakhtamane_mawad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Unit,Quantity,PricePerUnit,TotalPrice,Comment,CreatedDate")] tbl_sakhtamane_mawad tbl_sakhtamane_mawad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_sakhtamane_mawad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_sakhtamane_mawad);
        }

        // GET: Admin/tbl_sakhtamane_mawad/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_sakhtamane_mawad tbl_sakhtamane_mawad = db.tbl_sakhtamane_mawad.Find(id);
            if (tbl_sakhtamane_mawad == null)
            {
                return HttpNotFound();
            }
            return View(tbl_sakhtamane_mawad);
        }

        // POST: Admin/tbl_sakhtamane_mawad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tbl_sakhtamane_mawad tbl_sakhtamane_mawad = db.tbl_sakhtamane_mawad.Find(id);
            db.tbl_sakhtamane_mawad.Remove(tbl_sakhtamane_mawad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
