using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArianaRiseConstructionCompany.Models.Db;

namespace ArianaRiseConstructionCompany.Helpers
{
    public static class ConversionValidation
    {
        private static ArianaRiseCCEntities dbEntities = new ArianaRiseCCEntities();
        public static string ValidateA10FormConversions(this string value)
        {
            return string.IsNullOrEmpty(value) == true ? "0" : value;
        }
        public static string ValidateA10FormStringConversions(this string value)
        {
            return string.IsNullOrEmpty(value) == true ? "" : value;
        }
        public static string GetUserNameById(string userId)
        {
            return (userId != null) ? dbEntities.AspNetUsers.Where(u => u.Id.Equals(userId)).First().UserName : "";
        }
        public static string GetProjectNameByUserId(string userId)
        {
            string name ="";
            if (!string.IsNullOrEmpty(userId))
            {
                var user = dbEntities.AspNetUsers.Where(u => u.Id.Equals(userId)).First();

                if (user != null)
                {
                    if (user.AspNetRoles.FirstOrDefault().Name != "Admin")
                    {
                        name = dbEntities.tbl_projects.Find(user.ProjectId).Name;
                    }
                }
            }
            return name;
        }
        public static string GetUserProjectNameByProjectId(Guid userId)
        {
            return (userId != null) ? dbEntities.tbl_projects.FirstOrDefault(u => u.Id.Equals(userId)).Name : "";
        }
        public static double GetTotalById(Guid Id)
        {
            double sakhtamaneMawads = dbEntities.tbl_sakhtamane_mawad.Where(s => s.A10FormId.Equals(Id)).ToList().Sum(s => s.TotalPrice).Value;
            double aradajats = dbEntities.tbl_aradajat.Where(s => s.A10FormId.Equals(Id)).ToList().Sum(s => s.TotalPrice).Value;
            double karayas = dbEntities.tbl_karaya.Where(s => s.A10FormId.Equals(Id)).ToList().Sum(s => s.TotalPrice).Value;
            double masrafeMotafaraqaMawads = dbEntities.tbl_masrafe_motafaraqa_mawad.Where(s => s.A10FormId.Equals(Id)).ToList().Sum(s => s.TotalPrice).Value;
            double motarTarmimAwNormasayals = dbEntities.tbl_motar_tarmim_aw_normasayal.Where(s => s.A10FormId.Equals(Id)).ToList().Sum(s => s.TotalPrice).Value;
            double mashenareKarayas = dbEntities.tbl_mashenare_karaya.Where(s => s.A10FormId.Equals(Id)).ToList().Sum(s => s.TotalPrice).Value;
            double sahaweDaftarKarayaAwMashats = dbEntities.tbl_sahawe_daftar_karaya_aw_mashat.Where(s => s.A10FormId.Equals(Id)).ToList().Sum(s => s.TotalPrice).Value;
            double daKarDaParmakhtagJadwals = dbEntities.tbl_da_kar_da_parmakhtag_jadwal.Where(s => s.A10FormId.Equals(Id)).ToList().Sum(s => Convert.ToDouble(s.TotalHojam));
            double dananWrazePardakhtSawe = dbEntities.tbl_da_nan_wraze_karz.Where(s => s.A10FormId.Equals(Id)).ToList().Sum(s => Convert.ToDouble(s.Total));

            return dananWrazePardakhtSawe+sakhtamaneMawads + aradajats + karayas + masrafeMotafaraqaMawads + motarTarmimAwNormasayals + mashenareKarayas + sahaweDaftarKarayaAwMashats + daKarDaParmakhtagJadwals;
        }
        public static double GetProjectTotalById(Guid Id)
        {
            var user = dbEntities.AspNetUsers.ToList().FirstOrDefault(u => u.ProjectId.Equals(Id));
            if (user != null)
            {
                double sakhtamaneMawads = dbEntities.tbl_sakhtamane_mawad.Where(s => s.UserId.Equals(user.Id)).ToList().Sum(s => s.TotalPrice).Value;
                double aradajats = dbEntities.tbl_aradajat.Where(s => s.UserId.Equals(user.Id)).ToList().Sum(s => s.TotalPrice).Value;
                double karayas = dbEntities.tbl_karaya.Where(s => s.UserId.Equals(user.Id)).ToList().Sum(s => s.TotalPrice).Value;
                double masrafeMotafaraqaMawads = dbEntities.tbl_masrafe_motafaraqa_mawad.Where(s => s.UserId.Equals(user.Id)).ToList().Sum(s => s.TotalPrice).Value;
                double motarTarmimAwNormasayals = dbEntities.tbl_motar_tarmim_aw_normasayal.Where(s => s.UserId.Equals(user.Id)).ToList().Sum(s => s.TotalPrice).Value;
                double mashenareKarayas = dbEntities.tbl_mashenare_karaya.Where(s => s.UserId.Equals(user.Id)).ToList().Sum(s => s.TotalPrice).Value;
                double sahaweDaftarKarayaAwMashats = dbEntities.tbl_sahawe_daftar_karaya_aw_mashat.Where(s => s.UserId.Equals(user.Id)).ToList().Sum(s => s.TotalPrice).Value;
                double daKarDaParmakhtagJadwals = dbEntities.tbl_da_kar_da_parmakhtag_jadwal.Where(s => s.UserId.Equals(user.Id)).ToList().Sum(s => Convert.ToDouble(s.TotalHojam));
                double dananWrazePardakhtSawe = dbEntities.tbl_da_nan_wraze_karz.Where(s => s.UserId.Equals(user.Id)).ToList().Sum(s => Convert.ToDouble(s.Total));
                return dananWrazePardakhtSawe + sakhtamaneMawads + aradajats + karayas + masrafeMotafaraqaMawads + motarTarmimAwNormasayals + mashenareKarayas + sahaweDaftarKarayaAwMashats + daKarDaParmakhtagJadwals;
            }

            return 0;
        }
    }
}