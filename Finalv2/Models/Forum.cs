//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Finalv2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Forum
    {
        public decimal Post_ID { get; set; }
        public string Post_Content { get; set; }
        public System.DateTime Post_date { get; set; }
        public int Course_ID { get; set; }
        public int User_ID { get; set; }
    
        public virtual Cours Cours { get; set; }
        public virtual Users_Stud Users_Stud { get; set; }
    }
}
