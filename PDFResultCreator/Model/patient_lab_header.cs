//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PDFResultCreator.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class patient_lab_header
    {
        public System.Guid patient_id { get; set; }
        public string hospital_number { get; set; }
        public System.Guid lab_worker_order_id { get; set; }
        public string patient_name { get; set; }
        public System.DateTime date_of_birth { get; set; }
        public Nullable<int> age { get; set; }
        public string sex { get; set; }
        public string requesting_physician { get; set; }
        public string ordering_area { get; set; }
        public string patient_area { get; set; }
        public string service_category { get; set; }
        public string lab_orderable_name { get; set; }
        public string order_group { get; set; }
        public System.DateTime order_date_time { get; set; }
        public string doc_emp_nr { get; set; }
        public string med_tech { get; set; }
        public string chief_pathologist { get; set; }
    }
}
