using System;
using System.Linq;

namespace Password
{


    class Password
    {
        static void Main()
        {
            string password;

            while (true)
            {
                Console.Write("Введите пароль (Минимум: 3 цифры, 1 буква, 1 спецсимвол): ");
                password = Console.ReadLine();

                string errorMessage;
                if (Validate(password, out errorMessage))
                {
                    Console.WriteLine("Пароль принят!");
                    break;
                }
                else
                {
                    Console.WriteLine($" {errorMessage}");
                    Console.WriteLine();
                }
            }
        }

        static bool Validate(string password, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrEmpty(password))
            {
                errorMessage = "Пароль не может быть пустым";
                return false;
            }
            

            int digitCount = password.Count(char.IsDigit);
            if (digitCount < 3)
            {
                errorMessage = $"Пароль должен содержать хотя бы 3 цифры (сейчас {digitCount})";
                return false;
            }

            int specialCharCount = password.Count(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));
            if (specialCharCount != 1)
            {
                errorMessage = $"Пароль должен содержать ровно 1 спецсимвол (сейчас {specialCharCount})";
                return false;
            }

            int letterCount = password.Count(char.IsLetter);
            if (letterCount < 1)
            {
                errorMessage = $"Пароль должен содержать хотя бы 1 букву (сейчас {letterCount})";
                return false;
            }

            return true;
        }
    }
}