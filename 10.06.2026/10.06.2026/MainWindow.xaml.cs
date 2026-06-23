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

            // Временно отписываемся от события, чтобы избежать преждевременных вызовов
            sizeSlider.ValueChanged -= SizeSlider_ValueChanged;

            // Создаем ScaleTransform для изображения
            scaleTransform = new ScaleTransform();
            imageControl.RenderTransform = scaleTransform;
            imageControl.RenderTransformOrigin = new Point(0.5, 0.5);

            // Устанавливаем начальное значение слайдера
            sizeSlider.Value = 100;

            // Обновляем Label
            sizeLabel.Content = "100%";

            // Теперь подписываемся на событие
            sizeSlider.ValueChanged += SizeSlider_ValueChanged;
        }

        private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Дополнительная проверка на null (на всякий случай)
            if (scaleTransform == null || sizeLabel == null)
                return;

            double percentage = e.NewValue;
            double scale = percentage / 100.0;
            
            scaleTransform.ScaleX = scale;
           
            sizeLabel.Content = $"{percentage:F0}%";
        }
    }
}