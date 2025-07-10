using System;
using System.Collections.Generic;
using System.Drawing.Interop;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperOCR.src
{
    internal class OutputDirectoryStructure
    {
        public int Index { get; set; }
        public string BatchNameFolder { get; set; }
        public string IssueDateFolder { get; set; }
        public string SourceImageFileFullPath { get; set; }
        public string SourceImageFileName { get; set; }
        public string SourceImageFileNameWithoutExtension { get; set; }
        public string OutputDirectory { get; set; }
        public string OutputJp2ImageFileName { get; set; }
        public string OutputJp2ImageFileFullPath { get; set; }
        public string OutputPdfFileName { get; set; }
        public string OutputPdfFileFullPath { get; set; }
        public string OutputAltoFileName { get; set; }
        public string OutputAltoFileFullPath { get; set; }
        public string OutputDirectoryFullPath { get; set; }

        public OutputDirectoryStructure(int index, string batchNameFolder, string issueDateFolder, string sourceImageFileName, string sourceImageFileFullpath, string outputDirectory)
        {
            Index = index;

            BatchNameFolder = batchNameFolder;
            IssueDateFolder = issueDateFolder;
            SourceImageFileFullPath = sourceImageFileFullpath;
            SourceImageFileName = sourceImageFileName;
            SourceImageFileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceImageFileName);

            OutputDirectory = outputDirectory;
            OutputJp2ImageFileName = SourceImageFileNameWithoutExtension + ".jp2";
            OutputJp2ImageFileFullPath = Path.Combine(outputDirectory, batchNameFolder, issueDateFolder, OutputJp2ImageFileName);
            OutputPdfFileName = SourceImageFileNameWithoutExtension;
            OutputPdfFileFullPath = Path.Combine(outputDirectory, batchNameFolder, issueDateFolder, OutputPdfFileName);
            OutputAltoFileName = SourceImageFileNameWithoutExtension;
            OutputAltoFileFullPath = Path.Combine(OutputDirectory, batchNameFolder, issueDateFolder, OutputAltoFileName);

            OutputDirectoryFullPath = Path.Combine(OutputDirectory, batchNameFolder, issueDateFolder);
            if (!Directory.Exists(OutputDirectoryFullPath))
            {
                Directory.CreateDirectory(OutputDirectoryFullPath);
            }
        }
    }
}
