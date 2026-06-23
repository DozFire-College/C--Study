using System.Windows;
using System.Windows.Media;

namespace ImageResizer
{
    public partial class MainWindow : Window
    {
        private ScaleTransform scaleTransform;

        public MainWindow()
        {
            InitializeComponent();

            sizeSlider.ValueChanged -= SizeSlider_ValueChanged;

            scaleTransform = new ScaleTransform();
            imageControl.RenderTransform = scaleTransform;
            imageControl.RenderTransformOrigin = new Point(0.5, 0.5);

            sizeSlider.Value = 100;
            sizeLabel.Content = "100%";
            sizeSlider.ValueChanged += SizeSlider_ValueChanged;
        }

        private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (scaleTransform == null || sizeLabel == null)
                return;

            double percentage = e.NewValue;
            double scale = percentage / 100.0;
            
            scaleTransform.ScaleX = scale;
           
            sizeLabel.Content = $"{percentage:F0}%";
        }
    }
}
