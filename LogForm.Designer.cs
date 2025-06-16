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
            pauseAutoScrollCheckbox = new CheckBox();
            logEntryDataGridView = new DataGridView();
            logTimestampCol = new DataGridViewTextBoxColumn();
            logTypeCol = new DataGridViewTextBoxColumn();
            logMessageCol = new DataGridViewTextBoxColumn();
            viewLogFileButton = new Button();
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
            // pauseAutoScrollCheckbox
            // 
            pauseAutoScrollCheckbox.AutoSize = true;
            pauseAutoScrollCheckbox.Location = new Point(12, 635);
            pauseAutoScrollCheckbox.Name = "pauseAutoScrollCheckbox";
            pauseAutoScrollCheckbox.Size = new Size(147, 24);
            pauseAutoScrollCheckbox.TabIndex = 5;
            pauseAutoScrollCheckbox.Text = "Pause Auto-Scroll";
            pauseAutoScrollCheckbox.UseVisualStyleBackColor = true;
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
            // viewLogFileButton
            // 
            viewLogFileButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            viewLogFileButton.Location = new Point(602, 632);
            viewLogFileButton.Name = "viewLogFileButton";
            viewLogFileButton.Size = new Size(140, 29);
            viewLogFileButton.TabIndex = 7;
            viewLogFileButton.Text = "View Log File";
            viewLogFileButton.UseVisualStyleBackColor = true;
            viewLogFileButton.Click += viewLogFileButton_Click;
            // 
            // LogForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1006, 673);
            Controls.Add(viewLogFileButton);
            Controls.Add(logEntryDataGridView);
            Controls.Add(pauseAutoScrollCheckbox);
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

        internal static readonly int MAX_LOG_ROWS = 500;
        internal static readonly int INFO = 0, WARN = 1, ERROR = 2, DEBUG = 3;
        internal static readonly Dictionary<int, string> LogType = new Dictionary<int, string>
        {
            { INFO, "[INFO]" },
            { WARN, "[WARN]" },
            { ERROR, "[ERROR]"},
            { DEBUG, "[DEBUG]"}
        };

        private void CustomInitializations()
        {
            logTimestampCol.Width = 150;
            logTypeCol.Width = 60;
            logMessageCol.Width = logEntryDataGridView.Width - logTimestampCol.Width - logTypeCol.Width - 3;

            logTimestampCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            logTypeCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            logMessageCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
        }

        #endregion
        private Button hideButton;
        private Button clearButton;
        private CheckBox pauseAutoScrollCheckbox;
        private DataGridView logEntryDataGridView;
        private DataGridViewTextBoxColumn logTimestampCol;
        private DataGridViewTextBoxColumn logTypeCol;
        private DataGridViewTextBoxColumn logMessageCol;
        private Button viewLogFileButton;
    }
}