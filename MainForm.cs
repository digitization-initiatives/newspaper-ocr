using ImageMagick;
using ImageMagick.Formats;
using NewspaperOCR.src;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TesseractOCR;
using TesseractOCR.Enums;
using TesseractOCR.Renderers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NewspaperOCR
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            CustomInitializations();
        }

        #region My Custom Functions
        public void resetMainFormControls()
        {
            folderBrowserDialog.SelectedPath = String.Empty;
            folderBrowserTextBox.Text = String.Empty;
            folderBrowserButton.Enabled = true;
            loadImagesButton.Enabled = false;

            sourceFilesListView.Items.Clear();
            
            beginOCRButton.Enabled = false;
            cancelOCRButton.Enabled = false;
            optionsButton.Enabled = true;
        }

        public void resetMainFormStatusBar()
        {
            statusBarItem_numberOfImagesLoaded.Text = "No Image Files Loaded";
            statusBarItem_numberOfCompletedItems.Text = "-";
        }

        private void imageFilesListView_SizeChanged(object sender, EventArgs e)
        {
            sourceFilesListView_filenameCol.Width = sourceFilesListView.Width - 150;
        }

        #endregion My Custom Functions

        private void folderBrowserButton_Click(object sender, EventArgs e)
        {
            folderBrowserTextBox.Text = String.Empty;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderBrowserTextBox.Text = folderBrowserDialog.SelectedPath;
                loadImagesButton.Enabled = true;

                logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"{folderBrowserDialog.SelectedPath} is selected.");
            }
            else
            {
                folderBrowserTextBox.Text = String.Empty;
                folderBrowserDialog.SelectedPath = String.Empty;
                loadImagesButton.Enabled = false;

                logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"No folder has been selected.");
            }
        }
        private void loadImagesButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.SelectedPath != String.Empty)
            {
                if (ocr.ValidateIssueFolderNames(folderBrowserDialog.SelectedPath))
                {
                    List<string> imageFiles = new List<string>();

                    imageFiles.AddRange(Directory.GetFiles(folderBrowserDialog.SelectedPath, $"*.{Properties.Settings.Default.SourceImageFileFormat}", SearchOption.AllDirectories));

                    foreach (string imageFile in imageFiles)
                    {
                        ListViewItem item = new ListViewItem(imageFile);
                        item.SubItems.Add("...");
                        item.SubItems.Add("00:00:00");

                        sourceFilesListView.Items.Add(item);
                    }

                    ocr.totalNumberOfImages = imageFiles.Count;
                    ocr.completedOcrJobs = 0;

                    statusBarItem_numberOfImagesLoaded.Text = $"No. of Images Loaded: {ocr.totalNumberOfImages}";
                    statusBarItem_numberOfCompletedItems.Text = $"{ocr.completedOcrJobs}";

                    beginOCRButton.Enabled = true;

                    logForm.sendToLog(LogForm.LogType[LogForm.INFO], $"All issue folders in \"{folderBrowserDialog.SelectedPath}\" have successfully passed validation.");
                }
                else
                {
                    MessageBox.Show($"\"{folderBrowserDialog.SelectedPath}\" is empty or contains invalid issue folders, see log for more details.", "Invalid Issue Folders Found!");
                    logForm.sendToLog(LogForm.LogType[LogForm.ERROR], $"\"{folderBrowserDialog.SelectedPath}\" contains invalid issue folders, validation faild.");
                }
            }
        }

        private async void beginOCRButton_Click(object sender, EventArgs e)
        {
            beginOCRButton.Enabled = false;
            startOverButton.Enabled = false;
            optionsButton.Enabled = false;
            folderBrowserButton.Enabled = false;
            loadImagesButton.Enabled = false;

            totalTimeElapsedStopwatch.Start();
            totalTimeElapsedTimer.Start();

            cancelOCRButton.Enabled = true;

            ocr.CreateOutputDirectories();

            await ocr.ProcessOCRQueue();

            ocr.ValidateOutputFiles();

            cancelOCRButton.Enabled = false;

            totalTimeElapsedStopwatch.Stop();
            totalTimeElapsedTimer.Stop();
            statusBarItem_timeElapsed.Text = $"{totalTimeElapsedStopwatch.Elapsed:hh\\:mm\\:ss}";

            startOverButton.Enabled = true;
            optionsButton.Enabled = true;
        }

        private async void cancelOCRButton_Click(object sender, EventArgs e)
        {
            cancelOCRButton.Enabled = false;

            await ocr.CancelQueue();

            this.ActiveControl = null;
        }
        private void startOverButton_Click(object sender, EventArgs e)
        {
            if (ocr.ocrTasks.Count > 0)
            {
                MessageBox.Show("OCR tasks are currently running! Please cancel all jobs first and wait for the remaining {ocr.ocrTasks.Count} jobs to complete.", "OCR Tasks Are Running!");
                logForm.sendToLog(LogForm.LogType[LogForm.WARN], $"OCR tasks are currently running! Please cancel all jobs first and wait for the remaining {ocr.ocrTasks.Count} jobs to complete.");
            }
            else
            {
                resetMainFormControls();
                resetMainFormStatusBar();

                if (ocr.ocrItemsList != null)
                {
                    ocr.ocrItemsList.Clear();
                }

                if (ocr.ocrItemsQueue != null)
                {
                    ocr.ocrItemsQueue.Clear();
                }
            }
        }
        private void optionsButton_Click(object sender, EventArgs e)
        {
            if (optionsForm.Visible)
            {
                optionsForm.BringToFront();
            }
            else
            {
                optionsForm.Location = new Point(this.Location.X + this.Width + 20, this.Location.Y);
                optionsForm.Show();
            }
        }
        private void viewLogsButton_Click(object sender, EventArgs e)
        {
            if (logForm.Visible)
            {
                logForm.BringToFront();
            }
            else
            {
                logForm.Location = new Point(this.Location.X + this.Width + 10, this.Location.Y);
                logForm.Show();
            }
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void MainForm_Click(object sender, EventArgs e)
        {
            sourceFilesListView.SelectedItems.Clear();
        }
    }
}
