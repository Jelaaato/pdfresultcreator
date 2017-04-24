using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using PDFResultCreator.Model;
using TableDependency.EventArgs;
using TableDependency.Enums;
using PDFResultCreator.BusinessLogic;

namespace PDFResultCreator
{
    class DatabaseListener
    {
        #region Declarations
            
            private static LabResultEntities lab = new LabResultEntities();
            private static LabHeaderEntities labheader = new LabHeaderEntities();

        #endregion

        #region Methods

            static void dependency_OnChanged(object sender, RecordChangedEventArgs<patient_lab_header> e)
            {
                if (e.ChangeType == ChangeType.Insert)
                {
                    var changedEntity = e.Entity;
                    var name = changedEntity.patient_name;
                    var hn = changedEntity.hospital_number;
                    var dob = changedEntity.date_of_birth;
                    var age = changedEntity.age;
                    var sex = changedEntity.sex;
                    var rm = changedEntity.patient_area;
                    var doctor = changedEntity.requesting_physician;
                    var order = changedEntity.order_group;
                    var order_date = changedEntity.order_date_time; 
                    var serv_cat = changedEntity.service_category;
                    var lab_order = changedEntity.lab_orderable_name;
                    var doc_nr = changedEntity.doc_emp_nr;
                    var med_tech = changedEntity.med_tech;
                    var chief_patho = changedEntity.chief_pathologist;
                    
                    Console.WriteLine("Hospital Number: " + hn);
                    Console.WriteLine("Order Group: " + order);
                    CreatePDF.LabResult(name, hn, dob, age, sex, rm, doctor, order, order_date, serv_cat, lab_order, doc_nr, med_tech, chief_patho);
                }
            }

            static void dependency_OnError(object sender, ErrorEventArgs e)
            {
                Console.WriteLine(e.Error.Message);
            }

        #endregion



        static void Main(string[] args)
        {
            using (var dependency = new SqlTableDependency<patient_lab_header>(lab.Database.Connection.ConnectionString, "patient_lab_header"))
            {
                try
                {
                    dependency.OnChanged += dependency_OnChanged;
                    dependency.OnError += dependency_OnError;
                    dependency.Start();

                    Console.WriteLine("Listener has started ...");

                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
