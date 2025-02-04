using System;
using Lime.colors;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
namespace Lime.core
{
  public static class AuthenticationAccount
  {
    private static User? authenticatedUser;
    public static void Authentication(string filePath)
    {
      List<User> users = LoadUsers(filePath);

      if (users == null || users.Count == 0)
      {
        Console.WriteLine("Не удалось загрузить пользователей или список пуст.");
        return;
      }

      while (true)
      {
        Console.Clear();
        Colors.Yellow();
        Console.WriteLine($"Вход в учётную запись пользователя");
        Console.ResetColor();
        Console.WriteLine();

        Colors.Blue();
        Console.Write($"Введите имя: ");
        Console.ResetColor();
        string username = Console.ReadLine()!;

        Colors.Blue();
        Console.Write($"Введите пароль: ");
        Console.ResetColor();
        string password = Console.ReadLine()!;

        string hashedPassword = HashPassword(password);
        authenticatedUser = users.Find(u => u.Username == username && u.Password == hashedPassword);

        if (authenticatedUser != null)
        {
          Console.Clear();
          Colors.Green();
          Console.WriteLine($"Добро пожаловать, {authenticatedUser.Username}!");
          Console.ResetColor();
          Thread.Sleep(1500);
          Console.Clear();

          Colors.Gray();
          Console.WriteLine($"Терминал Cadence PC-0 \n Здесь вы сможете отправлять команды и получать соответствующий результат.");
          Console.WriteLine($"Введите команду - help для подробной информации о командах.");
          Console.ResetColor();
          Console.WriteLine();
          break;
        }
        else
        {
          Console.Clear();
          Colors.Red();
          Console.WriteLine($"Не правильный логин или пароль, попробуйте снова!");
          Console.ResetColor();
          Thread.Sleep(1000);
        }
      }
    }
    public static User? GetAuthenticatedUser()
    {
      return authenticatedUser;
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
    private static string HashPassword(string password)
    {
      using (var sha256 = SHA256.Create())
      {
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        StringBuilder builder = new StringBuilder()!;
        foreach (var b in bytes)
        {
          builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
      }
    }
  }
}