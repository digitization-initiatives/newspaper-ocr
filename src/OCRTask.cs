using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperOCR.src
{
    public class OCRTask
    {
        public OCRItem OcrItem { get; set; }
        public Task OcrTask { get; set; }
        public Stopwatch OcrStopwatch { get; set; }
    }
}
