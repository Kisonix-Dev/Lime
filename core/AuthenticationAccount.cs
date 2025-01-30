using System;
using Newtonsoft.Json;
namespace Lime.core
{
  public static class AuthenticationAccount
  {
    public static void Authentication(string filePath)
    {
      List<User> users = LoadUsers(filePath);
      while (true)
      {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Вход в учётную запись пользователя");
        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"Введите имя: ");
        Console.ResetColor();
        string username = Console.ReadLine()!;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"Введите пароль: ");
        Console.ResetColor();
        string password = Console.ReadLine()!;
        var user = users.Find(u => u.Username == username && u.Password == password);
        if (user != null)
        {
          Console.Clear();
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine($"Добро пожаловать, " + username + "!");
          Console.ResetColor();
          break;
        }
        else
        {
          Console.Clear();
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine($"Не правильный логин или пароль, попробуйте снова!");
          Console.ResetColor();
          Thread.Sleep(1000);
        }
      }
    }
    private static List<User> LoadUsers(string filePath)
    {
      if (File.Exists(filePath))
      {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>()!;
      }
      return new List<User>()!;
    }
  }
}