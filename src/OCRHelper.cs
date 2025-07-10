using System;
using System.Collections.Generic;
using System.Drawing.Interop;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesseractOCR.Enums;

namespace NewspaperOCR.src
{
    public class OCRHelper
    {
        private MainForm mainForm;
        private LogForm logForm;
        private OptionsForm optionsForm;
        internal List<OCROutputInfo> ocrOutputInfoList;
        internal Queue<OCROutputInfo> ocrOutputInfoQueue;

        internal int totalNumberOfImages = 0;
        internal int completedOcrJobs = 0;

        public OCRHelper(MainForm _mainForm, LogForm _logForm, OptionsForm _optionsForm)
        {
            mainForm = _mainForm;
            logForm = _logForm;
            optionsForm = _optionsForm;

            ocrOutputInfoList = new List<OCROutputInfo>();
        }

        public bool validateIssueFolderNames(string folderBrowserDialogSelectedPath)
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

        public void createOutputDirectories()
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
                
                OCROutputInfo directoryStructureItem = new OCROutputInfo(index, batchNameFolder, issueDateFolder, sourceImageFileName, sourceImageFileFullPath, outputDirectory);
                ocrOutputInfoList.Add(directoryStructureItem);
                
                logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"Output directory \"{directoryStructureItem.OutputDirectoryFullPath}\" has been created.");
            }

            ocrOutputInfoQueue = new Queue<OCROutputInfo>(ocrOutputInfoList);

            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"Batch and issue folders in \"{outputDirectory}\" have been created.");
        }

        public async Task testOcrWorkflow()
        {
            OCROutputInfo ocrOutputInfoItem;

            while (completedOcrJobs != totalNumberOfImages)
            {
                ocrOutputInfoItem = ocrOutputInfoQueue.Dequeue();
                logForm.sendToLog(LogForm.LogType[LogForm.DEBUG], $"Item index No.{ocrOutputInfoItem.Index} dequeued: {ocrOutputInfoItem.SourceImageFileFullPath}, and is being processed.");
                await Task.Delay(2000);

                mainForm.sourceFilesListView.Items[ocrOutputInfoItem.Index].SubItems[1].Text = "Completed";
                mainForm.statusBarItem_numberOfCompletedItems.Text = $"{completedOcrJobs}";
                
                completedOcrJobs++;
            }

            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"All {completedOcrJobs} images have been processed.");
        }

        public Language getOcrLanguage()
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


        public void updateStatusBar(string status, string message)
        {
            //statusBarItem_numberOfImagesLoaded.Text = status;
            //statusBarItem_numberOfCompletedItems.Text = message;
        }
    }
}
