using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperOCR.src
{
    public class OCRJob
    {
        public OCRItem OcrItem { get; set; }
        public int OcrJobIndex { get; set; }
        public Task<int> OcrTask { get; set; }
        public CancellationTokenSource CTS { get; set; }
        public bool IsCompleted => OcrTask?.IsCompleted ?? false;
        public bool IsCanceled => CTS?.IsCancellationRequested ?? false;

        public OCRJob(OCRItem _ocrItem, int _ocrJobIndex, Task<int> _ocrTask, CancellationTokenSource _cts)
        {
            OcrItem = _ocrItem;
            OcrJobIndex = _ocrJobIndex;
            OcrTask = _ocrTask;
            CTS = _cts;
        }
    }
}
