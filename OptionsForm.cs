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
        public string getOcrOutputLocation()
        {
            return ocrOutputLocationTextBox.Text;
        }

        #endregion


        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void resetToDefaultButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.TessdataLocation = Path.GetFullPath(".") + "\\tessdata";
            Properties.Settings.Default.OCROutputLocation = Path.GetFullPath(".") + "\\output";
            Properties.Settings.Default.ConcurrentOCRJobs = 1;
            Properties.Settings.Default.OCRLang = "eng";
            Properties.Settings.Default.Save();

            tessdataLocationTextBox.Text = Properties.Settings.Default.TessdataLocation;
            ocrOutputLocationTextBox.Text = Properties.Settings.Default.OCROutputLocation;
            concurrentOCRJobsComboBox.SelectedIndex = 0;
            ocrLangComboBox.SelectedIndex = 0;

            logForm.appendTextsToLog("\"Tessdata Location\" has been reset to: " + Properties.Settings.Default.TessdataLocation, logForm.LOG_TYPE_INFO);
            logForm.appendTextsToLog("\"OCR Output Location\" has been reset to: " + Properties.Settings.Default.OCROutputLocation, logForm.LOG_TYPE_INFO);
            logForm.appendTextsToLog("\"Concurrent OCR Jobs\" has been reset to: " + Properties.Settings.Default.ConcurrentOCRJobs.ToString(), logForm.LOG_TYPE_INFO);
            logForm.appendTextsToLog("\"OCR Language\" has been reset to: " + Properties.Settings.Default.OCRLang, logForm.LOG_TYPE_INFO);

        }

        private void tessdataLocationBrowseButton_Click(object sender, EventArgs e)
        {
            if (tessdataLocation_folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tessdataLocationTextBox.Text = tessdataLocation_folderBrowserDialog.SelectedPath;
            }
        }

        private void ocrOutputLocationBrowseButton_Click(object sender, EventArgs e)
        {
            if (ocrOutputLocation_folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ocrOutputLocationTextBox.Text = ocrOutputLocation_folderBrowserDialog.SelectedPath;
            }
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.TessdataLocation = tessdataLocationTextBox.Text;
            Properties.Settings.Default.OCROutputLocation = ocrOutputLocationTextBox.Text;
            Properties.Settings.Default.ConcurrentOCRJobs = concurrentOCRJobsComboBox.SelectedIndex + 1;
            Properties.Settings.Default.OCRLang = ocrLangComboBox.SelectedItem.ToString().Substring(0,3);
            Properties.Settings.Default.Save();

            logForm.appendTextsToLog("\"Tessdata Location\" has been changed to: " + Properties.Settings.Default.TessdataLocation, logForm.LOG_TYPE_INFO);
            logForm.appendTextsToLog("\"OCR Output Location\" has been changed to: " + Properties.Settings.Default.OCROutputLocation, logForm.LOG_TYPE_INFO);
            logForm.appendTextsToLog("\"Concurrent OCR Jobs\" has been changed to: " + Properties.Settings.Default.ConcurrentOCRJobs.ToString(), logForm.LOG_TYPE_INFO);
            logForm.appendTextsToLog("\"OCR Language\" has been changed to: " + Properties.Settings.Default.OCRLang, logForm.LOG_TYPE_INFO);
        }

    }
}
