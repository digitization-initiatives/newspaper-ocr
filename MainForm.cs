using ImageMagick;
using ImageMagick.Formats;
using NewspaperOCR.src;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
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
            // Reset MainForm UI:
            folderBrowserDialog.SelectedPath = String.Empty;
            folderBrowserTextBox.Text = String.Empty;
            loadImagesButton.Enabled = false;

            sourceFilesListView.Items.Clear();
            sourceFilesListView_filenameCol.Width = sourceFilesListView.Width - 150;

            beginOCRButton.Enabled = false;
            cancelOCRButton.Enabled = false;

            statusBarItem_numberOfImagesLoaded.Text = "No Image Files Loaded";
            statusBarItem_numberOfCompletedItems.Text = "-";
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
                if (ocrHelper.validateIssueFolderNames(folderBrowserDialog.SelectedPath))
                {
                    List<string> imageFiles = new List<string>();

                    imageFiles.AddRange(Directory.GetFiles(folderBrowserDialog.SelectedPath, $"*.{Properties.Settings.Default.SourceImageFileFormat}", SearchOption.AllDirectories));

                    foreach (string imageFile in imageFiles)
                    {
                        ListViewItem item = new ListViewItem(imageFile);
                        item.SubItems.Add("...");

                        sourceFilesListView.Items.Add(item);
                    }

                    statusBarItem_numberOfImagesLoaded.Text = $"No. of Images Loaded: {imageFiles.Count.ToString()}";
                    statusBarItem_numberOfCompletedItems.Text = $"0";

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
            ocrHelper.constructOutputDirectoryStructure();
            
            Language ocrLang = ocrHelper.getOcrLanguage();
            string tessdataLoc = Properties.Settings.Default.TessdataLocation;
            int concurrentOCRJobs = Properties.Settings.Default.ConcurrentOCRJobs;
            string tileSize = Properties.Settings.Default.TileSize;

            ocrHelper.testOCROutputQueue();


            //CancellationTokenSource cts = new CancellationTokenSource();

            //await ocr.processOCRQueue(ocrLang, tessdataLoc, concurrentOCRJobs, tileSize, cts.Token);

            //ocrHelper.startOver();
        }

        private void cancelOCRButton_Click(object sender, EventArgs e)
        {

        }
        private void startOverButton_Click(object sender, EventArgs e)
        {
            resetMainFormControls();
            resetMainFormStatusBar();

            ocrHelper.ocrOutputInfoList.Clear();
            ocrHelper.ocrOutputInfoQueue.Clear();
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


    }
}
