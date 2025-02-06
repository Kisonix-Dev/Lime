using System;
using System.Threading;
using Lime.colors;
namespace Lime.core
{
  //Loading screen.
  class Boot
  {
    public void FunctionBootSystem()
    {
      Console.CursorVisible = false;
      Console.Clear();
      int totalSteps = 50;
      Logo.PrintLogo();
      Console.Write("\n[");

      for (int i = 0; i <= totalSteps; i++)
      {
        Colors.White();
        Console.ResetColor();

        int percent = (i * 100) / totalSteps;

        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write($"[{new string('█', i)}{new string('.', totalSteps - i)}] {percent}%");

        Thread.Sleep(300);
      }
      Console.WriteLine("]");
      Console.WriteLine("\nЗагрузка завершена!");
      Thread.Sleep(1000);
    }
  }
}