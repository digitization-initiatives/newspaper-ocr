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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using System.Diagnostics;

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

        public List<OCRTask> ocrTasks = new List<OCRTask>();

        public OCR(MainForm _mainForm, LogForm _logForm, OptionsForm _optionsForm)
        {
            mainForm = _mainForm;
            logForm = _logForm;
            optionsForm = _optionsForm;

            ocrItemsList = new List<OCRItem>();
        }

        public bool IsFileLocked(string filePath)
        {
            try
            {
                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    //File is accessible.
                }
                return false;
            }
            catch (IOException)
            {
                logForm.sendToLog(LogForm.LogType[LogForm.WARN], $"File: {filePath} is locked or in use.");
                return true;
            }
        }
        public async Task WaitForFileRelease(string filePath, int maxRetries, int delay)
        {
            int retry = 0;

            while (IsFileLocked(filePath) && retry < maxRetries)
            {
                await Task.Delay(delay);
                retry++;
            }

            if (!IsFileLocked(filePath))
            {
                logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"File: {filePath} is accessible.");
            } else
            {
                logForm.sendToLog(LogForm.LogType[LogForm.ERROR], $"File: {filePath} is still locked or in use after {maxRetries} retries.");
            }
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
                
                logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"Output Issue Directory \"{ocrItem.OutputIssueDirectoryFullPath}\" has been created.");
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
                string tiledJp2FileFullPath = outputJp2FileFullPath + tileSize;

                using (var sourceImage = new MagickImage(sourceImageFileFullpath))
                {
                    sourceImage.Format = MagickFormat.Jp2;
                    sourceImage.Settings.Compression = CompressionMethod.JPEG2000;
                    sourceImage.Settings.SetDefine(MagickFormat.Jp2, "progression-order", "RLCP");
                    sourceImage.Quality = Properties.Settings.Default.Jp2CompressionLevel;
                    sourceImage.Write(tiledJp2FileFullPath);
                }

                if (File.Exists(outputJp2FileFullPath))
                {
                    File.Delete(outputJp2FileFullPath);
                }

                if (File.Exists(tiledJp2FileFullPath))
                {
                    await WaitForFileRelease(tiledJp2FileFullPath, 10, 1000);
                    File.Move(tiledJp2FileFullPath, outputJp2FileFullPath);
                    logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"\"{tiledJp2FileFullPath}\" renamed to \"{outputJp2FileFullPath}\".");
                }
                else if (File.Exists(outputJp2FileFullPath) && !File.Exists(tiledJp2FileFullPath))
                {
                    logForm.sendToLog(LogForm.LogType[LogForm.ERROR], $"Tiled JP2 file \"{tiledJp2FileFullPath}\" already renamed to \"{outputJp2FileFullPath}\".");
                }
            }
            catch (Exception ex)
            {
                logForm.sendToLog(LogForm.LogType[LogForm.ERROR], $"Error trying to perform OCR on file \"{sourceImageFileFullpath}\" with message: {ex.ToString()}");
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
                while ((ocrTasks.Count < concurrentOCRJobs) && (ocrItemsQueue.Count > 0))
                {
                    ocrItem = ocrItemsQueue.Dequeue();
                    OCRTask ocrTask = new OCRTask();

                    ocrTask.OcrItem = ocrItem;
                    ocrTask.OcrStopwatch = new Stopwatch();
                    ocrTask.OcrStopwatch.Start();
                    ocrTask.OcrTask = Task.Run(() => Ocr(ocrItem.SourceImageFileFullPath, ocrItem.SourceImageFileName, ocrItem.OutputPdfFileFullPath, ocrItem.OutputAltoFileFullPath, ocrItem.OutputJp2ImageFileFullPath, tessdataLoc, ocrLang, tileSize));

                    ocrTasks.Add(ocrTask);

                    logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"{ocrItem.SourceImageFileFullPath} has been added to the OCR job queue.");
                }

                if (ocrTasks.Count > 0)
                {
                    foreach (OCRTask runningTask in ocrTasks.ToList())
                    {
                        ListViewItem sourceImageFileListViewItem = mainForm.sourceFilesListView.Items[runningTask.OcrItem.Index];

                        if (runningTask.OcrTask.Status == TaskStatus.RanToCompletion)
                        {
                            runningTask.OcrStopwatch.Stop();
                            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"{runningTask.OcrItem.SourceImageFileFullPath} has completed OCR.");
                            sourceImageFileListViewItem.SubItems[1].Text = "Finished";
                            sourceImageFileListViewItem.SubItems[2].Text = $"{runningTask.OcrStopwatch.Elapsed:hh\\:mm\\:ss}";
                            completedOcrJobs++;
                            mainForm.statusBarItem_numberOfCompletedItems.Text = completedOcrJobs.ToString();

                            ocrTasks.Remove(runningTask);
                        }
                        else if (runningTask.OcrTask.Status == TaskStatus.Faulted)
                        {
                            runningTask.OcrStopwatch.Stop();
                            logForm.sendToLog(LogForm.LogType[LogForm.ERROR], $"{runningTask.OcrItem.SourceImageFileFullPath} has faulted");
                            sourceImageFileListViewItem.SubItems[1].Text = "Faulted";
                            sourceImageFileListViewItem.SubItems[2].Text = $"{runningTask.OcrStopwatch.Elapsed:hh\\:mm\\:ss}";
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
                            sourceImageFileListViewItem.SubItems[2].Text = $"{runningTask.OcrStopwatch.Elapsed:hh\\:mm\\:ss}";
                        }

                        await Task.Delay(2000);
                    }
                }

                //logForm.sendToLog(LogForm.LogType[LogForm.DEBUG], $"Completed Jobs: {completedOcrJobs}, Total Jobs: {totalNumberOfImages}.");
                //logForm.sendToLog(LogForm.LogType[LogForm.DEBUG], $"ocrTasks.Count: {ocrTasks.Count}, ocrItemsQueue.Count: {ocrItemsQueue.Count}.");
            }

            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"All {completedOcrJobs} images have been processed.");
        }

        public void ValidateOutputFiles()
        {
            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"Begin validating output files ... ");

            int filesValidated = 0;
            int totalFiles = ocrItemsList.Count * 3;

            foreach (OCRItem ocrItem in ocrItemsList)
            {
                string pdfFileFullPath = ocrItem.OutputPdfFileFullPath + ".pdf";
                string altoFileFUllPath = ocrItem.OutputAltoFileFullPath + ".xml";

                if (!File.Exists(pdfFileFullPath))
                {
                    logForm.sendToLog(LogForm.LogType[LogForm.WARN], $"{pdfFileFullPath} is missing, consider re-running the OCR job.");
                }
                else
                {
                    filesValidated++;
                }

                if (!File.Exists(altoFileFUllPath))
                {
                    logForm.sendToLog(LogForm.LogType[LogForm.WARN], $"{altoFileFUllPath} is missing, consider re-running the OCR job.");
                }
                else
                {
                    filesValidated++;
                }

                if (!File.Exists(ocrItem.OutputJp2ImageFileFullPath))
                {
                    logForm.sendToLog(LogForm.LogType[LogForm.WARN], $"{ocrItem.OutputJp2ImageFileFullPath} is missing, consider re-running the OCR job.");
                }
                else
                {
                    filesValidated++;
                }
            }

            if (filesValidated == totalFiles)
            {
                logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"Output files validation completed, all {filesValidated} files OCR'd successfully.");
            }
            else
            {
                logForm.sendToLog(LogForm.LogType[LogForm.WARN], $"Output files validation completed, {filesValidated} of {totalFiles} OCR'd successfully. Consider re-running OCR for the failed/missing ones.");
            }
        }


        public async Task CancelQueue()
        {
            while (ocrItemsQueue.Count > 0)
            {
                OCRItem ocrItem;

                ocrItem = ocrItemsQueue.Dequeue();
                ListViewItem sourceImageFileListViewItem = mainForm.sourceFilesListView.Items[ocrItem.Index];
                sourceImageFileListViewItem.SubItems[1].Text = "Cancelled";
                completedOcrJobs++;

                logForm.sendToLog(LogForm.LogType[LogForm.WARN], $"{ocrItem.SourceImageFileFullPath} cancelled.");
            }
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
