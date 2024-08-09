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
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
            CustomInitializations();
        }

        public void appendTextsToLog(string logText, string logType)
        {
            LOG_TIMESTAMP = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            logsTextBox.AppendText(LOG_TIMESTAMP + logType + logText);
            logsTextBox.AppendText(Environment.NewLine);
            logsTextBox.ScrollToCaret();
        }

        private void logFormSaveLogsButton_Click(object sender, EventArgs e)
        {
            string logFileName = $"ocr_{DateTime.Now:yyyyMMdd_HHmmss}.log";
            string logFileFullPath = Path.Combine(Properties.Settings.Default.LogLocation, logFileName);

            if (!Directory.Exists(Properties.Settings.Default.LogLocation))
            {
                Directory.CreateDirectory(Properties.Settings.Default.LogLocation);
            }

            File.WriteAllText(logFileFullPath, logsTextBox.Text);

            MessageBox.Show($"Log file saved to {logFileFullPath} .", "Logs Saved!");
        }

        private void logFormHideLogsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.viewLogsButton.Text = "View Logs";
        }

        private void clearLogsButton_Click(object sender, EventArgs e)
        {
            logsTextBox.Clear();
        }
    }
}
