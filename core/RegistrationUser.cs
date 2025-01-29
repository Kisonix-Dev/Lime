using System;
using System.Threading;
using Newtonsoft.Json;
namespace Lime.core
{
  public class User
  {
    public string? Name { get; set; }
    public string? Password { get; set; }
  }
  class Register
  {
    public static void RegistrationUser()
    {
      Thread.Sleep(500);
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine($"Создание учётной записи пользователя");
      Console.ResetColor();
      Console.WriteLine();

      User user = new User()!;

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write($"Введите имя пользователя: ");
      Console.ResetColor();
      user.Name = Console.ReadLine()!;
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write($"Введите пароль: ");
      Console.ResetColor();
      user.Password = Console.ReadLine()!;

      string projectFolderPath = Directory.GetCurrentDirectory();

      string logfolderPath = Path.Combine(projectFolderPath, "log/temporarily");

      if (!Directory.Exists(logfolderPath))
      {
        Directory.CreateDirectory(logfolderPath);
      }

      string filePath = Path.Combine(logfolderPath, "userdata.json");

      string json = JsonConvert.SerializeObject(user, Formatting.Indented);
      File.WriteAllText(filePath, json);

      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"Учётная запись успешно создана!");
      Console.ResetColor();

      Thread.Sleep(1000);
    }
  }
}