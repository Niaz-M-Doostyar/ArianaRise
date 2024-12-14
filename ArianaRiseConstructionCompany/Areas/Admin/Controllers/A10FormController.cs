using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArianaRiseConstructionCompany.Helpers;
using ArianaRiseConstructionCompany.Models.Db;
using Microsoft.AspNet.Identity;

namespace ArianaRiseConstructionCompany.Areas.Admin.Controllers
{
    public class A10FormController : Controller
    {
        private ArianaRiseCCEntities dbEntities = new ArianaRiseCCEntities();
        // GET: Admin/A10Form
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            ViewBag.Model = dbEntities.tbl_shawahed.ToList();
            return View();
        }
        [Authorize(Roles = "Admin")]
        // GET: Admin/A10Form/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize(Roles = "Admin, User")]
        // GET: Admin/A10Form/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin, User")]
        // POST: Admin/A10Form/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase[] files)
        {
            using (DbContextTransaction transaction = dbEntities.Database.BeginTransaction())
            {

                try
                {
                    List<tbl_sakhtamane_mawad> sakhtamaneMawads = new List<tbl_sakhtamane_mawad>();
                    List<tbl_aradajat> aradajats = new List<tbl_aradajat>();
                    List<tbl_karaya> karayas = new List<tbl_karaya>();
                    List<tbl_masrafe_motafaraqa_mawad> masrafeMotafaraqaMawads = new List<tbl_masrafe_motafaraqa_mawad>();
                    List<tbl_motar_tarmim_aw_normasayal> motarTarmimAwNormasayals = new List<tbl_motar_tarmim_aw_normasayal>();
                    List<tbl_mashenare_karaya> mashenareKarayas = new List<tbl_mashenare_karaya>();
                    List<tbl_sahawe_daftar_karaya_aw_mashat> sahaweDaftarKarayaAwMashats = new List<tbl_sahawe_daftar_karaya_aw_mashat>();
                    List<tbl_da_kar_da_parmakhtag_jadwal> daKarDaParmakhtagJadwals = new List<tbl_da_kar_da_parmakhtag_jadwal>();
                    tbl_da_nan_wraze_karz daNanWrazeKarz;
                    tbl_shawahed shawahedAndComment;

                    Guid a10FormId = Guid.NewGuid();
                    daNanWrazeKarz = new tbl_da_nan_wraze_karz()
                    {
                        Id = Guid.NewGuid(),
                        DaNanWrazeQart = double.Parse(collection["H_DaNanQarz1"].ToString().ValidateA10FormConversions()),
                        PardakhtSawe = double.Parse(collection["H_DaNanPardakhtSawe1"].ToString().ValidateA10FormConversions()),
                        Total = double.Parse(collection["H_TotalPrice1"].ToString().ValidateA10FormConversions()),
                        Comment = collection["H_Comment1"].ToString().ValidateA10FormStringConversions(),
                        A10FormId = a10FormId,
                        UserId = User.Identity.GetUserId()
                    };
                    dbEntities.tbl_da_nan_wraze_karz.Add(daNanWrazeKarz);

                    for (int i = 1; i <= 8; i++)
                    {
                        sakhtamaneMawads.Add(new tbl_sakhtamane_mawad()
                        {
                            Id = Guid.NewGuid(),
                            Name = collection["A_Name" + i].ToString().ValidateA10FormStringConversions(),
                            Comment = collection["A_Comment" + i].ToString().ValidateA10FormStringConversions(),
                            Unit = collection["A_Unit" + i].ToString().ValidateA10FormStringConversions(),
                            Quantity = Convert.ToInt32(collection["A_Quantity" + i].ToString().ValidateA10FormConversions()),
                            PricePerUnit = double.Parse(collection["A_PricePerUnit" + i].ToString().ValidateA10FormConversions()),
                            TotalPrice = Convert.ToInt32(collection["A_Quantity" + i].ToString().ValidateA10FormConversions()) *
                                         double.Parse(collection["A_PricePerUnit" + i].ToString().ValidateA10FormConversions()),
                            CreatedDate = DateTime.Now,
                            A10FormId = a10FormId,
                            UserId = User.Identity.GetUserId()
                        });
                    }
                    dbEntities.tbl_sakhtamane_mawad.AddRange(sakhtamaneMawads);

                    for (int i = 1; i <= 6; i++)
                    {
                        aradajats.Add(new tbl_aradajat()
                        {
                            Id = Guid.NewGuid(),
                            Name = collection["B_Name" + i].ToString().ValidateA10FormStringConversions(),
                            Comment = collection["B_Comment" + i].ToString().ValidateA10FormStringConversions(),
                            Unit = collection["B_Unit" + i].ToString().ValidateA10FormStringConversions(),
                            Quantity = Convert.ToInt32(collection["B_Quantity" + i].ToString().ValidateA10FormConversions()),
                            PricePerUnit = double.Parse(collection["B_PricePerUnit" + i].ToString().ValidateA10FormConversions()),
                            TotalPrice = Convert.ToInt32(collection["B_Quantity" + i].ToString().ValidateA10FormConversions()) *
                                         double.Parse(collection["B_PricePerUnit" + i].ToString().ValidateA10FormConversions()),
                            CreatedDate = DateTime.Now,
                            A10FormId = a10FormId,
                            UserId = User.Identity.GetUserId()
                        }); 
                        daKarDaParmakhtagJadwals.Add(new tbl_da_kar_da_parmakhtag_jadwal()
                        {
                            Id = Guid.NewGuid(),
                            SakhtamaneFaliyatona = collection["J_Name" + i].ToString().ValidateA10FormStringConversions(),
                            Comment = collection["J_Comment" + i].ToString().ValidateA10FormStringConversions(),
                            Unit = collection["J_Unit" + i].ToString().ValidateA10FormStringConversions(),
                            MakhkeneKarHojam = Convert.ToInt32(collection["J_Quantity" + i].ToString().ValidateA10FormConversions()),
                            DaNanWrazeDaKarHojam = Convert.ToInt32(collection["J_PricePerUnit" + i].ToString().ValidateA10FormConversions()),
                            TotalHojam = string.Format("{0}",Convert.ToInt32(collection["J_Quantity" + i].ToString().ValidateA10FormConversions()) +
                                                             Convert.ToInt32(collection["J_PricePerUnit" + i].ToString().ValidateA10FormConversions())),
                            CreatedDate = DateTime.Now,
                            A10FormId = a10FormId,
                            UserId = User.Identity.GetUserId()
                        });
                    }
                    dbEntities.tbl_aradajat.AddRange(aradajats);
                    dbEntities.tbl_da_kar_da_parmakhtag_jadwal.AddRange(daKarDaParmakhtagJadwals);
                    for (int i = 1; i <= 3; i++)
                    {
                        karayas.Add(new tbl_karaya()
                        {
                            Id = Guid.NewGuid(),
                            Name = collection["C_Name" + i].ToString().ValidateA10FormStringConversions(),
                            Comment = collection["C_Comment" + i].ToString().ValidateA10FormStringConversions(),
                            TotalPrice = Convert.ToInt32(collection["C_TotalPrice" + i].ToString().ValidateA10FormConversions()),
                            CreatedDate = DateTime.Now,
                            A10FormId = a10FormId,
                            UserId = User.Identity.GetUserId()
                        });
                        masrafeMotafaraqaMawads.Add(new tbl_masrafe_motafaraqa_mawad()
                        {
                            Id = Guid.NewGuid(),
                            Name = collection["D_Name" + i].ToString().ValidateA10FormStringConversions(),
                            Comment = collection["D_Comment" + i].ToString().ValidateA10FormStringConversions(),
                            TotalPrice = Convert.ToInt32(collection["D_TotalPrice" + i].ToString().ValidateA10FormConversions()),
                            CreatedDate = DateTime.Now,
                            A10FormId = a10FormId,
                            UserId = User.Identity.GetUserId()
                        });
                        motarTarmimAwNormasayals.Add(new tbl_motar_tarmim_aw_normasayal()
                        {
                            Id = Guid.NewGuid(),
                            Name = collection["E_Name" + i].ToString().ValidateA10FormStringConversions(),
                            Comment = collection["E_Comment" + i].ToString().ValidateA10FormStringConversions(),
                            TotalPrice = Convert.ToInt32(collection["E_TotalPrice" + i].ToString().ValidateA10FormConversions()),
                            CreatedDate = DateTime.Now,
                            A10FormId = a10FormId,
                            UserId = User.Identity.GetUserId()
                        });
                        mashenareKarayas.Add(new tbl_mashenare_karaya()
                        {
                            Id = Guid.NewGuid(),
                            Name = collection["F_Name" + i].ToString().ValidateA10FormStringConversions(),
                            Comment = collection["F_Comment" + i].ToString().ValidateA10FormStringConversions(),
                            Quantity = Convert.ToInt32(collection["F_Quantity" + i].ToString().ValidateA10FormConversions()),
                            PricePerUnit = double.Parse(collection["F_PricePerUnit" + i].ToString().ValidateA10FormConversions()),
                            TotalPrice = Convert.ToInt32(collection["F_Quantity" + i].ToString().ValidateA10FormConversions()) *
                                         double.Parse(collection["F_PricePerUnit" + i].ToString().ValidateA10FormConversions()),
                            CreatedDate = DateTime.Now,
                            A10FormId = a10FormId,
                            UserId = User.Identity.GetUserId()
                        });
                    }
                    dbEntities.tbl_karaya.AddRange(karayas);
                    dbEntities.tbl_masrafe_motafaraqa_mawad.AddRange(masrafeMotafaraqaMawads);
                    dbEntities.tbl_motar_tarmim_aw_normasayal.AddRange(motarTarmimAwNormasayals);
                    dbEntities.tbl_mashenare_karaya.AddRange(mashenareKarayas);
                    for (int i = 1; i <= 4; i++)
                    {
                        sahaweDaftarKarayaAwMashats.Add(new tbl_sahawe_daftar_karaya_aw_mashat()
                        {
                            Id = Guid.NewGuid(),
                            Name = collection["G_Name" + i].ToString().ValidateA10FormStringConversions(),
                            Comment = collection["G_Comment" + i].ToString().ValidateA10FormStringConversions(),
                            Unit = collection["G_Unit" + i].ToString().ValidateA10FormStringConversions(),
                            Quantity = Convert.ToInt32(collection["G_Quantity" + i].ToString().ValidateA10FormConversions()),
                            PricePerUnit = double.Parse(collection["G_PricePerUnit" + i].ToString().ValidateA10FormConversions()),
                            TotalPrice = Convert.ToInt32(collection["G_Quantity" + i].ToString().ValidateA10FormConversions()) *
                                         double.Parse(collection["G_PricePerUnit" + i].ToString().ValidateA10FormConversions()),
                            CreatedDate = DateTime.Now,
                            A10FormId = a10FormId,
                            UserId = User.Identity.GetUserId()
                        });
                    }
                    dbEntities.tbl_sahawe_daftar_karaya_aw_mashat.AddRange(sahaweDaftarKarayaAwMashats);
                
                    string fileNames = String.Empty;
                    if (files != null)
                    {
                        Random counter = new Random();
                        foreach (HttpPostedFileBase file in files)
                        {
                            string path = Server.MapPath("/Content/Uploads/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            var InputFileName = counter.Next(1, 1000000000) + Path.GetFileName(file.FileName);
                            fileNames = fileNames + ";" + InputFileName;
                            var ServerSavePath = Path.Combine(path + InputFileName);
                            file.SaveAs(ServerSavePath);
                            ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        }
                    }

                    shawahedAndComment = new tbl_shawahed()
                    {
                        Id = Guid.NewGuid(),
                        Url = fileNames,
                        Comment = collection["Comments"].ToString().ValidateA10FormStringConversions(),
                        OfficerName = collection["FinentialOfficer"].ToString().ValidateA10FormStringConversions(),
                        FormNumber = collection["FormNumber"].ToString().ValidateA10FormStringConversions(),
                        Day = collection["Day"].ToString().ValidateA10FormStringConversions(),
                        A10FormId = a10FormId,
                        CreatedDate = DateTime.Now,
                        UserId = User.Identity.GetUserId()
                    };
                    dbEntities.tbl_shawahed.Add(shawahedAndComment);
                    dbEntities.SaveChanges();
                    transaction.Commit();
                    ViewBag.Message = "Data Added Successfully";
                    return View("Create");
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    ViewBag.Message = "Data Not Added";
                    return View("Create");
                }

            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            using (DbContextTransaction transaction = dbEntities.Database.BeginTransaction())
            {
                try
                {
                    List<tbl_sakhtamane_mawad> sakhtamaneMawads =
                        dbEntities.tbl_sakhtamane_mawad.Where(s => s.A10FormId.Equals(id)).ToList();
                    List<tbl_aradajat> aradajats = dbEntities.tbl_aradajat.Where(s => s.A10FormId.Equals(id)).ToList();
                    List<tbl_karaya> karayas = dbEntities.tbl_karaya.Where(s => s.A10FormId.Equals(id)).ToList();
                    List<tbl_masrafe_motafaraqa_mawad> masrafeMotafaraqaMawads = dbEntities.tbl_masrafe_motafaraqa_mawad
                        .Where(s => s.A10FormId.Equals(id)).ToList();
                    List<tbl_motar_tarmim_aw_normasayal> motarTarmimAwNormasayals =
                        dbEntities.tbl_motar_tarmim_aw_normasayal.Where(s => s.A10FormId.Equals(id)).ToList();
                    List<tbl_mashenare_karaya> mashenareKarayas =
                        dbEntities.tbl_mashenare_karaya.Where(s => s.A10FormId.Equals(id)).ToList();
                    List<tbl_sahawe_daftar_karaya_aw_mashat> sahaweDaftarKarayaAwMashats =
                        dbEntities.tbl_sahawe_daftar_karaya_aw_mashat.Where(s => s.A10FormId.Equals(id)).ToList();
                    List<tbl_da_kar_da_parmakhtag_jadwal> daKarDaParmakhtagJadwals =
                        dbEntities.tbl_da_kar_da_parmakhtag_jadwal.Where(s => s.A10FormId.Equals(id)).ToList();
                    tbl_da_nan_wraze_karz daNanWrazeKarz =
                        dbEntities.tbl_da_nan_wraze_karz.FirstOrDefault(s => s.A10FormId.Equals(id));
                    tbl_shawahed shawahedAndComment =
                        dbEntities.tbl_shawahed.FirstOrDefault(s => s.A10FormId.Equals(id));
                    

                    dbEntities.tbl_sakhtamane_mawad.RemoveRange(sakhtamaneMawads);
                    dbEntities.tbl_aradajat.RemoveRange(aradajats);
                    dbEntities.tbl_karaya.RemoveRange(karayas);
                    dbEntities.tbl_masrafe_motafaraqa_mawad.RemoveRange(masrafeMotafaraqaMawads);
                    dbEntities.tbl_motar_tarmim_aw_normasayal.RemoveRange(motarTarmimAwNormasayals);
                    dbEntities.tbl_mashenare_karaya.RemoveRange(mashenareKarayas);
                    dbEntities.tbl_sahawe_daftar_karaya_aw_mashat.RemoveRange(sahaweDaftarKarayaAwMashats);
                    dbEntities.tbl_da_kar_da_parmakhtag_jadwal.RemoveRange(daKarDaParmakhtagJadwals);
                    dbEntities.tbl_da_nan_wraze_karz.Remove(daNanWrazeKarz);
                    dbEntities.tbl_shawahed.Remove(shawahedAndComment);
                    dbEntities.SaveChanges();
                    transaction.Commit();
                    return RedirectToAction("Index", new {message = "Record deleted successfully"});
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    return RedirectToAction("Index", new {message = "Record not deleted"});

                }
            }
        }
    }
}
