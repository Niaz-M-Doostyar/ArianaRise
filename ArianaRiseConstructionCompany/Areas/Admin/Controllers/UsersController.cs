using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using ArianaRiseConstructionCompany.Models.Db;

namespace ArianaRiseConstructionCompany.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private ArianaRiseCCEntities dbEntities = new ArianaRiseCCEntities();
        // GET: Admin/Users
        public ActionResult Index(string message = "")
        {
            ViewBag.Model = dbEntities.AspNetUsers.ToList();
            return View();
        }
        public ActionResult Delete(string id)
        {
            try
            {
                var res = dbEntities.AspNetUsers.Find(id);
                dbEntities.AspNetUsers.Remove(res);
                dbEntities.SaveChanges();
                return RedirectToAction("Index", new { message = "Record deleted successfully" });

            }
            catch
            {
                return RedirectToAction("Index", new { message = "Record not deleted" });
            }
        }
    }
}
