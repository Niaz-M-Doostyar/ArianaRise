using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArianaRiseConstructionCompany.Areas.Admin.Models;
using ArianaRiseConstructionCompany.Models.Db;
using Microsoft.Reporting.WebForms;

namespace ArianaRiseConstructionCompany.Reports
{
    public partial class ReportWebForm : System.Web.UI.Page
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
                    List<string> r = _context.tbl_shawahed.Where(u => u.A10FormId.Equals(id)).ToList()[0].Url.Split(';').ToList();
                    r.RemoveAt(0);
                    List<ShawahedImagesDsModel> images = new List<ShawahedImagesDsModel>();
                   
                    for (int i = 0; i < r.Count; i++)
                    {
                        images.Add(new ShawahedImagesDsModel()
                        {
                            Id = 1,
                            ImageUrl = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Uploads/" + r[i]))
                        });
                    }

                    
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/bin/A10FormReport.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc0 = new ReportDataSource("SakhtamaneMawadDataSet", _context.tbl_sakhtamane_mawad.Where(u=>u.A10FormId.Equals(id)).ToList());
                    ReportDataSource rdc1 = new ReportDataSource("DaNanWrazeQarzDataSet", _context.tbl_da_nan_wraze_karz.Where(u => u.A10FormId.Equals(id)).ToList());
                    ReportDataSource rdc2 = new ReportDataSource("AradajatDataSet", _context.tbl_aradajat.Where(u => u.A10FormId.Equals(id)).ToList());
                    ReportDataSource rdc3 = new ReportDataSource("DaKarDaParmakhtagJadwalDataSet", _context.tbl_da_kar_da_parmakhtag_jadwal.Where(u => u.A10FormId.Equals(id)).ToList());
                    ReportDataSource rdc4 = new ReportDataSource("MashnareKarayaDataSet", _context.tbl_mashenare_karaya.Where(u => u.A10FormId.Equals(id)).ToList());
                    ReportDataSource rdc5 = new ReportDataSource("ShawahedDataSet", _context.tbl_shawahed.Where(u => u.A10FormId.Equals(id)).ToList());
                    ReportDataSource rdc6 = new ReportDataSource("SahaweDaftarDataSet", _context.tbl_sahawe_daftar_karaya_aw_mashat.Where(u => u.A10FormId.Equals(id)).ToList());
                    ReportDataSource rdc7 = new ReportDataSource("MotarTarmimDataSet", _context.tbl_motar_tarmim_aw_normasayal.Where(u => u.A10FormId.Equals(id)).ToList());
                    ReportDataSource rdc8 = new ReportDataSource("MasrafeMotarafaDataSet", _context.tbl_masrafe_motafaraqa_mawad.Where(u => u.A10FormId.Equals(id)).ToList());
                    ReportDataSource rdc9 = new ReportDataSource("KarayaDataSet", _context.tbl_karaya.Where(u => u.A10FormId.Equals(id)).ToList());

                    ReportDataSource rdc10 = new ReportDataSource("ShawahedImagesDataSet", images);
                    ReportViewer1.LocalReport.DataSources.Add(rdc0);
                    ReportViewer1.LocalReport.DataSources.Add(rdc1);
                    ReportViewer1.LocalReport.DataSources.Add(rdc2);
                    ReportViewer1.LocalReport.DataSources.Add(rdc3);
                    ReportViewer1.LocalReport.DataSources.Add(rdc4);
                    ReportViewer1.LocalReport.DataSources.Add(rdc5);
                    ReportViewer1.LocalReport.DataSources.Add(rdc6);
                    ReportViewer1.LocalReport.DataSources.Add(rdc7);
                    ReportViewer1.LocalReport.DataSources.Add(rdc8);
                    ReportViewer1.LocalReport.DataSources.Add(rdc9);
                    ReportViewer1.LocalReport.DataSources.Add(rdc10);
                    
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