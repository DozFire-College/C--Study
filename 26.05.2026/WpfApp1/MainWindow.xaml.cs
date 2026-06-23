using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private readonly Brush[] colors = new Brush[]
        {
            Brushes.Red,
            Brushes.Orange,
            Brushes.Yellow,
            Brushes.Green,
            Brushes.Blue,
            Brushes.Indigo,
            Brushes.Violet,
            Brushes.Purple,
            Brushes.Cyan,
            Brushes.Magenta
        };

     
   
        private int currentColorIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
     
            ColorLabel.Foreground = colors[currentColorIndex];
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();               
            }
            else
            {
                timer.Start();
            }
        }
    }
}