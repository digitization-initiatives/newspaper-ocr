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
            SuspendLayout();
            // 
            // browse_Button
            // 
            browse_Button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            browse_Button.Location = new Point(974, 7);
            browse_Button.Name = "browse_Button";
            browse_Button.Size = new Size(120, 31);
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
            browse_TextBox.Size = new Size(956, 27);
            browse_TextBox.TabIndex = 1;
            // 
            // exitButton
            // 
            exitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            exitButton.Location = new Point(1130, 632);
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
            imageFilesListView.Size = new Size(1238, 582);
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
            optionsButton.Location = new Point(878, 632);
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
            beginOCRButton.Location = new Point(12, 632);
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
            viewLogsButton.Location = new Point(1004, 632);
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
            numberOfImagesLoadedLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            numberOfImagesLoadedLabel.AutoSize = true;
            numberOfImagesLoadedLabel.Location = new Point(218, 636);
            numberOfImagesLoadedLabel.Name = "numberOfImagesLoadedLabel";
            numberOfImagesLoadedLabel.Size = new Size(159, 20);
            numberOfImagesLoadedLabel.TabIndex = 8;
            numberOfImagesLoadedLabel.Text = "No. of Images Loaded:";
            // 
            // numberOfImages
            // 
            numberOfImages.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            numberOfImages.AutoSize = true;
            numberOfImages.Location = new Point(383, 636);
            numberOfImages.Name = "numberOfImages";
            numberOfImages.Size = new Size(15, 20);
            numberOfImages.TabIndex = 9;
            numberOfImages.Text = "-";
            // 
            // startOverButton
            // 
            startOverButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            startOverButton.Location = new Point(752, 632);
            startOverButton.Name = "startOverButton";
            startOverButton.Size = new Size(120, 29);
            startOverButton.TabIndex = 10;
            startOverButton.Text = "Start Over";
            startOverButton.UseVisualStyleBackColor = true;
            startOverButton.Click += startOverButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 673);
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
    }
}
