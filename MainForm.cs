using System.Reflection;

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

        private void imageFilesListView_SizeChanged(object sender, EventArgs e)
        {
            filenameCol.Width = imageFilesListView.Width - 150;

        }

        public void setDefaultOptions()
        {
            Properties.Settings.Default.TessdataLocation = Path.GetFullPath(".") + "\\tessdata";
            Properties.Settings.Default.OCROutputLocation = Path.GetFullPath(".") + "\\output";
            Properties.Settings.Default.ConcurrentOCRJobs = 1;
            Properties.Settings.Default.Save();
        }


        #endregion



        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            browseButton_TextBox.Text = String.Empty;

            if (browseButton_folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                browseButton_TextBox.Text = browseButton_folderBrowserDialog.SelectedPath;
            }
            else
            {
                browseButton_TextBox.Text = String.Empty;
            }
        }

        private void loadImagesButton_Click(object sender, EventArgs e)
        {
            if (browseButton_folderBrowserDialog.SelectedPath != null)
            {
                List<string> imageFiles = new List<string>();

                imageFiles.AddRange(Directory.GetFiles(browseButton_folderBrowserDialog.SelectedPath, "*.tif", SearchOption.AllDirectories));

                foreach (string imageFile in imageFiles)
                {
                    imageFilesListView.Items.Add(imageFile);
                }

                numberOfImages.Text = imageFiles.Count.ToString();
            }
        }

        private void viewLogsButton_Click(object sender, EventArgs e)
        {
            if (logForm.Visible)
            {
                logForm.Hide();
                viewLogsButton.Text = "View Logs";
            }
            else
            {
                logForm.Location = new Point(this.Location.X + this.Width + 10, this.Location.Y);
                logForm.Show();
                viewLogsButton.Text = "Hide Logs";
            }
        }

        private void beginOCRButton_Click(object sender, EventArgs e)
        {
            logForm.appendTextsToLog(Path.GetFullPath("."), logForm.LOG_TYPE_INFO);
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            optionsForm.Show();
        }
    }
}
