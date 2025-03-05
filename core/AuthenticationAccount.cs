using System;
using Lime.colors;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
namespace Lime.core
{
  //Authorize the user account.
  public static class AuthenticationAccount
  {
    private static User? authenticatedUser;
    public static void Authentication(string filePath)
    {
      Console.CursorVisible = true;

      List<User> users = LoadUsers(filePath);

      if (users == null || users.Count == 0)
      {
        Console.CursorVisible = false;
        Console.Clear();
        Colors.Red();
        Console.WriteLine("Не удалось загрузить пользователей или список пуст!");
        Console.ResetColor();
        Thread.Sleep(2000);
        CreateNewAccount.Register(filePath);
        return;
      }

      while (true)
      {
        string username;
        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Yellow();
          Console.WriteLine("Вход в учётную запись пользователя\n");
          Console.ResetColor();

          Colors.Blue();
          Console.Write("Введите имя > ");
          Console.ResetColor();

          username = Console.ReadLine()!;

          if (string.IsNullOrWhiteSpace(username))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Поле для ввода не должно быть пустым!");
            Thread.Sleep(2000);
            Console.Clear();
          }
          else
          {
            Thread.Sleep(300);
            break;
          }
        }

        string password;
        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Yellow();
          Console.WriteLine("Вход в учётную запись пользователя\n");
          Console.ResetColor();

          Colors.Blue();
          Console.Write("Введите пароль > ");
          Console.ResetColor();

          password = Console.ReadLine()!;

          if (string.IsNullOrWhiteSpace(password))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Поле для ввода не должно быть пустым!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
          }
          else
          {
            Thread.Sleep(300);
            break;
          }
        }

        string hashedPassword = HashPassword(password);
        authenticatedUser = users.Find(u => u.Username == username && u.Password == hashedPassword);

        if (authenticatedUser != null)
        {
          Console.CursorVisible = false;
          Console.Clear();
          Colors.Green();
          Console.WriteLine($"Добро пожаловать, {authenticatedUser.Username}!");
          Console.ResetColor();
          Thread.Sleep(1500);
          Console.Clear();

          Colors.Gray();
          Console.WriteLine("Терминал Cadence PC-0.\nВведите команду - 'help' для подробной информации о командах.\n");
          Console.ResetColor();
          break;
        }
        else
        {
          Console.CursorVisible = false;
          Console.Clear();
          Colors.Red();
          Console.WriteLine("Не правильный логин или пароль, попробуйте снова!");
          Console.ResetColor();
          Thread.Sleep(1500);
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
    //Encrypt the user account password.
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