using System;

namespace PDFExtract
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string path = @"C:\RPA\Comprobante_Transferencia_Itau.pdf";
            
            ExtractDataFromCoordinates extractDataFromCoordinates = new ExtractDataFromCoordinates(path);
            var coor = extractDataFromCoordinates.pdfReader.GetPageSize(1);
            Console.WriteLine(coor.Height + " " + coor.Width);
            
            extractDataFromCoordinates.ExtractByCoordinate();
        }
    }
}