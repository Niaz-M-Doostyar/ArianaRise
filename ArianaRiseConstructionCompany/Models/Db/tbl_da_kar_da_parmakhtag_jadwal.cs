//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArianaRiseConstructionCompany.Models.Db
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_da_kar_da_parmakhtag_jadwal
    {
        public System.Guid Id { get; set; }
        public string SakhtamaneFaliyatona { get; set; }
        public string Unit { get; set; }
        public Nullable<int> MakhkeneKarHojam { get; set; }
        public Nullable<int> DaNanWrazeDaKarHojam { get; set; }
        public string TotalHojam { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public System.Guid A10FormId { get; set; }
        public string UserId { get; set; }
    }
}
