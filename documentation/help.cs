using System;
using System.Drawing;
using Lime.colors;
namespace Lime.core
{
  class Help
  {
    public void DocHelp()
    {
      Console.CursorVisible = false;
      Console.Clear();
      Console.WriteLine("Использование: [Команда], [Опция], [Аргумент]");
      Colors.Yellow();
      Console.WriteLine("Аргументы не всегда обязательны к использованию.\n");
      Console.ResetColor();
      Console.WriteLine("help     -Команда отображает помощь по всем командам.");
    }
  }
}