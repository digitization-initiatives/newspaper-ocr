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
        public bool validateIssueFolderNames()
        {
            Regex issueFolderNamePattern = new Regex(@"^[a-zA-Z0-9_-]+_\d{4}-\d{2}-\d{2}$");

            List<string> issueFoldersPaths = new List<string>();
            List<string> files = new List<string>();

            issueFoldersPaths.AddRange(Directory.GetDirectories(folderBrowserDialog.SelectedPath));
            files.AddRange(Directory.GetFiles(folderBrowserDialog.SelectedPath));

            int validFolders = issueFoldersPaths.Count;

            if (files.Count > 0)
            {
                logForm.appendTextsToLog($"The following invalid files found in \"{folderBrowserDialog.SelectedPath}\". Only issue folders are allowed.", logForm.LOG_TYPE_WARN);
                foreach (string file in files)
                {
                    logForm.appendTextsToLog($"Invalid file: \"{file}\"", logForm.LOG_TYPE_WARN);
                }
                return false;
            }

            if (issueFoldersPaths.Count == 0)
            {
                logForm.appendTextsToLog($"No Issues Found in \"{folderBrowserDialog.SelectedPath}\"", logForm.LOG_TYPE_WARN);
                return false;
            }
            else
            {
                foreach (string issueFolderPath in issueFoldersPaths)
                {
                    string issueFolderName = Path.GetFileName(issueFolderPath);

                    if (!issueFolderNamePattern.IsMatch(issueFolderName))
                    {
                        logForm.appendTextsToLog($"\"{issueFolderPath}\" is not a valid issue folder name", logForm.LOG_TYPE_WARN);
                        validFolders--;
                    }
                    else
                    {
                        logForm.appendTextsToLog($"\"{issueFolderPath}\" is a valid issue folder name", logForm.LOG_TYPE_INFO);
                    }
                }

                if (validFolders < issueFoldersPaths.Count)
                {
                    logForm.appendTextsToLog($"Some issue folder names in \"{folderBrowserDialog.SelectedPath}\" are invalid, please see log for details.", logForm.LOG_TYPE_WARN);
                    return false;
                }
                else return true;
            }
        }
        public void constructOutputDirectoryStructure()
        {
            string batchNameFolder = Path.GetFileName(folderBrowserTextBox.Text);

            foreach (ListViewItem imageFileListViewItem in sourceFilesListView.Items)
            {
                //Get the item index:
                int index = imageFileListViewItem.Index;

                //Construct issueDateFolder:
                string issueDateFolder = imageFileListViewItem.SubItems[0].Text;
                issueDateFolder = issueDateFolder.Replace(folderBrowserTextBox.Text, "");
                var segments = issueDateFolder.Split(Path.DirectorySeparatorChar);
                if (segments.Length > 0)
                {
                    issueDateFolder = segments[1];
                }

                //Extract imageFileName:
                string imageFileName = Path.GetFileName(imageFileListViewItem.SubItems[0].Text);

                DirectoryStructure directoryStructureItem = new DirectoryStructure(index, batchNameFolder, issueDateFolder, imageFileName, imageFileListViewItem.SubItems[0].Text, Properties.Settings.Default.OCROutputLocation);
                directoryStructure.Add(directoryStructureItem);
            }

            //if (logForm.verboseLogCheckBox.Checked)
            //{
            //    foreach (DirectoryStructure directoryStructureItem in directoryStructure)
            //    {

            //    }
            //}
        }
        public Language getOcrLanguage()
        {
            switch (Properties.Settings.Default.OCRLang)
            {
                case "eng":
                    return Language.English;
                case "spa":
                    return Language.SpanishCastilian;
                case "fra":
                    return Language.French;
                case "jpn":
                    return Language.Japanese;
                default:
                    return Language.English;
            }
        }

        private async Task processOCRQueue(Language ocrLang, string tessdataLoc, int concurrentOCRJobs, string tileSize, CancellationToken ct)
        {
            Queue<DirectoryStructure> directoryStructureQueue = new Queue<DirectoryStructure>(directoryStructure);
            Dictionary<int, src.TaskStatus> concurrentJobsTracker = new Dictionary<int, src.TaskStatus>();
            Task ocrTask;

            int completedOcr = 0;
            DateTime batchStartTime = DateTime.Now;
            DateTime batchCompletionTime;
            TimeSpan batchProcessingTime;
            DirectoryStructure item;

            this.Invoke(() =>
            {
                statusBarItem_numberOfCompletedItems.Text = completedOcr.ToString();
                logForm.appendTextsToLog($"OCR of this batch started at: {batchStartTime.ToString(@"hh\:mm\:ss")}.", logForm.LOG_TYPE_INFO);
            });

            if (concurrentJobsTracker.Count == 0)
            {
                for (int i = 0; i < concurrentOCRJobs; i++)
                {
                    ct.ThrowIfCancellationRequested();

                    if (directoryStructureQueue.Count != 0)
                    {
                        item = directoryStructureQueue.Dequeue();
                        ocrTask = Task.Run(async () =>
                        {
                            //await ocr(item.SourceImageFileFullPath, item.SourceImageFileName, item.OutputPdfFileFullPath, item.OutputAltoFileFullPath, item.OutputJp2ImageFileFullPath, tessdataLoc, ocrLang, tileSize);
                            await Task.Delay(10000);
                            Invoke(() =>
                            {
                                logForm.appendTextsToLog($"{item.SourceImageFileFullPath} OCR started.", logForm.LOG_TYPE_INFO);
                            });
                        });
                        src.TaskStatus ocrTaskStatus = new src.TaskStatus(ocrTask, item);
                        concurrentJobsTracker.Add(i, ocrTaskStatus);
                    }
                    else
                    {
                        break;
                    }
                }
            } else
            {
                for (int i = 0; i < concurrentOCRJobs; i++)
                {
                    ct.ThrowIfCancellationRequested();

                    if (concurrentJobsTracker[i].RunningTask.IsCompleted)
                    {
                        Invoke(() =>
                        {
                            ListViewItem imageFileListViewItem = sourceFilesListView.Items[concurrentJobsTracker[i].Item.Index];
                            imageFileListViewItem.SubItems[1].Text = "Finished";
                        });

                        if (directoryStructureQueue.Count != 0)
                        {
                            item = directoryStructureQueue.Dequeue();
                            ocrTask = Task.Run(async () =>
                            {
                                //await ocr(item.SourceImageFileFullPath, item.SourceImageFileName, item.OutputPdfFileFullPath, item.OutputAltoFileFullPath, item.OutputJp2ImageFileFullPath, tessdataLoc, ocrLang, tileSize);
                                await Task.Delay(10000);
                                Invoke(() =>
                                {
                                    logForm.appendTextsToLog($"{item.SourceImageFileFullPath} OCR started.", logForm.LOG_TYPE_INFO);
                                });
                            });
                            concurrentJobsTracker[i].RunningTask = ocrTask;
                        }
                    }
                    else if (!concurrentJobsTracker[i].RunningTask.IsCompleted)
                    {
                        Invoke(() =>
                        {
                            ListViewItem imageFileListViewItem = sourceFilesListView.Items[concurrentJobsTracker[i].Item.Index];

                            if (imageFileListViewItem.SubItems[1].Text.Length < 8)
                            {
                                imageFileListViewItem.SubItems[1].Text += "..";
                            }
                            else
                            {
                                imageFileListViewItem.SubItems[1].Text = "...";
                            }

                            logForm.appendTextsToLog(concurrentJobsTracker[i].Item.SourceImageFileFullPath + " is being OCR'd " + imageFileListViewItem.SubItems[1].Text, logForm.LOG_TYPE_INFO);
                            //imageFileListViewItem.SubItems[1].Text = imageFileListViewItem.SubItems[1].Text;
                        });
                    } else
                    {
                        Invoke(() =>
                        {
                            ListViewItem imageFileListViewItem = sourceFilesListView.Items[concurrentJobsTracker[i].Item.Index];

                            imageFileListViewItem.SubItems[1].Text = "Faulted";

                            logForm.appendTextsToLog(concurrentJobsTracker[i].Item.SourceImageFileFullPath + " is not OCR'd.", logForm.LOG_TYPE_INFO);
                        });
                    }
                }
            }
        }

        private async Task ocr(string sourceImageFileFullpath, string sourceImageFileName, string outputPdfFileFullPath, string outputAltoFileFullPath, string outputJp2FileFullPath, string tessdataLoc, Language ocrLang, string tileSize)
        {

            using (var engine = new TesseractOCR.Engine(tessdataLoc, Language.English, EngineMode.LstmOnly))
            {
                using (var img = TesseractOCR.Pix.Image.LoadFromFile(sourceImageFileFullpath))
                {
                    using (var page = engine.Process(img))
                    {
                        using (var pdfRenderer = new PdfResult(outputPdfFileFullPath, tessdataLoc, false))
                        {
                            pdfRenderer.BeginDocument(sourceImageFileName);
                            pdfRenderer.AddPage(page);
                        }

                        using (var altoRenderer = new AltoResult(outputAltoFileFullPath))
                        {
                            altoRenderer.BeginDocument(sourceImageFileName);
                            altoRenderer.AddPage(page);
                        }
                    }
                }
            }

            using (var sourceImage = new MagickImage(sourceImageFileFullpath))
            {
                sourceImage.Format = MagickFormat.Jp2;
                sourceImage.Settings.Compression = CompressionMethod.JPEG2000;

                //sourceImage.ColorSpace = ColorSpace.Gray;
                //sourceImage.Settings.SetDefine(MagickFormat.Jp2, "number-resolutions", 5);
                //sourceImage.Settings.SetDefine(MagickFormat.Jp2, "Quality", "20,40,60,80");
                //sourceImage.Settings.SetDefine(MagickFormat.Jp2, "rate", "20,10,5,2,1");
                sourceImage.Settings.SetDefine(MagickFormat.Jp2, "progression-order", "RLCP");
                sourceImage.Quality = 40;

                string tiledJp2FileFullPath = outputJp2FileFullPath + tileSize;

                sourceImage.Write(tiledJp2FileFullPath);

                if (File.Exists(outputJp2FileFullPath))
                {
                    File.Delete(outputJp2FileFullPath);
                }

                File.Move(tiledJp2FileFullPath, outputJp2FileFullPath);
            }
        }


        private void startOver()
        {
            // Reset MainForm UI:
            folderBrowserTextBox.Text = String.Empty;
            folderBrowserDialog.SelectedPath = String.Empty;
            loadImagesButton.Enabled = false;

            sourceFilesListView.Items.Clear();
            sourceFilesListView_filenameCol.Width = sourceFilesListView.Width - 150;

            beginOCRButton.Enabled = false;

            statusBarItem_numberOfImagesLoaded.Text = "No Image Files Loaded";
            statusBarItem_numberOfCompletedItems.Text = "-";

            resetStatusBar();

            // Reset data structures :
            directoryStructure.Clear();
        }

        public void resetStatusBar()
        {
            statusBarItem_numberOfImagesLoaded.Text = "No Image Files Loaded";
            statusBarItem_numberOfCompletedItems.Text = "-";
        }

        public void updateStatusBar(string status, string message)
        {
            statusBarItem_numberOfImagesLoaded.Text = status;
            statusBarItem_numberOfCompletedItems.Text = message;
        }
        private void imageFilesListView_SizeChanged(object sender, EventArgs e)
        {
            sourceFilesListView_filenameCol.Width = sourceFilesListView.Width - 150;
        }

        #endregion

        private void folderBrowserButton_Click(object sender, EventArgs e)
        {
            folderBrowserTextBox.Text = String.Empty;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderBrowserTextBox.Text = folderBrowserDialog.SelectedPath;
                loadImagesButton.Enabled = true;
            }
            else
            {
                folderBrowserTextBox.Text = String.Empty;
                folderBrowserDialog.SelectedPath = String.Empty;
                loadImagesButton.Enabled = false;
            }
        }
        private void loadImagesButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.SelectedPath != String.Empty)
            {
                if (validateIssueFolderNames())
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
                }
                else
                {
                    MessageBox.Show($"\"{folderBrowserDialog.SelectedPath}\" is empty or contains invalid issue folders, see log for details.", "Invalid Issue Folders Found!");
                    logForm.appendTextsToLog($"\"{folderBrowserDialog.SelectedPath}\" contains invalid issue folders, validation faild.", logForm.LOG_TYPE_WARN);
                }
            }
        }

        private async void beginOCRButton_Click(object sender, EventArgs e)
        {
            constructOutputDirectoryStructure();

            Language ocrLang = getOcrLanguage();
            string tessdataLoc = Properties.Settings.Default.TessdataLocation;
            int concurrentOCRJobs = Properties.Settings.Default.ConcurrentOCRJobs;
            string tileSize = Properties.Settings.Default.TileSize;

            CancellationTokenSource cts = new CancellationTokenSource();

            await processOCRQueue(ocrLang, tessdataLoc, concurrentOCRJobs, tileSize, cts.Token);

            startOver();
        }

        private void cancelOCRButton_Click(object sender, EventArgs e)
        {

        }
        private void startOverButton_Click(object sender, EventArgs e)
        {
            startOver();
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
