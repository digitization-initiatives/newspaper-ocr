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
        public bool queueCancelled = false;

        public List<OCRTask> ocrTasks = new List<OCRTask>();

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

            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"Batch and issue folders in \"{outputDirectory}\" have been created.");
        }

        public async Task Ocr(string sourceImageFileFullpath, string sourceImageFileName, string outputPdfFileFullPath, string outputAltoFileFullPath, string outputJp2FileFullPath, string tessdataLoc, Language ocrLang, string tileSize)
        {
            try
            {
                //Perform OCR and produce PDF and Alto files:
                using (var engine = new TesseractOCR.Engine(tessdataLoc, Language.English, EngineMode.LstmOnly))
                {
                    using (var img = TesseractOCR.Pix.Image.LoadFromFile(sourceImageFileFullpath))
                    {
                        using (var page = engine.Process(img))
                        {
                            if (File.Exists(outputPdfFileFullPath))
                            {
                                File.Delete(outputPdfFileFullPath);
                            }

                            using (var pdfRenderer = new PdfResult(outputPdfFileFullPath, tessdataLoc, false))
                            {
                                pdfRenderer.BeginDocument(sourceImageFileName);
                                pdfRenderer.AddPage(page);
                            }

                            if (File.Exists(outputAltoFileFullPath))
                            {
                                File.Delete(outputAltoFileFullPath);
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
            }
            catch (Exception ex)
            {
                logForm.sendToLog(LogForm.LogType[LogForm.ERROR], $"Error trying to perform OCR on file \"{sourceImageFileFullpath}\" with message: {ex.Message}");
            }
        }

        public async Task ProcessOCRQueue()
        {
            Language ocrLang = GetOcrLanguage();
            string tessdataLoc = Properties.Settings.Default.TessdataLocation;
            string tileSize = Properties.Settings.Default.TileSize;
            int concurrentOCRJobs = Properties.Settings.Default.ConcurrentOCRJobs;

            //Set up OCR items queue:
            ocrItemsQueue = new Queue<OCRItem>(ocrItemsList);

            //Process OCR queue:
            OCRItem ocrItem;

            while (completedOcrJobs < totalNumberOfImages)
            {
                while ((ocrTasks.Count < concurrentOCRJobs) && (!queueCancelled) && (ocrItemsQueue.Count > 0))
                {
                    ocrItem = ocrItemsQueue.Dequeue();
                    OCRTask ocrTask = new OCRTask();

                    ocrTask.OcrItem = ocrItem;
                    ocrTask.OcrTask = Task.Run(() => Ocr(ocrItem.SourceImageFileFullPath, ocrItem.SourceImageFileName, ocrItem.OutputPdfFileFullPath, ocrItem.OutputAltoFileFullPath, ocrItem.OutputJp2ImageFileFullPath, tessdataLoc, ocrLang, tileSize));

                    ocrTasks.Add(ocrTask);

                    logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"{ocrItem.SourceImageFileFullPath} has been added to the OCR job queue.");
                }

                if (queueCancelled)
                {
                    while (ocrItemsQueue.Count > 0)
                    {
                        ocrItem = ocrItemsQueue.Dequeue();
                        ListViewItem sourceImageFileListViewItem = mainForm.sourceFilesListView.Items[ocrItem.Index];
                        sourceImageFileListViewItem.SubItems[1].Text = "Cancelled";
                        completedOcrJobs++;

                        logForm.sendToLog(LogForm.LogType[LogForm.WARN], $"{ocrItem.SourceImageFileFullPath} cancelled.");
                    }
                }

                if (ocrTasks.Count > 0)
                {
                    foreach (OCRTask runningTask in ocrTasks.ToList())
                    {
                        ListViewItem sourceImageFileListViewItem = mainForm.sourceFilesListView.Items[runningTask.OcrItem.Index];

                        if (runningTask.OcrTask.Status == TaskStatus.RanToCompletion)
                        {
                            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"{runningTask.OcrItem.SourceImageFileFullPath} has completed OCR.");
                            sourceImageFileListViewItem.SubItems[1].Text = "Finished";
                            completedOcrJobs++;
                            mainForm.statusBarItem_numberOfCompletedItems.Text = completedOcrJobs.ToString();

                            ocrTasks.Remove(runningTask);
                        }
                        else if (runningTask.OcrTask.Status == TaskStatus.Faulted)
                        {
                            logForm.sendToLog(LogForm.LogType[LogForm.ERROR], $"{runningTask.OcrItem.SourceImageFileFullPath} has faulted");
                            sourceImageFileListViewItem.SubItems[1].Text = "Faulted";
                            completedOcrJobs++;

                            ocrTasks.Remove(runningTask);
                        }
                        else if (!runningTask.OcrTask.IsCompleted)
                        {
                            if (sourceImageFileListViewItem.SubItems[1].Text.Length < 8)
                            {
                                sourceImageFileListViewItem.SubItems[1].Text += "..";
                            }
                            else
                            {
                                sourceImageFileListViewItem.SubItems[1].Text = "...";
                            }

                            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"{runningTask.OcrItem.SourceImageFileFullPath} is being ocr'd ...");
                            sourceImageFileListViewItem.SubItems[1].Text = sourceImageFileListViewItem.SubItems[1].Text;

                            await Task.Delay(2000);
                        }
                    }
                }

                logForm.sendToLog(LogForm.LogType[LogForm.DEBUG], $"Completed Jobs: {completedOcrJobs}, Total Jobs: {totalNumberOfImages}.");
                logForm.sendToLog(LogForm.LogType[LogForm.DEBUG], $"ocrTasks.Count: {ocrTasks.Count}, ocrItemsQueue.Count: {ocrItemsQueue.Count}.");
            }

            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"All {completedOcrJobs} images have been processed.");
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
    }
}
