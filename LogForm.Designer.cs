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
            hideButton = new Button();
            clearButton = new Button();
            pauseLogsCheckbox = new CheckBox();
            logEntryDataGridView = new DataGridView();
            logTimestampCol = new DataGridViewTextBoxColumn();
            logTypeCol = new DataGridViewTextBoxColumn();
            logTextCol = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)logEntryDataGridView).BeginInit();
            SuspendLayout();
            // 
            // hideButton
            // 
            hideButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            hideButton.Location = new Point(874, 632);
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
            clearButton.Location = new Point(748, 632);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(120, 29);
            clearButton.TabIndex = 4;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearLogsButton_Click;
            // 
            // pauseLogsCheckbox
            // 
            pauseLogsCheckbox.AutoSize = true;
            pauseLogsCheckbox.Location = new Point(12, 635);
            pauseLogsCheckbox.Name = "pauseLogsCheckbox";
            pauseLogsCheckbox.Size = new Size(147, 24);
            pauseLogsCheckbox.TabIndex = 5;
            pauseLogsCheckbox.Text = "Pause Auto-Scroll";
            pauseLogsCheckbox.UseVisualStyleBackColor = true;
            // 
            // logEntryDataGridView
            // 
            logEntryDataGridView.AllowUserToAddRows = false;
            logEntryDataGridView.AllowUserToDeleteRows = false;
            logEntryDataGridView.AllowUserToResizeColumns = false;
            logEntryDataGridView.AllowUserToResizeRows = false;
            logEntryDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logEntryDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            logEntryDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            logEntryDataGridView.Columns.AddRange(new DataGridViewColumn[] { logTimestampCol, logTypeCol, logTextCol });
            logEntryDataGridView.Location = new Point(12, 12);
            logEntryDataGridView.Name = "logEntryDataGridView";
            logEntryDataGridView.ReadOnly = true;
            logEntryDataGridView.RowHeadersWidth = 51;
            logEntryDataGridView.Size = new Size(982, 610);
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
            logTypeCol.HeaderText = "Log Type";
            logTypeCol.MinimumWidth = 6;
            logTypeCol.Name = "logTypeCol";
            logTypeCol.ReadOnly = true;
            logTypeCol.Width = 125;
            // 
            // logTextCol
            // 
            logTextCol.HeaderText = "Log Text";
            logTextCol.MinimumWidth = 6;
            logTextCol.Name = "logTextCol";
            logTextCol.ReadOnly = true;
            logTextCol.Width = 125;
            // 
            // LogForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1006, 673);
            Controls.Add(logEntryDataGridView);
            Controls.Add(pauseLogsCheckbox);
            Controls.Add(clearButton);
            Controls.Add(hideButton);
            MaximumSize = new Size(1024, 720);
            MinimumSize = new Size(1024, 720);
            Name = "LogForm";
            Text = "View Logs";
            ((System.ComponentModel.ISupportInitialize)logEntryDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        #region Custom Initializations

        internal MainForm mainForm;
        private string LOG_TIMESTAMP;

        internal static readonly string DEBUG = "debug", ERROR = "error", WARN = "warn", INFO = "info";
        internal static readonly Dictionary<string, string> LogType = new Dictionary<string, string>
        {
            { },
            { },
            { },
            { }
        }

        private void CustomInitializations()
        {
            public const int LOG_TYPE_INFO = 0;

            LOG_TYPE_INFO_TEXT = " - [INFO] - ";
            LOG_TYPE_WARN_TEXT = " - [WARN] - ";
            LOG_TYPE_ERROR_TEXT = " - [ERROR] - ";

            LOG_TIMESTAMP = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        #endregion
        private Button hideButton;
        private Button clearButton;
        private CheckBox pauseLogsCheckbox;
        private DataGridView logEntryDataGridView;
        private DataGridViewTextBoxColumn logTimestampCol;
        private DataGridViewTextBoxColumn logTypeCol;
        private DataGridViewTextBoxColumn logTextCol;
    }
}