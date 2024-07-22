namespace NewspaperOCR
{
    partial class OptionsForm
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
            tessdataLocationLabel = new Label();
            tessdataLocationTextBox = new TextBox();
            tessdataLocationBrowseButton = new Button();
            ocrOutputLocationLabel = new Label();
            ocrOutputLocationBrowseButton = new Button();
            ocrOutputLocationTextBox = new TextBox();
            concurrentOCRJobsLabel = new Label();
            concurrentOCRJobsComboBox = new ComboBox();
            closeButton = new Button();
            resetToDefaultButton = new Button();
            tessdataLocation_folderBrowserDialog = new FolderBrowserDialog();
            ocrOutputLocation_folderBrowserDialog = new FolderBrowserDialog();
            saveChangesButton = new Button();
            ocrLangComboBox = new ComboBox();
            ocrLangLabel = new Label();
            tileSizeComboBox = new ComboBox();
            tileSizeLabel = new Label();
            SuspendLayout();
            // 
            // tessdataLocationLabel
            // 
            tessdataLocationLabel.AutoSize = true;
            tessdataLocationLabel.Location = new Point(12, 9);
            tessdataLocationLabel.Name = "tessdataLocationLabel";
            tessdataLocationLabel.Size = new Size(130, 20);
            tessdataLocationLabel.TabIndex = 0;
            tessdataLocationLabel.Text = "Tessdata Location:";
            // 
            // tessdataLocationTextBox
            // 
            tessdataLocationTextBox.Location = new Point(17, 32);
            tessdataLocationTextBox.Name = "tessdataLocationTextBox";
            tessdataLocationTextBox.ReadOnly = true;
            tessdataLocationTextBox.Size = new Size(627, 27);
            tessdataLocationTextBox.TabIndex = 1;
            // 
            // tessdataLocationBrowseButton
            // 
            tessdataLocationBrowseButton.Location = new Point(650, 31);
            tessdataLocationBrowseButton.Name = "tessdataLocationBrowseButton";
            tessdataLocationBrowseButton.Size = new Size(120, 29);
            tessdataLocationBrowseButton.TabIndex = 2;
            tessdataLocationBrowseButton.Text = "... Browse ...";
            tessdataLocationBrowseButton.UseVisualStyleBackColor = true;
            tessdataLocationBrowseButton.Click += tessdataLocationBrowseButton_Click;
            // 
            // ocrOutputLocationLabel
            // 
            ocrOutputLocationLabel.AutoSize = true;
            ocrOutputLocationLabel.Location = new Point(12, 75);
            ocrOutputLocationLabel.Name = "ocrOutputLocationLabel";
            ocrOutputLocationLabel.Size = new Size(152, 20);
            ocrOutputLocationLabel.TabIndex = 3;
            ocrOutputLocationLabel.Text = "OCR Output Location:";
            // 
            // ocrOutputLocationBrowseButton
            // 
            ocrOutputLocationBrowseButton.Location = new Point(650, 97);
            ocrOutputLocationBrowseButton.Name = "ocrOutputLocationBrowseButton";
            ocrOutputLocationBrowseButton.Size = new Size(120, 29);
            ocrOutputLocationBrowseButton.TabIndex = 5;
            ocrOutputLocationBrowseButton.Text = "... Browse ...";
            ocrOutputLocationBrowseButton.UseVisualStyleBackColor = true;
            ocrOutputLocationBrowseButton.Click += ocrOutputLocationBrowseButton_Click;
            // 
            // ocrOutputLocationTextBox
            // 
            ocrOutputLocationTextBox.Location = new Point(17, 98);
            ocrOutputLocationTextBox.Name = "ocrOutputLocationTextBox";
            ocrOutputLocationTextBox.ReadOnly = true;
            ocrOutputLocationTextBox.Size = new Size(627, 27);
            ocrOutputLocationTextBox.TabIndex = 4;
            // 
            // concurrentOCRJobsLabel
            // 
            concurrentOCRJobsLabel.AutoSize = true;
            concurrentOCRJobsLabel.Location = new Point(222, 150);
            concurrentOCRJobsLabel.Name = "concurrentOCRJobsLabel";
            concurrentOCRJobsLabel.Size = new Size(150, 20);
            concurrentOCRJobsLabel.TabIndex = 6;
            concurrentOCRJobsLabel.Text = "Concurrent OCR Jobs:";
            // 
            // concurrentOCRJobsComboBox
            // 
            concurrentOCRJobsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            concurrentOCRJobsComboBox.FormattingEnabled = true;
            concurrentOCRJobsComboBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            concurrentOCRJobsComboBox.Location = new Point(223, 173);
            concurrentOCRJobsComboBox.Name = "concurrentOCRJobsComboBox";
            concurrentOCRJobsComboBox.Size = new Size(150, 28);
            concurrentOCRJobsComboBox.TabIndex = 7;
            // 
            // closeButton
            // 
            closeButton.Location = new Point(620, 632);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(150, 29);
            closeButton.TabIndex = 8;
            closeButton.Text = "Close Options";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += closeButton_Click;
            // 
            // resetToDefaultButton
            // 
            resetToDefaultButton.Location = new Point(308, 632);
            resetToDefaultButton.Name = "resetToDefaultButton";
            resetToDefaultButton.Size = new Size(150, 29);
            resetToDefaultButton.TabIndex = 9;
            resetToDefaultButton.Text = "Reset to Default";
            resetToDefaultButton.UseVisualStyleBackColor = true;
            resetToDefaultButton.Click += resetToDefaultButton_Click;
            // 
            // saveChangesButton
            // 
            saveChangesButton.Location = new Point(464, 632);
            saveChangesButton.Name = "saveChangesButton";
            saveChangesButton.Size = new Size(150, 29);
            saveChangesButton.TabIndex = 10;
            saveChangesButton.Text = "Save Changes";
            saveChangesButton.UseVisualStyleBackColor = true;
            saveChangesButton.Click += saveChangesButton_Click;
            // 
            // ocrLangComboBox
            // 
            ocrLangComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ocrLangComboBox.FormattingEnabled = true;
            ocrLangComboBox.Items.AddRange(new object[] { "eng - English", "spa - Spanish; Castilian", "fra - French", "jpn - Japanese" });
            ocrLangComboBox.Location = new Point(17, 173);
            ocrLangComboBox.Name = "ocrLangComboBox";
            ocrLangComboBox.Size = new Size(200, 28);
            ocrLangComboBox.TabIndex = 12;
            // 
            // ocrLangLabel
            // 
            ocrLangLabel.AutoSize = true;
            ocrLangLabel.Location = new Point(17, 150);
            ocrLangLabel.Name = "ocrLangLabel";
            ocrLangLabel.Size = new Size(110, 20);
            ocrLangLabel.TabIndex = 11;
            ocrLangLabel.Text = "OCR Language:";
            // 
            // tileSizeComboBox
            // 
            tileSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tileSizeComboBox.FormattingEnabled = true;
            tileSizeComboBox.Items.AddRange(new object[] { "[256x256]", "[512x512]", "[1024x1024]" });
            tileSizeComboBox.Location = new Point(379, 173);
            tileSizeComboBox.Name = "tileSizeComboBox";
            tileSizeComboBox.Size = new Size(150, 28);
            tileSizeComboBox.TabIndex = 14;
            // 
            // tileSizeLabel
            // 
            tileSizeLabel.AutoSize = true;
            tileSizeLabel.Location = new Point(377, 150);
            tileSizeLabel.Name = "tileSizeLabel";
            tileSizeLabel.Size = new Size(67, 20);
            tileSizeLabel.TabIndex = 13;
            tileSizeLabel.Text = "Tile Size:";
            // 
            // OptionsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 673);
            Controls.Add(tileSizeComboBox);
            Controls.Add(tileSizeLabel);
            Controls.Add(ocrLangComboBox);
            Controls.Add(ocrLangLabel);
            Controls.Add(saveChangesButton);
            Controls.Add(resetToDefaultButton);
            Controls.Add(closeButton);
            Controls.Add(concurrentOCRJobsComboBox);
            Controls.Add(concurrentOCRJobsLabel);
            Controls.Add(ocrOutputLocationBrowseButton);
            Controls.Add(ocrOutputLocationTextBox);
            Controls.Add(ocrOutputLocationLabel);
            Controls.Add(tessdataLocationBrowseButton);
            Controls.Add(tessdataLocationTextBox);
            Controls.Add(tessdataLocationLabel);
            Name = "OptionsForm";
            Text = "Options";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        #region Custom Initializations

        public MainForm mainForm;
        public LogForm logForm;
        private void CustomInitializations()
        {
            tessdataLocationTextBox.Text = Properties.Settings.Default.TessdataLocation;
            ocrOutputLocationTextBox.Text = Properties.Settings.Default.OCROutputLocation;
            concurrentOCRJobsComboBox.SelectedIndex = 0;
            ocrLangComboBox.SelectedIndex = 0;
            tileSizeComboBox.SelectedIndex = 2;
        }

        #endregion

        private Label tessdataLocationLabel;
        private TextBox tessdataLocationTextBox;
        private Button tessdataLocationBrowseButton;
        private Label ocrOutputLocationLabel;
        private Button ocrOutputLocationBrowseButton;
        private TextBox ocrOutputLocationTextBox;
        private Label concurrentOCRJobsLabel;
        private ComboBox concurrentOCRJobsComboBox;
        private Button closeButton;
        private Button resetToDefaultButton;
        private FolderBrowserDialog tessdataLocation_folderBrowserDialog;
        private FolderBrowserDialog ocrOutputLocation_folderBrowserDialog;
        private Button saveChangesButton;
        private ComboBox ocrLangComboBox;
        private Label ocrLangLabel;
        private ComboBox tileSizeComboBox;
        private Label tileSizeLabel;
    }
}