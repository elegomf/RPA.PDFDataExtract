using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFPdfViewer;

namespace PDFViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string path = @"C:\RPA\Comprobante_Transferencia_Itau.pdf";
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}