using NewspaperOCR.src;
using System.Runtime.CompilerServices;

namespace NewspaperOCR
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            browse_Button = new Button();
            browse_TextBox = new TextBox();
            exitButton = new Button();
            imageFilesListView = new ListView();
            filenameCol = new ColumnHeader();
            ocrStatusCol = new ColumnHeader();
            optionsButton = new Button();
            beginOCRButton = new Button();
            viewLogsButton = new Button();
            browse_folderBrowserDialog = new FolderBrowserDialog();
            loadImagesButton = new Button();
            numberOfImagesLoadedLabel = new Label();
            numberOfImages = new Label();
            startOverButton = new Button();
            statusBar = new StatusStrip();
            statusBarItem_Status = new ToolStripStatusLabel();
            statusBarItem_Message = new ToolStripStatusLabel();
            numberOfCompletedOcr = new Label();
            numberOfCompletedOcrLabel = new Label();
            statusBar.SuspendLayout();
            SuspendLayout();
            // 
            // browse_Button
            // 
            browse_Button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            browse_Button.Location = new Point(944, 7);
            browse_Button.Name = "browse_Button";
            browse_Button.Size = new Size(150, 31);
            browse_Button.TabIndex = 0;
            browse_Button.Text = "... Browse ...";
            browse_Button.UseVisualStyleBackColor = true;
            browse_Button.Click += browse_Button_Click;
            // 
            // browse_TextBox
            // 
            browse_TextBox.AcceptsReturn = true;
            browse_TextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            browse_TextBox.Location = new Point(12, 9);
            browse_TextBox.Name = "browse_TextBox";
            browse_TextBox.ReadOnly = true;
            browse_TextBox.Size = new Size(926, 27);
            browse_TextBox.TabIndex = 1;
            // 
            // exitButton
            // 
            exitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            exitButton.Location = new Point(1130, 614);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(120, 29);
            exitButton.TabIndex = 2;
            exitButton.Text = "Exit";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // imageFilesListView
            // 
            imageFilesListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            imageFilesListView.Columns.AddRange(new ColumnHeader[] { filenameCol, ocrStatusCol });
            imageFilesListView.Location = new Point(12, 44);
            imageFilesListView.Name = "imageFilesListView";
            imageFilesListView.Size = new Size(1238, 564);
            imageFilesListView.TabIndex = 3;
            imageFilesListView.UseCompatibleStateImageBehavior = false;
            imageFilesListView.View = View.Details;
            // 
            // filenameCol
            // 
            filenameCol.Text = "Filename";
            filenameCol.Width = 1088;
            // 
            // ocrStatusCol
            // 
            ocrStatusCol.Text = "OCR Status";
            ocrStatusCol.Width = 120;
            // 
            // optionsButton
            // 
            optionsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            optionsButton.Location = new Point(878, 614);
            optionsButton.Name = "optionsButton";
            optionsButton.Size = new Size(120, 29);
            optionsButton.TabIndex = 4;
            optionsButton.Text = "Options";
            optionsButton.UseVisualStyleBackColor = true;
            optionsButton.Click += optionsButton_Click;
            // 
            // beginOCRButton
            // 
            beginOCRButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            beginOCRButton.Location = new Point(12, 614);
            beginOCRButton.Name = "beginOCRButton";
            beginOCRButton.Size = new Size(200, 29);
            beginOCRButton.TabIndex = 5;
            beginOCRButton.Text = "Begin OCR";
            beginOCRButton.UseVisualStyleBackColor = true;
            beginOCRButton.Click += beginOCRButton_Click;
            // 
            // viewLogsButton
            // 
            viewLogsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            viewLogsButton.Location = new Point(1004, 614);
            viewLogsButton.Name = "viewLogsButton";
            viewLogsButton.Size = new Size(120, 29);
            viewLogsButton.TabIndex = 6;
            viewLogsButton.Text = "View Logs";
            viewLogsButton.UseVisualStyleBackColor = true;
            viewLogsButton.Click += viewLogsButton_Click;
            // 
            // loadImagesButton
            // 
            loadImagesButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            loadImagesButton.Location = new Point(1100, 7);
            loadImagesButton.Name = "loadImagesButton";
            loadImagesButton.Size = new Size(150, 31);
            loadImagesButton.TabIndex = 7;
            loadImagesButton.Text = "Load Images";
            loadImagesButton.UseVisualStyleBackColor = true;
            loadImagesButton.Click += loadImagesButton_Click;
            // 
            // numberOfImagesLoadedLabel
            // 
            numberOfImagesLoadedLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            numberOfImagesLoadedLabel.AutoSize = true;
            numberOfImagesLoadedLabel.Location = new Point(292, 618);
            numberOfImagesLoadedLabel.Name = "numberOfImagesLoadedLabel";
            numberOfImagesLoadedLabel.Size = new Size(159, 20);
            numberOfImagesLoadedLabel.TabIndex = 8;
            numberOfImagesLoadedLabel.Text = "No. of Images Loaded:";
            // 
            // numberOfImages
            // 
            numberOfImages.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            numberOfImages.AutoSize = true;
            numberOfImages.Location = new Point(457, 618);
            numberOfImages.Name = "numberOfImages";
            numberOfImages.Size = new Size(15, 20);
            numberOfImages.TabIndex = 9;
            numberOfImages.Text = "-";
            // 
            // startOverButton
            // 
            startOverButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            startOverButton.Location = new Point(752, 614);
            startOverButton.Name = "startOverButton";
            startOverButton.Size = new Size(120, 29);
            startOverButton.TabIndex = 10;
            startOverButton.Text = "Start Over";
            startOverButton.UseVisualStyleBackColor = true;
            startOverButton.Click += startOverButton_Click;
            // 
            // statusBar
            // 
            statusBar.ImageScalingSize = new Size(20, 20);
            statusBar.Items.AddRange(new ToolStripItem[] { statusBarItem_Status, statusBarItem_Message });
            statusBar.Location = new Point(0, 647);
            statusBar.Name = "statusBar";
            statusBar.Size = new Size(1262, 26);
            statusBar.TabIndex = 16;
            statusBar.Text = "statusBar";
            // 
            // statusBarItem_Status
            // 
            statusBarItem_Status.Name = "statusBarItem_Status";
            statusBarItem_Status.Size = new Size(162, 20);
            statusBarItem_Status.Text = "No Image Files Loaded";
            // 
            // statusBarItem_Message
            // 
            statusBarItem_Message.Name = "statusBarItem_Message";
            statusBarItem_Message.Size = new Size(0, 20);
            // 
            // numberOfCompletedOcr
            // 
            numberOfCompletedOcr.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            numberOfCompletedOcr.AutoSize = true;
            numberOfCompletedOcr.Location = new Point(616, 618);
            numberOfCompletedOcr.Name = "numberOfCompletedOcr";
            numberOfCompletedOcr.Size = new Size(15, 20);
            numberOfCompletedOcr.TabIndex = 18;
            numberOfCompletedOcr.Text = "-";
            // 
            // numberOfCompletedOcrLabel
            // 
            numberOfCompletedOcrLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            numberOfCompletedOcrLabel.AutoSize = true;
            numberOfCompletedOcrLabel.Location = new Point(524, 618);
            numberOfCompletedOcrLabel.Name = "numberOfCompletedOcrLabel";
            numberOfCompletedOcrLabel.Size = new Size(86, 20);
            numberOfCompletedOcrLabel.TabIndex = 17;
            numberOfCompletedOcrLabel.Text = "Completed:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 673);
            Controls.Add(numberOfCompletedOcr);
            Controls.Add(numberOfCompletedOcrLabel);
            Controls.Add(statusBar);
            Controls.Add(startOverButton);
            Controls.Add(numberOfImages);
            Controls.Add(numberOfImagesLoadedLabel);
            Controls.Add(loadImagesButton);
            Controls.Add(viewLogsButton);
            Controls.Add(beginOCRButton);
            Controls.Add(optionsButton);
            Controls.Add(imageFilesListView);
            Controls.Add(exitButton);
            Controls.Add(browse_TextBox);
            Controls.Add(browse_Button);
            MinimumSize = new Size(1280, 720);
            Name = "MainForm";
            Text = "Newspaper OCR";
            statusBar.ResumeLayout(false);
            statusBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        #region My custom initializations
        private LogForm logForm;
        private OptionsForm optionsForm;
        public Button viewLogsButton;
        private List<DirectoryStructure> directoryStructure;

        private void CustomInitializations()
        {
            //Initialize forms and properties:
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(200, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2 - 50);

            logForm = new LogForm();
            logForm.mainForm = this;
            logForm.StartPosition = FormStartPosition.Manual;
            logForm.Location = new Point(this.Location.X + this.Width + 10, this.Location.Y);

            optionsForm = new OptionsForm();
            optionsForm.mainForm = this;
            optionsForm.logForm = logForm;
            optionsForm.StartPosition = FormStartPosition.Manual;
            optionsForm.Location = new Point(this.Location.X + 50, this.Location.Y + 50);

            //Other initializations:
            directoryStructure = new List<DirectoryStructure>();

            loadImagesButton.Enabled = false;
            beginOCRButton.Enabled = false;
            clearStatusBar();

            filenameCol.Width = imageFilesListView.Width - 150;
            
            imageFilesListView.SizeChanged += imageFilesListView_SizeChanged;

            setDefaultOptions();

        }
        #endregion

        private Button browse_Button;
        private TextBox browse_TextBox;
        private Button exitButton;
        private ListView imageFilesListView;
        private ColumnHeader filenameCol;
        private Button optionsButton;
        private Button beginOCRButton;
        private ColumnHeader ocrStatusCol;
        private FolderBrowserDialog browse_folderBrowserDialog;
        private Button loadImagesButton;
        private Label numberOfImagesLoadedLabel;
        private Label numberOfImages;
        private Button startOverButton;
        private ToolStripStatusLabel statusStripItem_Status;
        private StatusStrip statusBar;
        private ToolStripStatusLabel statusBarItem_Status;
        private ToolStripStatusLabel statusBarItem_Message;
        private Label numberOfCompletedOcr;
        private Label numberOfCompletedOcrLabel;
    }
}
