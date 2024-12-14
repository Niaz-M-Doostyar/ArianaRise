using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArianaRiseConstructionCompany.Models.Db;

namespace ArianaRiseConstructionCompany.Areas.Admin.Controllers
{
    public class ProjectController : Controller
    {
        private ArianaRiseCCEntities db = new ArianaRiseCCEntities();

        // GET: Admin/Project
        public ActionResult Index()
        {
            return View(db.tbl_projects.ToList());
        }

        // GET: Admin/Project/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_projects tbl_projects = db.tbl_projects.Find(id);
            if (tbl_projects == null)
            {
                return HttpNotFound();
            }
            return View(tbl_projects);
        }
        // GET: Admin/Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Location")] tbl_projects tbl_projects)
        {
            if (ModelState.IsValid)
            {
                tbl_projects.Id = Guid.NewGuid();
                tbl_projects.CreatedDate = DateTime.Now;
                db.tbl_projects.Add(tbl_projects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_projects);
        }

        // GET: Admin/Project/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_projects tbl_projects = db.tbl_projects.Find(id);
            if (tbl_projects == null)
            {
                return HttpNotFound();
            }
            return View(tbl_projects);
        }

        // POST: Admin/Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Location,CreatedDate")] tbl_projects tbl_projects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_projects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_projects);
        }

        // GET: Admin/Project/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_projects tbl_projects = db.tbl_projects.Find(id);
            if (tbl_projects == null)
            {
                return HttpNotFound();
            }
            return View(tbl_projects);
        }

        // POST: Admin/Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tbl_projects tbl_projects = db.tbl_projects.Find(id);
            db.tbl_projects.Remove(tbl_projects);
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
