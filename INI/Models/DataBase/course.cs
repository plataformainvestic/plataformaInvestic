//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace INI.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class course
    {
        public int id { get; set; }
        public string code { get; set; }
        public string directory { get; set; }
        public string db_name { get; set; }
        public string course_language { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string category_code { get; set; }
        public Nullable<sbyte> visibility { get; set; }
        public int show_score { get; set; }
        public string tutor_name { get; set; }
        public string visual_code { get; set; }
        public string department_name { get; set; }
        public string department_url { get; set; }
        public Nullable<decimal> disk_quota { get; set; }
        public Nullable<System.DateTime> last_visit { get; set; }
        public Nullable<System.DateTime> last_edit { get; set; }
        public Nullable<System.DateTime> creation_date { get; set; }
        public Nullable<System.DateTime> expiration_date { get; set; }
        public string target_course_code { get; set; }
        public sbyte subscribe { get; set; }
        public sbyte unsubscribe { get; set; }
        public string registration_code { get; set; }
        public string legal { get; set; }
        public int activate_legal { get; set; }
        public Nullable<long> course_type_id { get; set; }
    }
}
