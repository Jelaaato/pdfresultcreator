using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFResultCreator.Model
{
    class labResultDTO
    {
        public string test { get; set; }
        public string result { get; set; }
        public string UOM { get; set; }
        public string reference_range { get; set; }
        public int? seq_num { get; set; }
    }
}
