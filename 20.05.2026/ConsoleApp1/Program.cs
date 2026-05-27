using System;
namespace ConsoleApp1
{
   
public enum Month
{
    January = 1,
    February,
    March,
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    December
}

public enum Season
{
    Winter,
    Spring,
    Summer,
    Autumn
}

public static class SeasonHelper
{
    public static Season GetSeason(Month month)
    {
        switch (month)
        {
            case Month.December:
            case Month.January:
            case Month.February:
                return Season.Winter;
            case Month.March:
            case Month.April:
            case Month.May:
                return Season.Spring;
            case Month.June:
            case Month.July:
            case Month.August:
                return Season.Summer;
            case Month.September:
            case Month.October:
            case Month.November:
                return Season.Autumn;
            default:
                throw new ArgumentException("Неизвестный месяц");
        }
    }

    public static Month[] GetMonths(Season season)
    {
        switch (season)
        {
            case Season.Winter:
                return new Month[] { Month.December, Month.January, Month.February };
            case Season.Spring:
                return new Month[] { Month.March, Month.April, Month.May };
            case Season.Summer:
                return new Month[] { Month.June, Month.July, Month.August };
            case Season.Autumn:
                return new Month[] { Month.September, Month.October, Month.November };
            default:
                throw new ArgumentException("Неизвестный сезон");
        }
    }

    public static void PrintSeasonInfo(Season season)
    {
       
        string seasonName = season switch
        {
            Season.Winter => "Зима",
            Season.Spring => "Весна",
            Season.Summer => "Лето",
            Season.Autumn => "Осень",
            _ => "Неизвестный сезон"
        };

        Console.WriteLine($"Сезон: {seasonName}");
        Console.WriteLine("Месяцы:");

        Month[] months = GetMonths(season);
        string[] monthNames = { "Декабрь", "Январь", "Февраль", "Март", "Апрель", "Май", 
                                "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь" };

        foreach (Month month in months)
        {
            Console.WriteLine($"  - {monthNames[(int)month - 1]}");
        }
    }
}
public enum Permissions
{
    None = 0,
    Read = 1,
    Write = 2,
    Delete = 4,
    Admin = 8
}

public class User
{
    public string Name { get; set; }
    public Permissions Role { get; private set; }

    public User(string name, Permissions initialPermissions = Permissions.None)
    {
        Name = name;
        Role = initialPermissions;
    }

    public bool HasPermission(Permissions permission)
    {
        return (Role & permission) == permission;
    }
    public void Grant(Permissions permission)
    {
        Role |= permission;
    }

    public void Revoke(Permissions permission)
    {
        Role &= ~permission;
    }

    public void PrintPermissions()
    {
        Console.WriteLine($"Пользователь: {Name}");
        Console.WriteLine($"Права доступа (Role = {(int)Role}):");

        if (Role == Permissions.None)
        {
            Console.WriteLine("  Нет прав");
            return;
        }

        if (HasPermission(Permissions.Read))
            Console.WriteLine("  - Чтение (Read)");
        
        if (HasPermission(Permissions.Write))
            Console.WriteLine("  - Запись (Write)");
        
        if (HasPermission(Permissions.Delete))
            Console.WriteLine("  - Удаление (Delete)");
        
        if (HasPermission(Permissions.Admin))
            Console.WriteLine("  - Администратор (Admin)");
    }
}

internal class Program
{
   static void Main(string[] args)
   {
       
       Month month = Month.March;
       Season season = SeasonHelper.GetSeason(month);
       Console.WriteLine($"Месяц {month} относится к сезону {season}");
       
       Month[] summerMonths = SeasonHelper.GetMonths(Season.Summer);
       Console.WriteLine($"Летние месяцы: {string.Join(", ", summerMonths)}");
 
       Console.WriteLine("Информация о сезоне:");
       SeasonHelper.PrintSeasonInfo(Season.Winter);
       
   
       User user = new User("Иван Петров");
       Console.WriteLine("Начальное состояние:");
       user.PrintPermissions();

       Console.WriteLine("Добавление прав на чтение и запись:");
       user.Grant(Permissions.Read | Permissions.Write);
       user.PrintPermissions();

       Console.WriteLine($"Имеет право на чтение: {user.HasPermission(Permissions.Read)}");
       Console.WriteLine($"Имеет право на удаление: {user.HasPermission(Permissions.Delete)}");

       Console.WriteLine("Добавить права администратора:");
       user.Grant(Permissions.Admin);
       user.PrintPermissions();

       Console.WriteLine("\nОтзыв права на запись:");
       user.Revoke(Permissions.Write);
       user.PrintPermissions();
       
       Console.WriteLine("\nПользователь со всеми правами:");
       User admin = new User("Администратор", Permissions.Read | Permissions.Write | 
      Permissions.Delete | Permissions.Admin);
       admin.PrintPermissions();
      
   }
}

}