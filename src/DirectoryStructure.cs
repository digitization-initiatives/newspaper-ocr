using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperOCR.src
{
    internal class DirectoryStructure
    {
        public string BatchNameFolder { get; set; }
        public string IssueDateFolder { get; set; }
        public string SourceImageFileName { get; set; }
        public string SourceImageFileNameWithoutExtension { get; set; }
        public string SourceImageFileFullPath { get; set; }
        public string OutputDirectory { get; set; }
        public string OutputJp2ImageFileName { get; set; }
        public string OutputJp2ImageFileFullPath { get; set; }
        public string OutputJpgFileName { get; set; }
        public string OutputJpgImageFileFullPath { get; set; }
        public string OutputTextOnlyPdfFileName { get; set; }
        public string OutputTextOnlyPdfFileFullPath { get; set; }
        public string OutputPdfFileName { get; set; }
        public string OutputPdfFileFullPath { get; set; }
        public string OutputAltoFileName { get; set; }
        public string OutputAltoFileFullPath { get; set; }
        public string OutputDirectoryFullPath { get; set; }

        public DirectoryStructure(string batchNameFolder, string issueDateFolder, string sourceImageFileName, string sourceImageFileFullpath, string outputDirectory)
        {
            BatchNameFolder = batchNameFolder;
            IssueDateFolder = issueDateFolder;
            SourceImageFileName = sourceImageFileName;
            SourceImageFileFullPath = sourceImageFileFullpath;
            SourceImageFileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceImageFileName);

            OutputDirectory = outputDirectory;
            OutputJp2ImageFileName = SourceImageFileNameWithoutExtension + ".jp2";
            OutputJp2ImageFileFullPath = Path.Combine(outputDirectory, batchNameFolder, issueDateFolder, OutputJp2ImageFileName);
            OutputJpgFileName = SourceImageFileNameWithoutExtension + ".jpg";
            OutputJpgImageFileFullPath = Path.Combine(outputDirectory, batchNameFolder, issueDateFolder, OutputJpgFileName);
            OutputTextOnlyPdfFileName = SourceImageFileNameWithoutExtension;
            OutputPdfFileFullPath = Path.Combine(outputDirectory, batchNameFolder, issueDateFolder, OutputTextOnlyPdfFileName);
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
