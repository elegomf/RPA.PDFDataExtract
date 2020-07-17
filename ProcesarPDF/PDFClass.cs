using System;
using System.Drawing;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Rectangle = iTextSharp.text.Rectangle;

namespace ProcesarPDF
{
    public class PDFClass
    {
        public PdfReader PdfReader;

        public PDFClass(string _path)
        {
            PdfReader = new PdfReader(_path);
        }

        public Rectangle PageSize(int page = 1)
        {
            return PdfReader.GetPageSize(page);
        }

        public string ExtractData(float UIWith, float UIHeight, Point ll, Point ur,
            int page = 1)
        {
            Console.WriteLine("Test");
            float MultX = PageSize().Width / UIWith;
            float MultY = PageSize().Height / UIHeight;

            ITextExtractionStrategy strategy;

            Rectangle rectangle = new Rectangle(ll.X * MultX, (UIHeight - ll.Y) * MultY, ur.X * MultX,(UIHeight - ur.Y) * MultY);
            // Rectangle rectangle = new Rectangle(447, 934-250, 678, 951); // -> Ok
            RenderFilter filter = new RegionTextRenderFilter(rectangle);
            strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
            // return "" + UIWith + " " + UIHeight + " " + ll.X + " " + ll.Y + " " + ur.X + " " + ur.Y;

            return PdfTextExtractor.GetTextFromPage(PdfReader, page, strategy);
        }
    }
}