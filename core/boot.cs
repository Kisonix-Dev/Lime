using System;
namespace Lime.core
{
  class Boot
  {
    public void Function_boot_system()
    {
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine($"Загрузка системы...");
      Console.ResetColor();
    }
  }
}