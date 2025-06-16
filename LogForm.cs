using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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

        public void addLogEntryToUI(string logType, string logMessage)
        {
            string timestamp;

            if (InvokeRequired)
            {
                Invoke(new Action(() => addLogEntryToUI(logType, logMessage)));
                return;
            }

            timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            int rowIndex = logEntryDataGridView.Rows.Add(timestamp, logType, logMessage);

            if (logEntryDataGridView.Rows.Count > MAX_LOG_ROWS)
            {
                logEntryDataGridView.Rows.RemoveAt(0);
            }

            if (!pauseAutoScrollCheckbox.Checked)
            {
                logEntryDataGridView.FirstDisplayedScrollingRowIndex = logEntryDataGridView.Rows.Count - 1;
            }
        }

        private void logFormSaveLogsButton_Click(object sender, EventArgs e)
        {
            string logFileName = $"ocr_{DateTime.Now:yyyyMMdd_HHmmss}.log";
            string logFileFullPath = Path.Combine(Properties.Settings.Default.LogLocation, logFileName);

            if (!Directory.Exists(Properties.Settings.Default.LogLocation))
            {
                Directory.CreateDirectory(Properties.Settings.Default.LogLocation);
            }

            //File.WriteAllText(logFileFullPath, debugTextbox.Text);

            MessageBox.Show($"Log file saved to {logFileFullPath} .", "Logs Saved!");
        }

        private void logFormHideLogsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.viewLogsButton.Text = "View Logs";
        }

        private void clearLogsButton_Click(object sender, EventArgs e)
        {
            //debugTextbox.Clear();
        }

        private void viewLogFileButton_Click(object sender, EventArgs e)
        {
            addLogEntryToUI(LogForm.LogType[0], $"This is a test log message.");
        }
    }
}

