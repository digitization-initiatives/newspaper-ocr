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
        public OptionsForm()
        {
            InitializeComponent();
            CustomInitializations();
        }

        #region Custom Functions
        private void printSettingsToLogs()
        {
            logForm.appendTextsToLog($"[Tessdata Location] has been changed to: {Properties.Settings.Default.TessdataLocation}.", logForm.LOG_TYPE_INFO);
            logForm.appendTextsToLog($"[OCR Output Location] has been changed to: {Properties.Settings.Default.OCROutputLocation}.", logForm.LOG_TYPE_INFO);
            logForm.appendTextsToLog($"[Log Location] has been changed to: {Properties.Settings.Default.LogLocation}.", logForm.LOG_TYPE_INFO);
            logForm.appendTextsToLog($"[Concurrent OCR Jobs] has been changed to: {Properties.Settings.Default.ConcurrentOCRJobs.ToString()}.", logForm.LOG_TYPE_INFO);
            logForm.appendTextsToLog($"[OCR Language] has been changed to: {Properties.Settings.Default.OCRLang}.", logForm.LOG_TYPE_INFO);
            logForm.appendTextsToLog($"[Tile Size] has been changed to: {Properties.Settings.Default.TileSize}.", logForm.LOG_TYPE_INFO);
        }
        private void updateOptionsFormUI()
        {
            // Update Directory Settings UI:
            tessdataLocationTextBox.Text = Properties.Settings.Default.TessdataLocation;
            ocrOutputLocationTextBox.Text = Properties.Settings.Default.OCROutputLocation;
            logLocationTextBox.Text = Properties.Settings.Default.LogLocation;

            // Update OCR Settings UI:
            ocrLangComboBox.SelectedItem = Properties.Settings.Default.OCRLang;
            concurrentOCRJobsComboBox.SelectedItem = Properties.Settings.Default.ConcurrentOCRJobs.ToString();
            tileSizeComboBox.SelectedItem = Properties.Settings.Default.TileSize;
        }

        public void setDefaultOptions()
        {
            // Update Settings :
            Properties.Settings.Default.TessdataLocation = Path.GetFullPath(".") + "\\tessdata";
            Properties.Settings.Default.OCROutputLocation = Path.GetFullPath(".") + "\\output";
            Properties.Settings.Default.LogLocation = Path.GetFullPath(".") + "\\log";
            
            Properties.Settings.Default.ConcurrentOCRJobs = 1;
            Properties.Settings.Default.OCRLang = "eng";
            Properties.Settings.Default.TileSize = "[1024x1024]";

            Properties.Settings.Default.Save();

            // Clear folderBrowserDialog :
            tessdataLocation_folderBrowserDialog.SelectedPath = String.Empty;
            ocrOutputLocation_folderBrowserDialog.SelectedPath = String.Empty;
            logLocation_folderBrowserDialog.SelectedPath = String.Empty;

            // Update OptionsForm UI :
            updateOptionsFormUI();

            // Print logs :
            printSettingsToLogs();
        }

        private void saveChanges()
        {
            // Update Settings :
            Properties.Settings.Default.TessdataLocation = tessdataLocationTextBox.Text;
            Properties.Settings.Default.OCROutputLocation = ocrOutputLocationTextBox.Text;
            Properties.Settings.Default.LogLocation = logLocationTextBox.Text;

            Properties.Settings.Default.ConcurrentOCRJobs = concurrentOCRJobsComboBox.SelectedIndex + 1;
            Properties.Settings.Default.OCRLang = ocrLangComboBox.SelectedItem.ToString().Substring(0, 3);
            Properties.Settings.Default.TileSize = tileSizeComboBox.SelectedItem.ToString();
            
            Properties.Settings.Default.Save();

            // Update OptionsForm UI :
            updateOptionsFormUI();

            // Print logs :
            printSettingsToLogs();
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

        private void logLocationBrowseButton_Click(object sender, EventArgs e)
        {
            if (logLocation_folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                logLocationTextBox.Text = logLocation_folderBrowserDialog.SelectedPath;
            }
            else
            {
                logLocationTextBox.Text = Properties.Settings.Default.LogLocation;
            }
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            saveChanges();
        }

        private void resetToDefaultButton_Click(object sender, EventArgs e)
        {
            setDefaultOptions();
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            saveChanges();
            this.Hide();
        }
    }
}
