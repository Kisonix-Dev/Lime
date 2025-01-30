using System;
using Newtonsoft.Json;
using System.Threading;
namespace Lime.core
{
  public class User
  {
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
  }
  public static class CreateNewAccount
  {
    public static void Register(string filePath)
    {
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine($"Создание учётной записи пользователя");
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

      var users = LoadUsers(filePath);
      int newId = users.Count > 0 ? users[^1].Id + 1 : 1;

      users.Add(new User { Id = newId, Username = username, Password = password });
      File.WriteAllText(filePath, JsonConvert.SerializeObject(users, Formatting.Indented));

      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"Учётная азпись создана.");
      Console.ResetColor();
      Thread.Sleep(1700);
      AuthenticationAccount.Authentication(filePath);
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