using System.Diagnostics;

namespace NewspaperOCR
{
    partial class LogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            hideButton = new Button();
            clearButton = new Button();
            pauseLogMonitoringCheckbox = new CheckBox();
            logEntryDataGridView = new DataGridView();
            logTimestampCol = new DataGridViewTextBoxColumn();
            logTypeCol = new DataGridViewTextBoxColumn();
            logMessageCol = new DataGridViewTextBoxColumn();
            ViewFullLogsButton = new Button();
            statusStrip1 = new StatusStrip();
            maxLogEntryStatusStripLabel = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)logEntryDataGridView).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // hideButton
            // 
            hideButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            hideButton.Location = new Point(874, 614);
            hideButton.Name = "hideButton";
            hideButton.Size = new Size(120, 29);
            hideButton.TabIndex = 3;
            hideButton.Text = "Hide";
            hideButton.UseVisualStyleBackColor = true;
            hideButton.Click += logFormHideLogsButton_Click;
            // 
            // clearButton
            // 
            clearButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            clearButton.Location = new Point(748, 614);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(120, 29);
            clearButton.TabIndex = 4;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearLogsButton_Click;
            // 
            // pauseLogMonitoringCheckbox
            // 
            pauseLogMonitoringCheckbox.AutoSize = true;
            pauseLogMonitoringCheckbox.Location = new Point(12, 617);
            pauseLogMonitoringCheckbox.Name = "pauseLogMonitoringCheckbox";
            pauseLogMonitoringCheckbox.Size = new Size(175, 24);
            pauseLogMonitoringCheckbox.TabIndex = 5;
            pauseLogMonitoringCheckbox.Text = "Pause Log Monitoring";
            pauseLogMonitoringCheckbox.UseVisualStyleBackColor = true;
            // 
            // logEntryDataGridView
            // 
            logEntryDataGridView.AllowUserToAddRows = false;
            logEntryDataGridView.AllowUserToDeleteRows = false;
            logEntryDataGridView.AllowUserToResizeColumns = false;
            logEntryDataGridView.AllowUserToResizeRows = false;
            logEntryDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            logEntryDataGridView.BackgroundColor = Color.White;
            logEntryDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            logEntryDataGridView.Columns.AddRange(new DataGridViewColumn[] { logTimestampCol, logTypeCol, logMessageCol });
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            logEntryDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            logEntryDataGridView.Location = new Point(12, 12);
            logEntryDataGridView.Name = "logEntryDataGridView";
            logEntryDataGridView.ReadOnly = true;
            logEntryDataGridView.RowHeadersVisible = false;
            logEntryDataGridView.RowHeadersWidth = 51;
            logEntryDataGridView.ScrollBars = ScrollBars.Vertical;
            logEntryDataGridView.Size = new Size(982, 596);
            logEntryDataGridView.TabIndex = 6;
            // 
            // logTimestampCol
            // 
            logTimestampCol.HeaderText = "Timestamp";
            logTimestampCol.MinimumWidth = 6;
            logTimestampCol.Name = "logTimestampCol";
            logTimestampCol.ReadOnly = true;
            logTimestampCol.Width = 125;
            // 
            // logTypeCol
            // 
            logTypeCol.HeaderText = "Type";
            logTypeCol.MinimumWidth = 6;
            logTypeCol.Name = "logTypeCol";
            logTypeCol.ReadOnly = true;
            logTypeCol.Width = 125;
            // 
            // logMessageCol
            // 
            logMessageCol.HeaderText = "Log Message";
            logMessageCol.MinimumWidth = 6;
            logMessageCol.Name = "logMessageCol";
            logMessageCol.ReadOnly = true;
            logMessageCol.Width = 125;
            // 
            // ViewFullLogsButton
            // 
            ViewFullLogsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ViewFullLogsButton.Location = new Point(582, 614);
            ViewFullLogsButton.Name = "ViewFullLogsButton";
            ViewFullLogsButton.Size = new Size(160, 29);
            ViewFullLogsButton.TabIndex = 7;
            ViewFullLogsButton.Text = "View Full Logs";
            ViewFullLogsButton.UseVisualStyleBackColor = true;
            ViewFullLogsButton.Click += viewLogFileButton_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { maxLogEntryStatusStripLabel });
            statusStrip1.Location = new Point(0, 647);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1006, 26);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // maxLogEntryStatusStripLabel
            // 
            maxLogEntryStatusStripLabel.Name = "maxLogEntryStatusStripLabel";
            maxLogEntryStatusStripLabel.Size = new Size(294, 20);
            maxLogEntryStatusStripLabel.Text = "Only the most recent log entries are shown.";
            // 
            // LogForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1006, 673);
            ControlBox = false;
            Controls.Add(statusStrip1);
            Controls.Add(ViewFullLogsButton);
            Controls.Add(logEntryDataGridView);
            Controls.Add(pauseLogMonitoringCheckbox);
            Controls.Add(clearButton);
            Controls.Add(hideButton);
            MaximumSize = new Size(1024, 720);
            MinimumSize = new Size(1024, 720);
            Name = "LogForm";
            Text = "View Logs";
            ((System.ComponentModel.ISupportInitialize)logEntryDataGridView).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        #region Custom Initializations

        //Override default OnShown behavior:
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.ActiveControl = null;
        }

        internal MainForm mainForm;

        internal static readonly int MAX_LOG_ROWS = 500;
        internal static readonly int INFO = 0, WARN = 1, ERROR = 2, DEBUG = 3;
        internal static readonly Dictionary<int, string> LogType = new Dictionary<int, string>
        {
            { INFO, "[INFO]" },
            { WARN, "[WARN]" },
            { ERROR, "[ERROR]"},
            { DEBUG, "[DEBUG]"}
        };

        internal string logFileName = String.Empty;
        internal string logFileFullPath = String.Empty;

        private void CustomInitializations()
        {
            // Set log monitoring window column size:
            logTimestampCol.Width = 150;
            logTypeCol.Width = 60;
            logMessageCol.Width = logEntryDataGridView.Width - logTimestampCol.Width - logTypeCol.Width - 3;

            // Define cell text alignment:
            logTimestampCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            logTypeCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            logMessageCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            
            maxLogEntryStatusStripLabel.Text = $"Only the most recent {MAX_LOG_ROWS} log entries are shown.";

            // Initialize log file:
            Properties.Settings.Default.LogLocation = Path.GetFullPath(".") + "\\logs";

            logFileName = $"ocr_{DateTime.Now:yyyyMMdd_HHmmss}.log";
            logFileFullPath = Path.Combine(Properties.Settings.Default.LogLocation, logFileName);

            if (!Directory.Exists(Properties.Settings.Default.LogLocation))
            {
                Directory.CreateDirectory(Properties.Settings.Default.LogLocation);
            }

            File.WriteAllText(logFileFullPath, String.Empty);
        }

        #endregion

        private Button hideButton;
        private Button clearButton;
        private CheckBox pauseLogMonitoringCheckbox;
        private DataGridView logEntryDataGridView;
        private DataGridViewTextBoxColumn logTimestampCol;
        private DataGridViewTextBoxColumn logTypeCol;
        private DataGridViewTextBoxColumn logMessageCol;
        private Button ViewFullLogsButton;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel maxLogEntryStatusStripLabel;
    }
}