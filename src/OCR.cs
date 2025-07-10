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

        public int ocr(string sourceImageFileFullpath, string sourceImageFileName, string outputPdfFileFullPath, string outputAltoFileFullPath, string outputJp2FileFullPath, string tessdataLoc, Language ocrLang, string tileSize, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                //Perform OCR and produce PDF and Alto files:
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

                //Perform image compression and generate JP2 file:
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

                return 0;
            }
            catch (OperationCanceledException)
            {
                return 1;
            }
            catch (Exception ex)
            {
                return 2;
            }
        }
    }
}
