using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesseractOCR.Enums;
using TesseractOCR.Renderers;

namespace NewspaperOCR.src
{
    internal class OCR
    {
        private MainForm mainForm;
        private LogForm logForm;
        private OptionsForm optionsForm;

        public OCR(MainForm _mainForm, LogForm _logForm, OptionsForm _optionsForm)
        {
            mainForm = _mainForm;
            logForm = _logForm;
            optionsForm = _optionsForm;
        }

        private async Task processOCRQueue(Language ocrLang, string tessdataLoc, int concurrentOCRJobs, string tileSize, CancellationToken ct)
        {
            Queue<OutputDirectoryStructure> directoryStructureQueue = new Queue<OutputDirectoryStructure>(directoryStructure);
            Dictionary<int, src.TaskStatus> concurrentJobsTracker = new Dictionary<int, src.TaskStatus>();
            Task ocrTask;

            int completedOcr = 0;
            DateTime batchStartTime = DateTime.Now;
            DateTime batchCompletionTime;
            TimeSpan batchProcessingTime;
            OutputDirectoryStructure item;

            mainForm.Invoke(() =>
            {
                mainForm.statusBarItem_numberOfCompletedItems.Text = completedOcr.ToString();
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
            }
            else
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
                    }
                    else
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
    }
}
