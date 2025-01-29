using System;
using System.Threading;
namespace Lime.core
{
  class Login
  {
    public static void LoginUser()
    {
      Thread.Sleep(500);
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine($"Создание учётной записи пользователя");
      Console.ResetColor();
      Console.WriteLine();
    }
  }
}