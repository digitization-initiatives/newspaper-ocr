using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperOCR.src
{
    public class OCRJob
    {
        public int ListViewItemIndex { get; set; }
        public string SourceImageFileFullPath { get; set; }
        public string OutputJp2ImageFileName { get; set; }
        public string OutputJp2ImageFileFullPath { get; set; }
        public string OutputPdfFileName { get; set; }
        public string OutputPdfFileFullPath { get; set; }
        public string OutputAltoFileName { get; set; }
        public string OutputAltoFileFullPath { get; set; }
        public string OutputDirectoryFullPath { get; set; }

        public int OcrJobIndex { get; set; }
        public Task OcrTask { get; set; }
        public CancellationTokenSource CTS { get; set; }
        public bool IsCompleted => OcrTask?.IsCompleted ?? false;
        public bool IsCanceled => CTS?.IsCancellationRequested ?? false;

    }
}
