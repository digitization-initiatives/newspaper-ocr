using System;
using System.Collections.Generic;
using System.Drawing.Interop;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewspaperOCR.src
{
    public class OCRHelperUtilities
    {
        private MainForm mainForm;
        private LogForm logForm;
        private OptionsForm optionsForm;

        public OCRHelperUtilities(MainForm _mainForm, LogForm _logForm, OptionsForm _optionsForm)
        {
            mainForm = _mainForm;
            logForm = _logForm;
            optionsForm = _optionsForm;
        }

        public bool validateIssueFolderNames()
        {
            Regex issueFolderNamePattern = new Regex(@"^[a-zA-Z0-9_-]+_\d{4}-\d{2}-\d{2}$");

            List<string> issueFoldersPaths = new List<string>();
            List<string> files = new List<string>();

            issueFoldersPaths.AddRange(Directory.GetDirectories(mainForm.folderBrowserDialog.SelectedPath));
            files.AddRange(Directory.GetFiles(mainForm.folderBrowserDialog.SelectedPath));

            int validFolders = issueFoldersPaths.Count;

            if (files.Count > 0)
            {
                logForm.appendTextsToLog($"The following invalid files found in \"{mainForm.folderBrowserDialog.SelectedPath}\". Only issue folders are allowed.", logForm.LOG_TYPE_WARN);
                foreach (string file in files)
                {
                    logForm.appendTextsToLog($"Invalid file: \"{file}\"", logForm.LOG_TYPE_WARN);
                }
                return false;
            }

            if (issueFoldersPaths.Count == 0)
            {
                logForm.appendTextsToLog($"No Issues Found in \"{mainForm.folderBrowserDialog.SelectedPath}\"", logForm.LOG_TYPE_WARN);
                return false;
            }
            else
            {
                foreach (string issueFolderPath in issueFoldersPaths)
                {
                    string issueFolderName = Path.GetFileName(issueFolderPath);

                    if (!issueFolderNamePattern.IsMatch(issueFolderName))
                    {
                        logForm.appendTextsToLog($"\"{issueFolderPath}\" is not a valid issue folder name", logForm.LOG_TYPE_WARN);
                        validFolders--;
                    }
                    else
                    {
                        logForm.appendTextsToLog($"\"{issueFolderPath}\" is a valid issue folder name", logForm.LOG_TYPE_INFO);
                    }
                }

                if (validFolders < issueFoldersPaths.Count)
                {
                    logForm.appendTextsToLog($"Some issue folder names in \"{mainForm.folderBrowserDialog.SelectedPath}\" are invalid, please see log for details.", logForm.LOG_TYPE_WARN);
                    return false;
                }
                else return true;
            }
        }
    }
}
