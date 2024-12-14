using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArianaRiseConstructionCompany.Areas.Admin.Models;
using ArianaRiseConstructionCompany.Models.Db;
using Microsoft.Reporting.WebForms;

namespace ArianaRiseConstructionCompany.Reports
{
    public partial class ProjectReportWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Guid id = new Guid(Request.QueryString["id"]);
                ArianaRiseCCEntities entities = new ArianaRiseCCEntities();
                using (var _context = entities)
                {
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    var user = _context.AspNetUsers.ToList().Where(u => u.ProjectId.Equals(id)).First().Id;

                    var sMawad = _context.tbl_sakhtamane_mawad.Where(u => u.UserId.Equals(user))
                        .GroupBy(s => s.Name)
                        .Select(x =>
                            new
                            {
                                Name = x.Key,
                                TotalQuantity = x.Sum(s => s.Quantity),
                                TotalPrice = x.Sum(p => p.TotalPrice)
                            }).ToList();
                    List<tbl_sakhtamane_mawad> sakhtamaneMawad = new List<tbl_sakhtamane_mawad>();
                    double perUnitPrice;
                    foreach (var temp in sMawad)
                    {
                        perUnitPrice = (double) (temp.TotalPrice / Convert.ToDouble(temp.TotalQuantity));
                        sakhtamaneMawad.Add(
                            new tbl_sakhtamane_mawad()
                            {
                                Name = temp.Name,TotalPrice = temp.TotalPrice,Quantity = temp.TotalQuantity,PricePerUnit = perUnitPrice
                            });
                    }


                    var danaerazeQarz = _context.tbl_da_nan_wraze_karz.Where(u => u.UserId.Equals(user))
                        .GroupBy(s => s.UserId)
                        .Select(x =>
                            new
                            {
                                Name = x.Key,
                                TotalPrice = x.Sum(p => p.Total)
                            }).ToList();
                    List<tbl_da_nan_wraze_karz> tbl_da_nan_wraze_karz = new List<tbl_da_nan_wraze_karz>();
                    foreach (var temp in danaerazeQarz)
                    {
                        tbl_da_nan_wraze_karz.Add(
                            new tbl_da_nan_wraze_karz()
                            {
                                Total = temp.TotalPrice
                            });
                    }

                    var aradajat = _context.tbl_aradajat.Where(u => u.UserId.Equals(user))
                        .GroupBy(s => s.Name)
                        .Select(x =>
                            new
                            {
                                Name = x.Key,
                                TotalQuantity = x.Sum(s => s.Quantity),
                                TotalPrice = x.Sum(p => p.TotalPrice)
                            }).ToList();
                    List<tbl_aradajat> tbl_aradajat = new List<tbl_aradajat>();
                    foreach (var temp in aradajat)
                    {
                        perUnitPrice = (double)(temp.TotalPrice / Convert.ToDouble(temp.TotalQuantity));
                        tbl_aradajat.Add(
                            new tbl_aradajat()
                            {
                                Name = temp.Name,
                                TotalPrice = temp.TotalPrice,
                                Quantity = temp.TotalQuantity,
                                PricePerUnit = perUnitPrice
                            });
                    }

                    var daKarDaParmakhtagJadwal = _context.tbl_da_kar_da_parmakhtag_jadwal.Where(u => u.UserId.Equals(user))
                        .GroupBy(s => s.SakhtamaneFaliyatona)
                        .AsEnumerable()
                        .Select(x =>
                            new
                            {
                                Name = x.Key,
                                TotalHojam = x.Sum(s => Convert.ToDouble(s.TotalHojam)),
                                DaNanWrazeDaKarHojam = x.Sum(p => p.DaNanWrazeDaKarHojam),
                                MakhkeneKarHojam = x.Sum(p => p.MakhkeneKarHojam),

                            }).ToList();
                    List<tbl_da_kar_da_parmakhtag_jadwal> tbl_da_kar_da_parmakhtag_jadwal = new List<tbl_da_kar_da_parmakhtag_jadwal>();
                    foreach (var temp in daKarDaParmakhtagJadwal)
                    {
                        tbl_da_kar_da_parmakhtag_jadwal.Add(
                            new tbl_da_kar_da_parmakhtag_jadwal()
                            {
                                SakhtamaneFaliyatona = temp.Name,
                                TotalHojam = temp.TotalHojam.ToString(),
                                DaNanWrazeDaKarHojam = temp.DaNanWrazeDaKarHojam,
                                MakhkeneKarHojam = temp.MakhkeneKarHojam
                            });
                    }
                    var mashenareKaraya = _context.tbl_mashenare_karaya.Where(u => u.UserId.Equals(user))
                        .GroupBy(s => s.Name)
                        .Select(x =>
                            new
                            {
                                Name = x.Key,
                                TotalQuantity = x.Sum(s => s.Quantity),
                                TotalPrice = x.Sum(p => p.TotalPrice)

                            }).ToList();
                    List<tbl_mashenare_karaya> tbl_mashenare_karaya = new List<tbl_mashenare_karaya>();
                    foreach (var temp in mashenareKaraya)
                    {
                        perUnitPrice = (double)(temp.TotalPrice / Convert.ToDouble(temp.TotalQuantity));
                        tbl_mashenare_karaya.Add(
                            new tbl_mashenare_karaya()
                            {
                                Name = temp.Name,
                                TotalPrice = temp.TotalPrice,
                                Quantity = temp.TotalQuantity,
                                PricePerUnit = perUnitPrice
                            });
                    }

                    var sahaweDaftar = _context.tbl_sahawe_daftar_karaya_aw_mashat.Where(u => u.UserId.Equals(user))
                        .GroupBy(s => s.Name)
                        .Select(x =>
                            new
                            {
                                Name = x.Key,
                                TotalQuantity = x.Sum(s => s.Quantity),
                                TotalPrice = x.Sum(p => p.TotalPrice)

                            }).ToList();
                    List<tbl_sahawe_daftar_karaya_aw_mashat> tbl_sahawe_daftar_karaya_aw_mashat = new List<tbl_sahawe_daftar_karaya_aw_mashat>();
                    foreach (var temp in sahaweDaftar)
                    {
                        perUnitPrice = (double)(temp.TotalPrice / Convert.ToDouble(temp.TotalQuantity));
                        tbl_sahawe_daftar_karaya_aw_mashat.Add(
                            new tbl_sahawe_daftar_karaya_aw_mashat()
                            {
                                Name = temp.Name,
                                TotalPrice = temp.TotalPrice,
                                Quantity = temp.TotalQuantity,
                                PricePerUnit = perUnitPrice
                            });
                    }

                    var motarTarmim = _context.tbl_motar_tarmim_aw_normasayal.Where(u => u.UserId.Equals(user))
                        .GroupBy(s => s.Name)
                        .Select(x =>
                            new
                            {
                                Name = x.Key,
                                TotalQuantity = x.Sum(s => s.Quantity),
                                TotalPrice = x.Sum(p => p.TotalPrice)

                            }).ToList();
                    List<tbl_motar_tarmim_aw_normasayal> tbl_motar_tarmim_aw_normasayal = new List<tbl_motar_tarmim_aw_normasayal>();
                    foreach (var temp in motarTarmim)
                    {
                        perUnitPrice = (double)(temp.TotalPrice / Convert.ToDouble(temp.TotalQuantity));
                        tbl_motar_tarmim_aw_normasayal.Add(
                            new tbl_motar_tarmim_aw_normasayal()
                            {
                                Name = temp.Name,
                                TotalPrice = temp.TotalPrice,
                                Quantity = temp.TotalQuantity,
                                PricePerUnit = perUnitPrice
                            });
                    }

                    var masarefMotar = _context.tbl_masrafe_motafaraqa_mawad.Where(u => u.UserId.Equals(user))
                        .GroupBy(s => s.Name)
                        .Select(x =>
                            new
                            {
                                Name = x.Key,
                                TotalQuantity = x.Sum(s => s.Quantity),
                                TotalPrice = x.Sum(p => p.TotalPrice)

                            }).ToList();
                    List<tbl_masrafe_motafaraqa_mawad> tbl_masrafe_motafaraqa_mawad = new List<tbl_masrafe_motafaraqa_mawad>();
                    foreach (var temp in masarefMotar)
                    {
                        perUnitPrice = (double)(temp.TotalPrice / Convert.ToDouble(temp.TotalQuantity));
                        tbl_masrafe_motafaraqa_mawad.Add(
                            new tbl_masrafe_motafaraqa_mawad()
                            {
                                Name = temp.Name,
                                TotalPrice = temp.TotalPrice,
                                Quantity = temp.TotalQuantity,
                                PricePerUnit = perUnitPrice
                            });
                    }

                    var karaya = _context.tbl_karaya.Where(u => u.UserId.Equals(user))
                        .GroupBy(s => s.Name)
                        .Select(x =>
                            new
                            {
                                Name = x.Key,
                                TotalPrice = x.Sum(p => p.TotalPrice)

                            }).ToList();
                    List<tbl_karaya> tbl_karaya = new List<tbl_karaya>();
                    foreach (var temp in karaya)
                    {
                        tbl_karaya.Add(
                            new tbl_karaya()
                            {
                                Name = temp.Name,
                                TotalPrice = temp.TotalPrice,
                            });
                    }
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/bin/ProjectReport.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc0 = new ReportDataSource("SakhtamaneMawadDataSet", sakhtamaneMawad);
                    ReportDataSource rdc1 = new ReportDataSource("DaNanWrazeQarzDataSet", tbl_da_nan_wraze_karz);
                    ReportDataSource rdc2 = new ReportDataSource("AradajatDataSet", tbl_aradajat);
                    ReportDataSource rdc3 = new ReportDataSource("DaKarDaParmakhtagJadwalDataSet", tbl_da_kar_da_parmakhtag_jadwal);
                    ReportDataSource rdc4 = new ReportDataSource("MashnareKarayaDataSet", tbl_mashenare_karaya);
                    ReportDataSource rdc6 = new ReportDataSource("SahaweDaftarDataSet", tbl_sahawe_daftar_karaya_aw_mashat);
                    ReportDataSource rdc7 = new ReportDataSource("MotarTarmimDataSet", tbl_motar_tarmim_aw_normasayal);
                    ReportDataSource rdc8 = new ReportDataSource("MasrafeMotarafaDataSet", tbl_masrafe_motafaraqa_mawad);
                    ReportDataSource rdc9 = new ReportDataSource("KarayaDataSet", tbl_karaya);
                    
                    ReportViewer1.LocalReport.DataSources.Add(rdc0);
                    ReportViewer1.LocalReport.DataSources.Add(rdc1);
                    ReportViewer1.LocalReport.DataSources.Add(rdc2);
                    ReportViewer1.LocalReport.DataSources.Add(rdc3);
                    ReportViewer1.LocalReport.DataSources.Add(rdc4);
                    ReportViewer1.LocalReport.DataSources.Add(rdc6);
                    ReportViewer1.LocalReport.DataSources.Add(rdc7);
                    ReportViewer1.LocalReport.DataSources.Add(rdc8);
                    ReportViewer1.LocalReport.DataSources.Add(rdc9);

                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.DataBind();
                }
            }

        }
        public static byte[] ImageToBinary(string imagePath)
        {
            FileStream fS = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] b = new byte[fS.Length];
            fS.Read(b, 0, (int)fS.Length);
            fS.Close();
            return b;
        }
    }
}