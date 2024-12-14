using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArianaRiseConstructionCompany.Models.Db;

namespace ArianaRiseConstructionCompany.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        private ArianaRiseCCEntities dbEntity = new ArianaRiseCCEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                DateTime fromDate = DateTime.Parse(collection["fromDate"].ToString());
                DateTime toDate = DateTime.Parse(collection["toDate"].ToString());
                ViewBag.Model = dbEntity.tbl_shawahed.ToList().Where(d => d.CreatedDate.Value.Date >= fromDate.Date && d.CreatedDate.Value.Date <= toDate.Date).ToList();
                return View("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ProjectsReports()
        {
            return View(dbEntity.tbl_projects.ToList());
        }
        [HttpPost]
        public ActionResult ProjectsReports(FormCollection collection)
        {
            try
            {
                DateTime fromDate = DateTime.Parse(collection["fromDate"].ToString());
                DateTime toDate = DateTime.Parse(collection["toDate"].ToString());
                var model = dbEntity.tbl_projects.ToList().Where(d => d.CreatedDate.Value.Date >= fromDate.Date && d.CreatedDate.Value.Date <= toDate.Date).ToList();
                return View("ProjectsReports",model);
            }
            catch
            {
                return View("ProjectsReports");
            }
        }
        //public ActionResult ProjectsReportsDetails(Guid id)
        //{
        //    var user = dbEntity.AspNetUsers.ToList().Where(u => u.ProjectId.Equals(id)).First().Id;
            
        //    var sa = dbEntity.tbl_sakhtamane_mawad.Where(u=>u.UserId.Equals(user))
        //        .GroupBy(s => s.Name)
        //        .Select(x => 
        //            new
        //            {
        //                Name = x.Key, 
        //                TotalQuantity = x.Sum(s => s.Quantity), 
        //                TotalPrice = x.Sum(p => p.TotalPrice),
        //            }).ToList();
        //    return View();
        //}
    }
}
