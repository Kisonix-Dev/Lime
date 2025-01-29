using System;
using System.Threading;
namespace Lime.core
{
  class Boot
  {
    public void Function_boot_system()
    {
      Console.Clear();
      int totalSteps = 50;
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"Загрузка LIME");
      Console.ResetColor();
      Console.WriteLine();
      Console.Write($"[");

      for (int i = 0; i <= totalSteps; i++)
      {
        Console.ForegroundColor = ConsoleColor.White;
        Console.ResetColor();

        int percent = (i * 100) / totalSteps;

        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write($"[{new string('█', i)}{new string('.', totalSteps - i)}] {percent}%");

        Thread.Sleep(300);
      }
      Console.WriteLine($"]");
      Console.WriteLine();
      Console.WriteLine($"Загрузка завершена!");
      Thread.Sleep(1000);
    }
  }
}