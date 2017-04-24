using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using PDFResultCreator.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFResultCreator.BusinessLogic
{
    class CreatePDF
    {
        #region Global Declarations

        private static LabResultEntities labres = new LabResultEntities();
        private static LabHeaderEntities labheader = new LabHeaderEntities();

        private static Chunk hr = new Chunk(new LineSeparator());
        //private static Document doc = new Document();
        private static MemoryStream mst = new MemoryStream();

        #endregion

        #region Methods
        #endregion

        public static void LabResult(string name, string HN, DateTime dob, int? age, string sex, string rm, string doctor, string order, DateTime order_date, string serv_cat, string lab_order, string doc_nr, string med_tech, string chief_pathologist)
        {
            #region Declarations

                List<labResultDTO> labResult;
                Document doc = new Document(PageSize.LETTER);
                int[] headertblwidth = { 20, 30 };
                int[] labresultswidth = { 15, 10, 10, 10 };
                var headerFont = FontFactory.GetFont("Arial", 9);
                var valueFont = FontFactory.GetFont("Arial", 11);
                var labheaderFont = FontFactory.GetFont("Arial", 10, Font.BOLD);

                string path = @"C:\inetpub\wwwroot\AHMCPortal\PDFResults\";
                Image logo = Image.GetInstance(Directory.GetCurrentDirectory() + "/Images/ahmc_logo.png"); 

            #endregion

            #region Queries

                do
                {
                    labResult = (from p in labres.patient_lab_results
                                 where p.hospital_number == HN && p.order_group == order
                                 select new labResultDTO
                                 {
                                     test = p.test,
                                     result = p.result,
                                     UOM = p.UOM,
                                     reference_range = p.reference_range,
                                     seq_num = p.seq_num
                                 }).OrderBy(p => p.seq_num).ToList();
                } while (labResult.Count() == 0);
                

            #endregion

                FileStream fst = new FileStream(path + HN + "_" + name + "_" + serv_cat + "_" + order + "_" +  doc_nr + "_" + order_date.ToString("yyyy-MM-dd") + ".pdf", FileMode.Create);
                using (fst)
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, fst);
                    //writer.PageEvent = new PdfFooter();
                    writer.CloseStream = false;

                    doc.Open();

                    PdfPTable headerImg = new PdfPTable(1);
                    headerImg.WidthPercentage = 30;
                    headerImg.DefaultCell.Border = Rectangle.NO_BORDER;
                    headerImg.AddCell(logo);

                    PdfPTable demoTbl = new PdfPTable(4);
                    demoTbl.WidthPercentage = 100;
                    demoTbl.DefaultCell.Border = Rectangle.NO_BORDER;
                    demoTbl.AddCell(new Phrase("Name:", valueFont));
                    demoTbl.AddCell(new Phrase(name, valueFont));
                    demoTbl.AddCell(new Phrase("HN:", valueFont));
                    demoTbl.AddCell(new Phrase(HN, valueFont));
                    demoTbl.AddCell(new Phrase("Date of Birth:", valueFont));
                    demoTbl.AddCell(new Phrase(dob.ToString("yyyy-MM-dd"), valueFont));
                    demoTbl.AddCell(new Phrase("Age:", valueFont));
                    demoTbl.AddCell(new Phrase(age.ToString(), valueFont));
                    demoTbl.AddCell(new Phrase("Sex:", valueFont));
                    demoTbl.AddCell(new Phrase(sex, valueFont));
                    demoTbl.AddCell(new Phrase("Room:", valueFont));
                    demoTbl.AddCell(new Phrase(rm, valueFont));
                    demoTbl.AddCell(new Phrase("Physician:", valueFont));
                    demoTbl.AddCell(new Phrase(doctor, valueFont));
                    demoTbl.AddCell(new Phrase("Order Group:", valueFont));
                    demoTbl.AddCell(new Phrase(order, valueFont));
                    demoTbl.AddCell(new Phrase("Order Date:", valueFont));
                    demoTbl.AddCell(new Phrase(order_date.ToString(), valueFont));
                    demoTbl.AddCell(new Phrase(" "));
                    demoTbl.AddCell(new Phrase(" "));

                    //PdfPCell demoCell = new PdfPCell(demoTbl);

                    //PdfPTable headerImages = new PdfPTable(2);
                    //headerImages.DefaultCell.Padding = 8;
                    //headerImages.DefaultCell.Border = Rectangle.NO_BORDER;
                    //headerImages.AddCell(logo);
                    //headerImages.AddCell(new Phrase("*Insert Image here."));

                    //PdfPCell headerimgCell = new PdfPCell(headerImages);

                    //PdfPTable headerTbl = new PdfPTable(2);
                    //headerTbl.DefaultCell.Border = Rectangle.NO_BORDER;
                    //headerTbl.SetWidths(headertblwidth);
                    //headerTbl.WidthPercentage = 100;
                    //headerTbl.AddCell(headerimgCell);
                    //headerTbl.AddCell(demoCell);

                    PdfPTable labHeaderTbl = new PdfPTable(4);
                    labHeaderTbl.WidthPercentage = 100;
                    labHeaderTbl.DefaultCell.Border = Rectangle.NO_BORDER;
                    labHeaderTbl.SetWidths(labresultswidth);
                    labHeaderTbl.AddCell(new Phrase("Test", labheaderFont));
                    labHeaderTbl.AddCell(new Phrase("Result", labheaderFont));
                    labHeaderTbl.AddCell(new Phrase("Unit", labheaderFont));
                    labHeaderTbl.AddCell(new Phrase("Reference Range", labheaderFont));

                    PdfPTable labResultsTbl = new PdfPTable(4);
                    labResultsTbl.WidthPercentage = 100;
                    labResultsTbl.DefaultCell.Border = Rectangle.NO_BORDER;
                    labResultsTbl.SetWidths(labresultswidth);

                   
                    foreach (var item in labResult)
                    {
                        labResultsTbl.AddCell(new Phrase(item.test, headerFont));
                        labResultsTbl.AddCell(new Phrase(item.result, headerFont));
                        labResultsTbl.AddCell(new Phrase(item.UOM, headerFont));
                        labResultsTbl.AddCell(new Phrase(item.reference_range, headerFont));
                    }


                    doc.Add(headerImg);
                    doc.Add(new Paragraph("\n"));
                    doc.Add(demoTbl);
                    doc.Add(hr);
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Phrase(serv_cat + " " + "|" + " " + lab_order, valueFont));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(labHeaderTbl);
                    doc.Add(hr);
                    doc.Add(labResultsTbl);

                    doc.Close();
                    Console.WriteLine("PDF Successfully Generated!");
                }
        }

    }
}
