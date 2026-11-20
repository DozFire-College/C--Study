using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Win32;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage bitmapImage1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Все файлы|*.*";

            if (openFileDialog1.ShowDialog() == true)
            {
                try
                {
                    bitmapImage1 = new BitmapImage();
                    bitmapImage1.BeginInit();
                    bitmapImage1.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage1.UriSource = new Uri(openFileDialog1.FileName);
                    bitmapImage1.EndInit();

                    textBox3.Text = bitmapImage1.PixelWidth.ToString();
                    textBox4.Text = bitmapImage1.PixelHeight.ToString();

                    button2_Click(sender, e);
                }
                catch (Exception ex)
                {
                    bitmapImage1 = null;
                    MessageBox.Show("Не удалось открыть файл: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (bitmapImage1 == null)
            {
                MessageBox.Show("Сначала откройте изображение");
                return;
            }

            double x, y, w, h;
            if (!double.TryParse(textBox1.Text, out x) || !double.TryParse(textBox2.Text, out y) ||
                !double.TryParse(textBox3.Text, out w) || !double.TryParse(textBox4.Text, out h))
            {
                MessageBox.Show("Введите числа");
                return;
            }

            if (w <= 0 || h <= 0)
            {
                MessageBox.Show("Ширина и высота должны быть больше нуля");
                return;
            }

            int width_main = (int)(border1.ActualWidth - border1.BorderThickness.Left - border1.BorderThickness.Right);
            int height_main = (int)(border1.ActualHeight - border1.BorderThickness.Top - border1.BorderThickness.Bottom);

            DrawingVisual drawingVisual1 = new DrawingVisual();
            using (DrawingContext drawingContext1 = drawingVisual1.RenderOpen())
            {
                drawingContext1.DrawRectangle(Brushes.White, null, new Rect(0, 0, width_main, height_main));
                drawingContext1.DrawImage(bitmapImage1, new Rect(x, y, w, h));
            }

            RenderTargetBitmap renderTargetBitmap1 = new RenderTargetBitmap(width_main, height_main, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap1.Render(drawingVisual1);
            image1.Source = renderTargetBitmap1;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            image1.Source = null;
        }
    }
}
