using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ProcesarPDF;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace JPGCoordinates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool MouseClicked = false;
        
        private string path = @"C:\RPA\Comprobante_Transferencia_Itau.pdf";
        private string pathJPG = @"C:\RPA\ProcesarPDF\Pendientes\Temp.bmp";

        private Point ll;
        private Point ur;
        private System.Windows.Point PuntoFijo;
        private PDFClass pdfClass;
        
        public MainWindow()
        {
            pdfClass = new PDFClass(path);
            InitializeComponent();
            BitmapImage bitmap = new BitmapImage(); 
            bitmap.BeginInit();  
            bitmap.UriSource = new Uri(pathJPG);
            bitmap.EndInit();

            Imagen.Source = bitmap;

            PageWith.Content = "Width: " + pdfClass.PageSize().Width;
            PageHeight.Content = "Height: " + pdfClass.PageSize().Height;
        }

        private void Rectangle_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            PuntoFijo = e.GetPosition(Imagen);
            ll = new Point((int)PuntoFijo.X, (int)PuntoFijo.Y);
            Canvas.SetTop(Rectangle, ll.Y);
            Canvas.SetLeft(Rectangle, ll.X);
            Rectangle.Visibility = Visibility.Visible;
            MouseClicked = true;
        }

        private void Canvas_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            MouseClicked = false;
            var pos = e.GetPosition(Imagen);
            // var ur = new Point((int)pos.X, (int)pos.Y);
            MessageBox.Show(pdfClass.ExtractData(
                (float) Imagen.Source.Width,
                (float) Imagen.Source.Height,
                ll,
                ur));
                
        }

        private void Canvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            PosX.Content = "PosX: " +  e.GetPosition(Imagen).X;
            PosY.Content = "PosY: " + (e.GetPosition(Imagen).Y);
            
            ur.X = Convert.ToInt32(Canvas.GetLeft(Rectangle) + Rectangle.Width);
            ur.Y = Convert.ToInt32(Canvas.GetTop(Rectangle));
            RecADY.Content = "ll.Y: " + ll.Y;
            RecADX.Content = "ll.X: " + ll.X;

            ll.X = Convert.ToInt32(Canvas.GetLeft(Rectangle));
            ll.Y = Convert.ToInt32(Canvas.GetTop(Rectangle) + Rectangle.Height);
            RecBIY.Content = "ur.Y: " + ur.Y;
            RecBIX.Content = "ur.X: " + ur.X;

            if (MouseClicked)
            {
                var pos = e.GetPosition(Imagen);
                Point coordinate = new Point((int)pos.X, (int)pos.Y);
                
                
                
                if (coordinate.X > PuntoFijo.X) 
                {
                    Rectangle.Width = coordinate.X - Canvas.GetLeft(Rectangle);
                }

                if (coordinate.X < PuntoFijo.X)
                {
                    Rectangle.Width = Math.Abs(coordinate.X - PuntoFijo.X);
                    Canvas.SetLeft(Rectangle, coordinate.X);
                }

                if (coordinate.Y > PuntoFijo.Y)
                {
                    Rectangle.Height = coordinate.Y - PuntoFijo.Y;
                }

                if (coordinate.Y < PuntoFijo.Y)
                {
                    Rectangle.Height = Math.Abs(coordinate.Y - PuntoFijo.Y);
                    Canvas.SetTop(Rectangle, coordinate.Y);
                }
            }
        }
    }
}