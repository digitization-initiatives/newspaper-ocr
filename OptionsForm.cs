using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewspaperOCR
{
    public partial class OptionsForm : Form
    {
        public OptionsForm(LogForm logFormRef)
        {
            logForm = logFormRef;
            InitializeComponent();
            CustomInitializations();
        }

        #region Custom Functions

        //Override default ToolTip behavior:
        private void tessdataLocationTextBox_MouseHover(object sender, EventArgs e)
        {
            string toolTipText = tessdataLocationTextBox.Text;

            Point cursorPosition = tessdataLocationTextBox.PointToClient(Cursor.Position);
            tessdataLocationTextBoxToolTip.Show(toolTipText, tessdataLocationTextBox, cursorPosition.X, cursorPosition.Y + 10, int.MaxValue);
        }
        private void tessdataLocationTextBox_MouseLeave(object sender, EventArgs e)
        {
            tessdataLocationTextBoxToolTip.Hide(tessdataLocationTextBox);
        }
        private void ocrOutputLocationTextBox_MouseHover(object sender, EventArgs e)
        {
            string toolTipText = ocrOutputLocationTextBox.Text;

            Point cursorPosition = ocrOutputLocationTextBox.PointToClient(Cursor.Position);
            ocrOutputLocationTextBoxToolTip.Show(toolTipText, ocrOutputLocationTextBox, cursorPosition.X, cursorPosition.Y + 10, int.MaxValue);
        }
        private void ocrOutputLocationTextBox_MouseLeave(object sender, EventArgs e)
        {
            ocrOutputLocationTextBoxToolTip.Hide(ocrOutputLocationTextBox);
        }

        private void printSettingsToLogs()
        {
            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"[Tessdata Location] has been changed to: {Properties.Settings.Default.TessdataLocation}");
            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"[OCR Output Location] has been changed to: {Properties.Settings.Default.OCROutputLocation}");
            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"[Log Location] has been changed to: {Properties.Settings.Default.LogLocation}");
            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"[Concurrent OCR Jobs] has been changed to: {Properties.Settings.Default.ConcurrentOCRJobs.ToString()}.");
            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"[OCR Language] has been changed to: {Properties.Settings.Default.OCRLang}");
            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"[Tile Size] has been changed to: {Properties.Settings.Default.TileSize}");
            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"[Source Image File Format] has been changed to: {Properties.Settings.Default.SourceImageFileFormat}");
            logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"[Source Image File Format] has been changed to: {Properties.Settings.Default.IssueFolderNameValidationRegex}");
        }
        private void updateOptionsFormUI()
        {
            // Update Directory Settings UI:
            tessdataLocationTextBox.Text = Properties.Settings.Default.TessdataLocation;
            ocrOutputLocationTextBox.Text = Properties.Settings.Default.OCROutputLocation;

            // Update OCR Settings UI:
            concurrentOCRJobsComboBox.SelectedItem = Properties.Settings.Default.ConcurrentOCRJobs.ToString();
            ocrLangComboBox.SelectedItem = Properties.Settings.Default.OCRLang;
            tileSizeComboBox.SelectedItem = Properties.Settings.Default.TileSize;
            sourceImageFileFormatComboBox.SelectedItem = Properties.Settings.Default.SourceImageFileFormat;
            issueFolderNameValidationRegexTextBox.Text = Properties.Settings.Default.IssueFolderNameValidationRegex;
        }

        public void setDefaultOptions()
        {
            // Update Settings :
            Properties.Settings.Default.TessdataLocation = Path.GetFullPath(".") + "\\src\\tessdata";
            Properties.Settings.Default.OCROutputLocation = Path.GetFullPath(".") + "\\output";

            Properties.Settings.Default.ConcurrentOCRJobs = 5;
            Properties.Settings.Default.OCRLang = "eng";
            Properties.Settings.Default.TileSize = "[1024x1024]";
            Properties.Settings.Default.SourceImageFileFormat = "tif";
            Properties.Settings.Default.IssueFolderNameValidationRegex = @"^[a-zA-Z0-9]+_\d{4}-\d{2}-\d{2}$";

            Properties.Settings.Default.Save();

            // Clear folderBrowserDialog :
            tessdataLocation_folderBrowserDialog.SelectedPath = String.Empty;
            ocrOutputLocation_folderBrowserDialog.SelectedPath = String.Empty;
            logLocation_folderBrowserDialog.SelectedPath = String.Empty;

            // Update OptionsForm UI :
            updateOptionsFormUI();
        }

        private void saveChanges()
        {
            // Update Settings :
            Properties.Settings.Default.TessdataLocation = tessdataLocationTextBox.Text;
            Properties.Settings.Default.OCROutputLocation = ocrOutputLocationTextBox.Text;

            Properties.Settings.Default.ConcurrentOCRJobs = concurrentOCRJobsComboBox.SelectedIndex + 1;
            Properties.Settings.Default.OCRLang = ocrLangComboBox.SelectedItem.ToString().Substring(0, 3);
            Properties.Settings.Default.TileSize = tileSizeComboBox.SelectedItem.ToString();
            Properties.Settings.Default.SourceImageFileFormat = sourceImageFileFormatComboBox.SelectedItem.ToString();
            Properties.Settings.Default.IssueFolderNameValidationRegex = issueFolderNameValidationRegexTextBox.Text;

            Properties.Settings.Default.Save();

            // Update OptionsForm UI :
            updateOptionsFormUI();
        }

        #endregion

        private void tessdataLocationBrowseButton_Click(object sender, EventArgs e)
        {
            if (tessdataLocation_folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tessdataLocationTextBox.Text = tessdataLocation_folderBrowserDialog.SelectedPath;
            }
            else
            {
                tessdataLocationTextBox.Text = Properties.Settings.Default.TessdataLocation;
            }
        }

        private void ocrOutputLocationBrowseButton_Click(object sender, EventArgs e)
        {
            if (ocrOutputLocation_folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ocrOutputLocationTextBox.Text = ocrOutputLocation_folderBrowserDialog.SelectedPath;
            }
            else
            {
                ocrOutputLocationTextBox.Text = Properties.Settings.Default.OCROutputLocation;
            }
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            saveChanges();
            printSettingsToLogs();
        }

        private void resetToDefaultButton_Click(object sender, EventArgs e)
        {
            setDefaultOptions();
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            saveChanges();
            printSettingsToLogs();
            this.Hide();
        }

        private void jp2CompressionLevelTrackbar_Scroll(object sender, EventArgs e)
        {
            jp2CompressionLevelValue.Text = jp2CompressionLevelTrackbar.Value.ToString();
        }
    }
}
