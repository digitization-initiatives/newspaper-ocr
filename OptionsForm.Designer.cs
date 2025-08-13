using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using System.Runtime.Intrinsics.X86;
using System;

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
            components = new System.ComponentModel.Container();
            tessdataLocationLabel = new Label();
            tessdataLocationTextBox = new TextBox();
            tessdataLocationBrowseButton = new Button();
            ocrOutputLocationLabel = new Label();
            ocrOutputLocationBrowseButton = new Button();
            ocrOutputLocationTextBox = new TextBox();
            concurrentOCRJobsLabel = new Label();
            closeButton = new Button();
            resetToDefaultButton = new Button();
            tessdataLocation_folderBrowserDialog = new FolderBrowserDialog();
            ocrOutputLocation_folderBrowserDialog = new FolderBrowserDialog();
            saveChangesButton = new Button();
            ocrLangComboBox = new ComboBox();
            ocrLangLabel = new Label();
            tileSizeComboBox = new ComboBox();
            tileSizeLabel = new Label();
            logLocation_folderBrowserDialog = new FolderBrowserDialog();
            applicationSettingsLabel = new Label();
            ocrSettingsLabel = new Label();
            sourceImageFileFormatComboBox = new ComboBox();
            sourceImageFileFormatLabel = new Label();
            optionsFormStatusStrip = new StatusStrip();
            tessdataLocationTextBoxToolTip = new ToolTip(components);
            ocrOutputLocationTextBoxToolTip = new ToolTip(components);
            issueFolderNameValidationRegexTextBoxLabel = new Label();
            issueFolderNameValidationRegexTextBox = new TextBox();
            jp2CompressionLevelLabel = new Label();
            jp2CompressionLevelTrackbar = new TrackBar();
            jp2CompressionLevelValue = new Label();
            concurrentOCRJobsNumericUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)jp2CompressionLevelTrackbar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)concurrentOCRJobsNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // tessdataLocationLabel
            // 
            tessdataLocationLabel.AutoSize = true;
            tessdataLocationLabel.Location = new Point(17, 44);
            tessdataLocationLabel.Name = "tessdataLocationLabel";
            tessdataLocationLabel.Size = new Size(590, 20);
            tessdataLocationLabel.TabIndex = 0;
            tessdataLocationLabel.Text = "Tessdata Location: (choose your Tessdata location or leave as default to use embedded)";
            // 
            // tessdataLocationTextBox
            // 
            tessdataLocationTextBox.Location = new Point(17, 67);
            tessdataLocationTextBox.Name = "tessdataLocationTextBox";
            tessdataLocationTextBox.ReadOnly = true;
            tessdataLocationTextBox.Size = new Size(627, 27);
            tessdataLocationTextBox.TabIndex = 1;
            // 
            // tessdataLocationBrowseButton
            // 
            tessdataLocationBrowseButton.Location = new Point(650, 66);
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
            ocrOutputLocationLabel.Location = new Point(17, 110);
            ocrOutputLocationLabel.Name = "ocrOutputLocationLabel";
            ocrOutputLocationLabel.Size = new Size(152, 20);
            ocrOutputLocationLabel.TabIndex = 3;
            ocrOutputLocationLabel.Text = "OCR Output Location:";
            // 
            // ocrOutputLocationBrowseButton
            // 
            ocrOutputLocationBrowseButton.Location = new Point(650, 132);
            ocrOutputLocationBrowseButton.Name = "ocrOutputLocationBrowseButton";
            ocrOutputLocationBrowseButton.Size = new Size(120, 29);
            ocrOutputLocationBrowseButton.TabIndex = 5;
            ocrOutputLocationBrowseButton.Text = "... Browse ...";
            ocrOutputLocationBrowseButton.UseVisualStyleBackColor = true;
            ocrOutputLocationBrowseButton.Click += ocrOutputLocationBrowseButton_Click;
            // 
            // ocrOutputLocationTextBox
            // 
            ocrOutputLocationTextBox.Location = new Point(17, 133);
            ocrOutputLocationTextBox.Name = "ocrOutputLocationTextBox";
            ocrOutputLocationTextBox.ReadOnly = true;
            ocrOutputLocationTextBox.Size = new Size(627, 27);
            ocrOutputLocationTextBox.TabIndex = 4;
            // 
            // concurrentOCRJobsLabel
            // 
            concurrentOCRJobsLabel.AutoSize = true;
            concurrentOCRJobsLabel.Location = new Point(270, 213);
            concurrentOCRJobsLabel.Name = "concurrentOCRJobsLabel";
            concurrentOCRJobsLabel.Size = new Size(150, 20);
            concurrentOCRJobsLabel.TabIndex = 6;
            concurrentOCRJobsLabel.Text = "Concurrent OCR Jobs:";
            // 
            // closeButton
            // 
            closeButton.Location = new Point(620, 612);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(150, 29);
            closeButton.TabIndex = 8;
            closeButton.Text = "Close Options";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += closeButton_Click;
            // 
            // resetToDefaultButton
            // 
            resetToDefaultButton.Location = new Point(308, 612);
            resetToDefaultButton.Name = "resetToDefaultButton";
            resetToDefaultButton.Size = new Size(150, 29);
            resetToDefaultButton.TabIndex = 9;
            resetToDefaultButton.Text = "Reset to Default";
            resetToDefaultButton.UseVisualStyleBackColor = true;
            resetToDefaultButton.Click += resetToDefaultButton_Click;
            // 
            // saveChangesButton
            // 
            saveChangesButton.Location = new Point(464, 612);
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
            ocrLangComboBox.Items.AddRange(new object[] { "eng", "spa", "fra", "jpn" });
            ocrLangComboBox.Location = new Point(17, 236);
            ocrLangComboBox.Name = "ocrLangComboBox";
            ocrLangComboBox.Size = new Size(247, 28);
            ocrLangComboBox.TabIndex = 12;
            // 
            // ocrLangLabel
            // 
            ocrLangLabel.AutoSize = true;
            ocrLangLabel.Location = new Point(17, 213);
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
            tileSizeComboBox.Location = new Point(523, 236);
            tileSizeComboBox.Name = "tileSizeComboBox";
            tileSizeComboBox.Size = new Size(247, 28);
            tileSizeComboBox.TabIndex = 14;
            // 
            // tileSizeLabel
            // 
            tileSizeLabel.AutoSize = true;
            tileSizeLabel.Location = new Point(523, 213);
            tileSizeLabel.Name = "tileSizeLabel";
            tileSizeLabel.Size = new Size(67, 20);
            tileSizeLabel.TabIndex = 13;
            tileSizeLabel.Text = "Tile Size:";
            // 
            // applicationSettingsLabel
            // 
            applicationSettingsLabel.AutoSize = true;
            applicationSettingsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            applicationSettingsLabel.Location = new Point(12, 16);
            applicationSettingsLabel.Name = "applicationSettingsLabel";
            applicationSettingsLabel.Size = new Size(154, 20);
            applicationSettingsLabel.TabIndex = 18;
            applicationSettingsLabel.Text = "Application Settings:";
            // 
            // ocrSettingsLabel
            // 
            ocrSettingsLabel.AutoSize = true;
            ocrSettingsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ocrSettingsLabel.Location = new Point(12, 186);
            ocrSettingsLabel.Name = "ocrSettingsLabel";
            ocrSettingsLabel.Size = new Size(108, 20);
            ocrSettingsLabel.TabIndex = 19;
            ocrSettingsLabel.Text = "OCR Settings :";
            // 
            // sourceImageFileFormatComboBox
            // 
            sourceImageFileFormatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sourceImageFileFormatComboBox.FormattingEnabled = true;
            sourceImageFileFormatComboBox.Items.AddRange(new object[] { "tif", "jpg", "png" });
            sourceImageFileFormatComboBox.Location = new Point(17, 303);
            sourceImageFileFormatComboBox.Name = "sourceImageFileFormatComboBox";
            sourceImageFileFormatComboBox.Size = new Size(247, 28);
            sourceImageFileFormatComboBox.TabIndex = 21;
            // 
            // sourceImageFileFormatLabel
            // 
            sourceImageFileFormatLabel.AutoSize = true;
            sourceImageFileFormatLabel.Location = new Point(17, 280);
            sourceImageFileFormatLabel.Name = "sourceImageFileFormatLabel";
            sourceImageFileFormatLabel.Size = new Size(185, 20);
            sourceImageFileFormatLabel.TabIndex = 20;
            sourceImageFileFormatLabel.Text = "Source Image File Format :";
            // 
            // optionsFormStatusStrip
            // 
            optionsFormStatusStrip.ImageScalingSize = new Size(20, 20);
            optionsFormStatusStrip.Location = new Point(0, 651);
            optionsFormStatusStrip.Name = "optionsFormStatusStrip";
            optionsFormStatusStrip.Size = new Size(782, 22);
            optionsFormStatusStrip.TabIndex = 22;
            optionsFormStatusStrip.Text = "statusStrip1";
            // 
            // tessdataLocationTextBoxToolTip
            // 
            tessdataLocationTextBoxToolTip.ShowAlways = true;
            // 
            // issueFolderNameValidationRegexTextBoxLabel
            // 
            issueFolderNameValidationRegexTextBoxLabel.AutoSize = true;
            issueFolderNameValidationRegexTextBoxLabel.Location = new Point(270, 280);
            issueFolderNameValidationRegexTextBoxLabel.Name = "issueFolderNameValidationRegexTextBoxLabel";
            issueFolderNameValidationRegexTextBoxLabel.Size = new Size(250, 20);
            issueFolderNameValidationRegexTextBoxLabel.TabIndex = 23;
            issueFolderNameValidationRegexTextBoxLabel.Text = "Issue Folder Name Validation Regex:";
            // 
            // issueFolderNameValidationRegexTextBox
            // 
            issueFolderNameValidationRegexTextBox.Location = new Point(270, 303);
            issueFolderNameValidationRegexTextBox.Name = "issueFolderNameValidationRegexTextBox";
            issueFolderNameValidationRegexTextBox.Size = new Size(500, 27);
            issueFolderNameValidationRegexTextBox.TabIndex = 24;
            issueFolderNameValidationRegexTextBox.Text = "^[a-zA-Z0-9]+_\\d{4}-\\d{2}-\\d{2}$";
            // 
            // jp2CompressionLevelLabel
            // 
            jp2CompressionLevelLabel.AutoSize = true;
            jp2CompressionLevelLabel.Location = new Point(17, 358);
            jp2CompressionLevelLabel.Name = "jp2CompressionLevelLabel";
            jp2CompressionLevelLabel.Size = new Size(171, 20);
            jp2CompressionLevelLabel.TabIndex = 25;
            jp2CompressionLevelLabel.Text = "JPEG Compression Level:";
            // 
            // jp2CompressionLevelTrackbar
            // 
            jp2CompressionLevelTrackbar.LargeChange = 10;
            jp2CompressionLevelTrackbar.Location = new Point(225, 356);
            jp2CompressionLevelTrackbar.Maximum = 100;
            jp2CompressionLevelTrackbar.Name = "jp2CompressionLevelTrackbar";
            jp2CompressionLevelTrackbar.Size = new Size(545, 56);
            jp2CompressionLevelTrackbar.SmallChange = 5;
            jp2CompressionLevelTrackbar.TabIndex = 26;
            jp2CompressionLevelTrackbar.TickFrequency = 5;
            jp2CompressionLevelTrackbar.Value = 40;
            jp2CompressionLevelTrackbar.Scroll += jp2CompressionLevelTrackbar_Scroll;
            // 
            // jp2CompressionLevelValue
            // 
            jp2CompressionLevelValue.AutoSize = true;
            jp2CompressionLevelValue.Location = new Point(194, 358);
            jp2CompressionLevelValue.Name = "jp2CompressionLevelValue";
            jp2CompressionLevelValue.Size = new Size(25, 20);
            jp2CompressionLevelValue.TabIndex = 27;
            jp2CompressionLevelValue.Text = "40";
            // 
            // concurrentOCRJobsNumericUpDown
            // 
            concurrentOCRJobsNumericUpDown.Location = new Point(270, 236);
            concurrentOCRJobsNumericUpDown.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            concurrentOCRJobsNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            concurrentOCRJobsNumericUpDown.Name = "concurrentOCRJobsNumericUpDown";
            concurrentOCRJobsNumericUpDown.Size = new Size(247, 27);
            concurrentOCRJobsNumericUpDown.TabIndex = 28;
            concurrentOCRJobsNumericUpDown.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // OptionsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 673);
            ControlBox = false;
            Controls.Add(concurrentOCRJobsNumericUpDown);
            Controls.Add(jp2CompressionLevelValue);
            Controls.Add(jp2CompressionLevelTrackbar);
            Controls.Add(jp2CompressionLevelLabel);
            Controls.Add(issueFolderNameValidationRegexTextBox);
            Controls.Add(issueFolderNameValidationRegexTextBoxLabel);
            Controls.Add(optionsFormStatusStrip);
            Controls.Add(sourceImageFileFormatComboBox);
            Controls.Add(sourceImageFileFormatLabel);
            Controls.Add(ocrSettingsLabel);
            Controls.Add(applicationSettingsLabel);
            Controls.Add(tileSizeComboBox);
            Controls.Add(tileSizeLabel);
            Controls.Add(ocrLangComboBox);
            Controls.Add(ocrLangLabel);
            Controls.Add(saveChangesButton);
            Controls.Add(resetToDefaultButton);
            Controls.Add(closeButton);
            Controls.Add(concurrentOCRJobsLabel);
            Controls.Add(ocrOutputLocationBrowseButton);
            Controls.Add(ocrOutputLocationTextBox);
            Controls.Add(ocrOutputLocationLabel);
            Controls.Add(tessdataLocationBrowseButton);
            Controls.Add(tessdataLocationTextBox);
            Controls.Add(tessdataLocationLabel);
            MaximumSize = new Size(800, 720);
            MinimumSize = new Size(800, 720);
            Name = "OptionsForm";
            Text = "Options";
            ((System.ComponentModel.ISupportInitialize)jp2CompressionLevelTrackbar).EndInit();
            ((System.ComponentModel.ISupportInitialize)concurrentOCRJobsNumericUpDown).EndInit();
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

        public MainForm mainForm;
        public LogForm logForm;

        private void CustomInitializations()
        {
            tessdataLocationTextBox.MouseHover += tessdataLocationTextBox_MouseHover;
            tessdataLocationTextBox.MouseLeave += tessdataLocationTextBox_MouseLeave;

            ocrOutputLocationTextBox.MouseHover += ocrOutputLocationTextBox_MouseHover;
            ocrOutputLocationTextBox.MouseLeave += ocrOutputLocationTextBox_MouseLeave;

            setDefaultOptions();
        }

        #endregion

        private Label tessdataLocationLabel;
        private TextBox tessdataLocationTextBox;
        private Button tessdataLocationBrowseButton;
        private Label ocrOutputLocationLabel;
        private Button ocrOutputLocationBrowseButton;
        private TextBox ocrOutputLocationTextBox;
        private Label concurrentOCRJobsLabel;
        private Button closeButton;
        private Button resetToDefaultButton;
        private FolderBrowserDialog tessdataLocation_folderBrowserDialog;
        private FolderBrowserDialog ocrOutputLocation_folderBrowserDialog;
        private Button saveChangesButton;
        private ComboBox ocrLangComboBox;
        private Label ocrLangLabel;
        private ComboBox tileSizeComboBox;
        private Label tileSizeLabel;
        private FolderBrowserDialog logLocation_folderBrowserDialog;
        private Label applicationSettingsLabel;
        private Label ocrSettingsLabel;
        private ComboBox sourceImageFileFormatComboBox;
        private Label sourceImageFileFormatLabel;
        private StatusStrip optionsFormStatusStrip;
        private ToolTip tessdataLocationTextBoxToolTip;
        private ToolTip ocrOutputLocationTextBoxToolTip;
        private Label issueFolderNameValidationRegexTextBoxLabel;
        private TextBox issueFolderNameValidationRegexTextBox;
        private Label jp2CompressionLevelLabel;
        private TrackBar jp2CompressionLevelTrackbar;
        private Label jp2CompressionLevelValue;
        private NumericUpDown concurrentOCRJobsNumericUpDown;
    }
}