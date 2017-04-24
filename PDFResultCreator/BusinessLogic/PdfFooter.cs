using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
namespace PDFResultCreator.BusinessLogic
{
    class PdfFooter : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document doc)
        {
 	         base.OnEndPage(writer, doc);
             doc.Add(new Phrase("**This report has been electronically signed**"));
        }
    }
}
