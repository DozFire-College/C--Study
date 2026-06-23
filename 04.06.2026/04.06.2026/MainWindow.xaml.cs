using System;
using System.Windows;
using System.Windows.Threading;

namespace ProgressBarApp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private int currentValue = 0;
        private const int MaxValue = 100;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); 
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentValue++;

  
            progressBar.Value = currentValue;

            if (currentValue >= MaxValue)
            {
                timer.Stop();
                ShowCongratulations();
            }
        }

        private void ShowCongratulations()
        {
            congratulationsPanel.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromSeconds(0.01);
            timer.Tick += Timer_Tick;
        }
    }
}