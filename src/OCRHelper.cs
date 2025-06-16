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

        //public bool validateIssueFolderNames()
        //{
        //    Regex issueFolderNamePattern = new Regex(@"^[a-zA-Z0-9_-]+_\d{4}-\d{2}-\d{2}$");

        //    List<string> issueFoldersPaths = new List<string>();
        //    List<string> files = new List<string>();

        //    issueFoldersPaths.AddRange(Directory.GetDirectories(mainForm.folderBrowserDialog.SelectedPath));
        //    files.AddRange(Directory.GetFiles(mainForm.folderBrowserDialog.SelectedPath));

        //    int validFolders = issueFoldersPaths.Count;

        //    if (files.Count > 0)
        //    {
        //        logForm.appendTextsToLog($"The following invalid files found in \"{mainForm.folderBrowserDialog.SelectedPath}\". Only issue folders are allowed.", logForm.LOG_TYPE_WARN);
        //        foreach (string file in files)
        //        {
        //            logForm.appendTextsToLog($"Invalid file: \"{file}\"", logForm.LOG_TYPE_WARN);
        //        }
        //        return false;
        //    }

        //    if (issueFoldersPaths.Count == 0)
        //    {
        //        logForm.appendTextsToLog($"No Issues Found in \"{mainForm.folderBrowserDialog.SelectedPath}\"", logForm.LOG_TYPE_WARN);
        //        return false;
        //    }
        //    else
        //    {
        //        foreach (string issueFolderPath in issueFoldersPaths)
        //        {
        //            string issueFolderName = Path.GetFileName(issueFolderPath);

        //            if (!issueFolderNamePattern.IsMatch(issueFolderName))
        //            {
        //                logForm.appendTextsToLog($"\"{issueFolderPath}\" is not a valid issue folder name", logForm.LOG_TYPE_WARN);
        //                validFolders--;
        //            }
        //            else
        //            {
        //                logForm.appendTextsToLog($"\"{issueFolderPath}\" is a valid issue folder name", logForm.LOG_TYPE_INFO);
        //            }
        //        }

        //        if (validFolders < issueFoldersPaths.Count)
        //        {
        //            logForm.appendTextsToLog($"Some issue folder names in \"{mainForm.folderBrowserDialog.SelectedPath}\" are invalid, please see log for details.", logForm.LOG_TYPE_WARN);
        //            return false;
        //        }
        //        else return true;
        //    }
        //}

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
        public void startOver()
        {
            //// Reset MainForm UI:
            //folderBrowserTextBox.Text = String.Empty;
            //folderBrowserDialog.SelectedPath = String.Empty;
            //loadImagesButton.Enabled = false;

            //sourceFilesListView.Items.Clear();
            //sourceFilesListView_filenameCol.Width = sourceFilesListView.Width - 150;

            //beginOCRButton.Enabled = false;

            //statusBarItem_numberOfImagesLoaded.Text = "No Image Files Loaded";
            //statusBarItem_numberOfCompletedItems.Text = "-";

            //resetStatusBar();

            //// Reset data structures :
            //directoryStructure.Clear();
        }

        public void resetStatusBar()
        {
            //statusBarItem_numberOfImagesLoaded.Text = "No Image Files Loaded";
            //statusBarItem_numberOfCompletedItems.Text = "-";
        }

        public void updateStatusBar(string status, string message)
        {
            //statusBarItem_numberOfImagesLoaded.Text = status;
            //statusBarItem_numberOfCompletedItems.Text = message;
        }
    }
}
