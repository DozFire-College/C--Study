using System;
using System.Threading;
using System.Threading.Tasks;

namespace Timer
{


    public class Timer
    {

        public void TimerVoid(Action action, int seconds)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (seconds < 0)
                throw new ArgumentException("Время не может быть отрицательным", nameof(seconds));

            Task.Delay(seconds * 1000).ContinueWith(_ => action());
        }


    }

    class Programmm
    {
        static void Mainm()
        {
            var timer = new Timer();
            Console.WriteLine("Таймер запущен.");
            timer.TimerVoid(() => { Console.WriteLine("Действие выполнено через 3 секунды"); }, 3);
        }
    }
}