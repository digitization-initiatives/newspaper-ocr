using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        public string getTimestamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public void sendToLog(string logType, string logMessage)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => sendToLog(logType, logMessage)));
                return;
            }

            // Handle log monitoring UI:
            if (!pauseLogMonitoringCheckbox.Checked)
            {
                int rowIndex = logEntryDataGridView.Rows.Add(getTimestamp(), logType, logMessage);
                logEntryDataGridView.FirstDisplayedScrollingRowIndex = logEntryDataGridView.Rows.Count - 1;
            }

            if (logEntryDataGridView.Rows.Count > MAX_LOG_ROWS)
            {
                logEntryDataGridView.Rows.RemoveAt(0);
            }

            // Handle writing to log file:
            string messageEntry = $"{getTimestamp()} - {logType} - {logMessage}" + Environment.NewLine;

            File.AppendAllText(logFileFullPath, messageEntry);
        }

        private void logFormHideLogsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.viewLogsButton.Text = "View Logs";
        }

        private void clearLogsButton_Click(object sender, EventArgs e)
        {
            logEntryDataGridView.Rows.Clear();
            logEntryDataGridView.Refresh();
        }

        private void viewLogFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"notepad.exe", logFileFullPath);
            }
            catch (Exception err)
            {
                sendToLog(LogForm.LogType[LogForm.ERROR], err.Message);
            }
        }

        private void LogForm_Click(object sender, EventArgs e)
        {
            logEntryDataGridView.ClearSelection();
        }
    }
}

