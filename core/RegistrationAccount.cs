using System;
using Lime.colors;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
namespace Lime.core
{
  //Create a user account.
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
      Console.CursorVisible = true;

      string username;
      while (true)
      {
        Console.CursorVisible = true;
        Console.Clear();
        Colors.Yellow();
        Console.WriteLine("Создание учётной записи пользователя\n");
        Console.ResetColor();

        Colors.Blue();
        Console.Write("Введите имя: ");
        Console.ResetColor();
        username = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(username))
        {
          Console.CursorVisible = false;
          Colors.Red();
          Console.WriteLine("Поле для ввода не должно быть пустым!");
          Thread.Sleep(1000);
          Console.Clear();
        }
        else
        {
          break;
        }
      }

      string password;
      while (true)
      {
        Console.CursorVisible = true;
        Console.Clear();
        Colors.Yellow();
        Console.WriteLine("Создание учётной записи пользователя\n");
        Console.ResetColor();

        Colors.Blue();
        Console.Write("Введите пароль: ");
        Console.ResetColor();
        password = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(password))
        {
          Console.CursorVisible = false;
          Colors.Red();
          Console.WriteLine("Поле для ввода не должно быть пустым!");
          Console.ResetColor();
          Thread.Sleep(1000);
          Console.Clear();
        }
        else
        {
          break;
        }
      }

      var users = LoadUsers(filePath);
      int newId = users.Count > 0 ? users[^1].Id + 1 : 1;

      string hashedPassword = HashPassword(password);
      users.Add(new User { Id = newId, Username = username, Password = hashedPassword });
      File.WriteAllText(filePath, JsonConvert.SerializeObject(users, Formatting.Indented));

      Console.CursorVisible = false;
      Console.Clear();
      Colors.Green();
      Console.WriteLine("Учётная запись создана.");
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
    //Encrypt the user account password.
    private static string HashPassword(string password)
    {
      using (var sha256 = SHA256.Create())
      {
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        StringBuilder builder = new StringBuilder();
        foreach (var b in bytes)
        {
          builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
      }
    }
  }
}
