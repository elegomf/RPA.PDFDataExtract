using System;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace PDFExtract
{
    public class ExtractDataFromCoordinates
    {
        private string Path;
        public PdfReader pdfReader;
            
        StringBuilder text = new StringBuilder();
        
        public ExtractDataFromCoordinates(string _path)
        {
            if (File.Exists(_path))
            {
                pdfReader = new PdfReader(_path);
            }
        }

        public string ExtractByCoordinate()
        {
            ITextExtractionStrategy strategy;
            
            Rectangle rectangle = new Rectangle(320, 785-250, 368, 799-250);
            // Rectangle rectangle = new Rectangle(447, 934-250, 678, 951); -> Ok
            RenderFilter filter = new RegionTextRenderFilter(rectangle);
            strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
            
            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            {
                Console.WriteLine(PdfTextExtractor.GetTextFromPage(pdfReader, page,strategy));
            }

            return null;
        }
    }
}