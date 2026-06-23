using System;
using System.Windows;
using System.Windows.Threading;

namespace ScreamerApp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private int countdown;
        private ScreamerWindow screamerWindow;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            countdown = 5;
            TimerText.Text = countdown.ToString();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            countdown--;
            TimerText.Text = countdown.ToString();

            if (countdown <= 0)
            {
                ShowScreamer();

                countdown = 5;
                TimerText.Text = countdown.ToString();
            }
        }

        private void ShowScreamer()
        {
            screamerWindow = new ScreamerWindow();
            screamerWindow.Show();
            var closeTimer = new DispatcherTimer();
            closeTimer.Interval = TimeSpan.FromSeconds(2);
            closeTimer.Tick += (s, args) =>
            {
                closeTimer.Stop();
                if (screamerWindow != null)
                {
                    screamerWindow.Close();
                    screamerWindow = null;
                }
            };
            closeTimer.Start();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            countdown = 5;
            TimerText.Text = countdown.ToString();
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
        }
    }
}