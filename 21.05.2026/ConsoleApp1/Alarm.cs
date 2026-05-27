using System;
using System.Threading;
public class AlarmEventArgs : EventArgs
    {
        public DateTime AlarmTime { get; }
        public string Message { get; }

        public AlarmEventArgs(DateTime alarmTime, string message = "")
        {
            AlarmTime = alarmTime;
            Message = message;
        }
    }

   
    public class AlarmClock
    {
        public event EventHandler<AlarmEventArgs> AlarmRang;

        private int _alarmHours;
        private int _alarmMinutes;
        private int _alarmSeconds;
        private bool _isAlarmSet;
        private bool _isRunning;

        public void SetAlarm(int hours, int minutes, int seconds = 0)
        {
            _alarmHours = hours;
            _alarmMinutes = minutes;
            _alarmSeconds = seconds;
            _isAlarmSet = true;
            Console.WriteLine($"Будильник установлен на {hours}:{minutes}:{seconds}");
        }

        public void Start()
        {
            if (!_isAlarmSet)
            {
                Console.WriteLine("Сначала установите время будильника!");
                return;
            }

            _isRunning = true;
            Console.WriteLine("Будильник запущен. Ожидание...");

            while (_isRunning)
            {
                DateTime now = DateTime.Now;
                
                Console.Write($"\rТекущее время: {now:HH:mm:ss} | Будильник: {_alarmHours}:{_alarmMinutes}:{_alarmSeconds}   ");

                if (now.Hour == _alarmHours && 
                    now.Minute == _alarmMinutes && 
                    now.Second == _alarmSeconds)
                {
                    OnAlarmRang(new AlarmEventArgs(now));
                    _isAlarmSet = false;
                    break;
                }

                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }

        protected virtual void OnAlarmRang(AlarmEventArgs e)
        {
            AlarmRang?.Invoke(this, e);
        }
    }

class Program
{
   
    

    static void Main(string[] args)
    {
        Console.WriteLine("=== Будильник ===");

        
        AlarmClock alarm = new AlarmClock();

        
        alarm.AlarmRang += (sender, e) =>
        {
            Console.WriteLine("*** БУДИЛЬНИК! ***");
            Console.WriteLine($"Время: {e.AlarmTime:HH:mm:ss}");
        };

       
        Console.WriteLine("Установка времени будильника:");
        Console.Write("Введите часы (0-23): ");
        int hours = int.Parse(Console.ReadLine());
        Console.Write("Введите минуты (0-59): ");
        int minutes = int.Parse(Console.ReadLine());
        Console.Write("Введите секунды (0-59): ");
        int seconds = int.Parse(Console.ReadLine());

        alarm.SetAlarm(hours, minutes, seconds);

        Console.WriteLine("Нажмите Enter для запуска будильника...");
        Console.ReadLine();

        
        Thread alarmThread = new Thread(() => alarm.Start());
        alarmThread.Start();

        Console.WriteLine("Нажмите Enter для остановки будильника");
        Console.ReadLine();

        alarm.Stop();
        alarmThread.Join();
        
        Console.WriteLine("Будильник остановлен. Программа завершена.");
        Console.ReadKey();
    }
}