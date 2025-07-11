using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing.Interop;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesseractOCR.Enums;
using TesseractOCR.Renderers;

namespace NewspaperOCR.src
{
    public class OCR
    {
        private MainForm mainForm;
        private LogForm logForm;
        private OptionsForm optionsForm;
        public List<OCRItem> ocrItemsList;
        public Queue<OCRItem> ocrItemsQueue;

        public int totalNumberOfImages = 0;
        public int completedOcrJobs = 0;

        public List<OCRJob> concurrentOcrJobs;

        public OCR(MainForm _mainForm, LogForm _logForm, OptionsForm _optionsForm)
        {
            mainForm = _mainForm;
            logForm = _logForm;
            optionsForm = _optionsForm;

            ocrItemsList = new List<OCRItem>();
        }

        public bool ValidateIssueFolderNames(string folderBrowserDialogSelectedPath)
        {
            Regex issueFolderNamePattern = new Regex(Properties.Settings.Default.IssueFolderNameValidationRegex);

            List<string> issueFoldersPaths = new List<string>();
            List<string> files = new List<string>();

            issueFoldersPaths.AddRange(Directory.GetDirectories(folderBrowserDialogSelectedPath));
            files.AddRange(Directory.GetFiles(folderBrowserDialogSelectedPath));

            int validFolders = issueFoldersPaths.Count;

            // Ensure there are no individual files in the folder besides issue folders.
            if (files.Count > 0)
            {
                logForm.sendToLog(LogForm.LogType[LogForm.ERROR], $"The following invalid files found in \"{folderBrowserDialogSelectedPath}\". Only issue folders are allowed.");
                foreach (string file in files)
                {
                    logForm.sendToLog(LogForm.LogType[LogForm.ERROR], $"Invalid file: \"{file}\"");
                }
                return false;
            }

            // Validate issue folders:
            if (issueFoldersPaths.Count == 0)
            {
                logForm.sendToLog(LogForm.LogType[LogForm.WARN], $"No Issues Found in \"{folderBrowserDialogSelectedPath}\"");
                return false;
            }
            else
            {
                foreach (string issueFolderPath in issueFoldersPaths)
                {
                    string issueFolderName = Path.GetFileName(issueFolderPath);

                    if (!issueFolderNamePattern.IsMatch(issueFolderName))
                    {
                        logForm.sendToLog(LogForm.LogType[LogForm.ERROR], $"\"{issueFolderPath}\" is not a valid issue folder name");
                        validFolders--;
                    }
                    else
                    {
                        logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"\"{issueFolderPath}\" is a valid issue folder name");
                    }
                }

                if (validFolders < issueFoldersPaths.Count)
                {
                    logForm.sendToLog(LogForm.LogType[LogForm.ERROR], $"Some issue folder names in \"{folderBrowserDialogSelectedPath}\" are invalid, please see log for details.");
                    return false;
                }
                else return true;
            }
        }

        public void CreateOutputDirectories()
        {
            string batchFolderFullPath = mainForm.folderBrowserTextBox.Text;
            string batchNameFolder = Path.GetFileName(batchFolderFullPath);
            string outputDirectory = Properties.Settings.Default.OCROutputLocation;

            foreach (ListViewItem imageFileListViewItem in mainForm.sourceFilesListView.Items)
            {
                int index = imageFileListViewItem.Index;
                string sourceImageFileFullPath = imageFileListViewItem.SubItems[0].Text;

                //Construct issueDateFolder:
                string issueDateFolder = sourceImageFileFullPath.Replace(batchFolderFullPath + "\\", "");
                string[] segments = issueDateFolder.Split(Path.DirectorySeparatorChar);
                if (segments.Length > 0)
                {
                    issueDateFolder = segments[0];
                }

                //Extract imageFileName:
                string sourceImageFileName = Path.GetFileName(sourceImageFileFullPath);
                
                OCRItem ocrItem = new OCRItem(index, batchNameFolder, issueDateFolder, sourceImageFileName, sourceImageFileFullPath, outputDirectory);
                ocrItemsList.Add(ocrItem);
                
                logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"Output directory \"{ocrItem.OutputDirectoryFullPath}\" has been created.");
            }

            ocrItemsQueue = new Queue<OCRItem>(ocrItemsList);

            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"Batch and issue folders in \"{outputDirectory}\" have been created.");
        }

        public async Task<int> Ocr(string sourceImageFileFullpath, string sourceImageFileName, string outputPdfFileFullPath, string outputAltoFileFullPath, string outputJp2FileFullPath, string tessdataLoc, Language ocrLang, string tileSize, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                //Perform OCR and produce PDF and Alto files:
                using (var engine = new TesseractOCR.Engine(tessdataLoc, Language.English, EngineMode.LstmOnly))
                {
                    using (var img = TesseractOCR.Pix.Image.LoadFromFile(sourceImageFileFullpath))
                    {
                        using (var page = engine.Process(img))
                        {
                            using (var pdfRenderer = new PdfResult(outputPdfFileFullPath, tessdataLoc, false))
                            {
                                pdfRenderer.BeginDocument(sourceImageFileName);
                                pdfRenderer.AddPage(page);
                            }

                            using (var altoRenderer = new AltoResult(outputAltoFileFullPath))
                            {
                                altoRenderer.BeginDocument(sourceImageFileName);
                                altoRenderer.AddPage(page);
                            }
                        }
                    }
                }

                //Perform image compression and generate JP2 file:
                using (var sourceImage = new MagickImage(sourceImageFileFullpath))
                {
                    sourceImage.Format = MagickFormat.Jp2;
                    sourceImage.Settings.Compression = CompressionMethod.JPEG2000;
                    sourceImage.Settings.SetDefine(MagickFormat.Jp2, "progression-order", "RLCP");
                    sourceImage.Quality = Properties.Settings.Default.Jp2CompressionLevel;

                    string tiledJp2FileFullPath = outputJp2FileFullPath + tileSize;

                    sourceImage.Write(tiledJp2FileFullPath);

                    if (File.Exists(outputJp2FileFullPath))
                    {
                        File.Delete(outputJp2FileFullPath);
                    }

                    File.Move(tiledJp2FileFullPath, outputJp2FileFullPath);
                }

                return 0;
            }
            catch (OperationCanceledException)
            {
                return 1;
            }
            catch (Exception ex)
            {
                return 2;
            }
        }

        public async Task TestOcrWorkflow()
        {
            OCRItem ocrOutputInfoItem;

            while (completedOcrJobs != totalNumberOfImages)
            {
                ocrOutputInfoItem = ocrItemsQueue.Dequeue();
                logForm.sendToLog(LogForm.LogType[LogForm.DEBUG], $"Item index No.{ocrOutputInfoItem.Index} dequeued: {ocrOutputInfoItem.SourceImageFileFullPath}, and is being processed.");
                await Task.Delay(2000);

                mainForm.sourceFilesListView.Items[ocrOutputInfoItem.Index].SubItems[1].Text = "Completed";
                mainForm.statusBarItem_numberOfCompletedItems.Text = $"{completedOcrJobs}";
                
                completedOcrJobs++;
            }

            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"All {completedOcrJobs} images have been processed.");
        }

        public int TestOcr(int timeDelay)
        {
            foreach (OCRItem ocrItem in ocrItemsList)
            {
                logForm.sendToLog(LogForm.LogType[LogForm.DEBUG], $"Item index No.{ocrItem.Index} dequeued: {ocrItem.SourceImageFileFullPath}, and is being processed.");
            }

            return 0;
        }

        public Language GetOcrLanguage()
        {
            switch (Properties.Settings.Default.OCRLang)
            {
                case "eng":
                    return Language.English;
                case "spa":
                    return Language.SpanishCastilian;
                case "fra":
                    return Language.French;
                case "jpn":
                    return Language.Japanese;
                default:
                    return Language.English;
            }
        }


        public void UpdateStatusBar(string status, string message)
        {
            //statusBarItem_numberOfImagesLoaded.Text = status;
            //statusBarItem_numberOfCompletedItems.Text = message;
        }
    }
}
