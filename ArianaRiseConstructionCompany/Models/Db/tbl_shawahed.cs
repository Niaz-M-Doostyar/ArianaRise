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
    
    public partial class tbl_shawahed
    {
        public System.Guid Id { get; set; }
        public string Url { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public System.Guid A10FormId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public string OfficerName { get; set; }
        public string FormNumber { get; set; }
        public string Day { get; set; }
        public Nullable<System.Guid> ProjectId { get; set; }
    }
}
