using FilterControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageFilterTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var b = new Bitmap(@"c:\share\1425539750793.png");

            using (MemoryStream memory = new MemoryStream())
            {
                //var bitmap = FilterManager.CopyAsGrayscale(b);
                //var bitmap = FilterManager.CopyWithTransparency(b);
                //var bitmap = FilterManager.CopyAsSepiaTone(b);
                //var bitmap = FilterManager.Cartoon(b);
                //var bitmap = FilterManager.FuzzyEdgeBlurFilter(b);
                //var bitmap = FilterManager.MedianFilter(b, 5);
                //var bitmap = FilterManager.AdjustBrightness(b, 50);
                var bitmap = FilterManager.GaussianBlur(b);

                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                xImage.Source = bitmapImage;
            }
        }
    }
}