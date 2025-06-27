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
        internal List<OutputDirectoryStructure> outputDirectoryStructure;

        public OCRHelper(MainForm _mainForm, LogForm _logForm, OptionsForm _optionsForm)
        {
            mainForm = _mainForm;
            logForm = _logForm;
            optionsForm = _optionsForm;

            outputDirectoryStructure = new List<OutputDirectoryStructure>();
        }

        public bool validateIssueFolderNames(string folderBrowserDialogSelectedPath)
        {
            Regex issueFolderNamePattern = new Regex(@"^[a-zA-Z0-9]+_\d{4}-\d{2}-\d{2}_\d{2}$");

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

        public void constructOutputDirectoryStructure()
        {
            string batchNameFolder = Path.GetFileName(mainForm.folderBrowserTextBox.Text);

            foreach (ListViewItem imageFileListViewItem in mainForm.sourceFilesListView.Items)
            {
                //Get the item index:
                int index = imageFileListViewItem.Index;

                //Construct issueDateFolder:
                string issueDateFolder = imageFileListViewItem.SubItems[0].Text;
                issueDateFolder = issueDateFolder.Replace(mainForm.folderBrowserTextBox.Text, "");
                var segments = issueDateFolder.Split(Path.DirectorySeparatorChar);
                if (segments.Length > 0)
                {
                    issueDateFolder = segments[1];
                }

                //Extract imageFileName:
                string imageFileName = Path.GetFileName(imageFileListViewItem.SubItems[0].Text);

                OutputDirectoryStructure directoryStructureItem = new OutputDirectoryStructure(index, batchNameFolder, issueDateFolder, imageFileName, imageFileListViewItem.SubItems[0].Text, Properties.Settings.Default.OCROutputLocation);
                //mainForm.outputDirectoryStructure.Add(directoryStructureItem);
            }
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
