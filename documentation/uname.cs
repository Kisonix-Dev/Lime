using System;
using Lime.colors;
namespace Lime.Core
{
  //Command help
  class Uname
  {
    public void UnameR()
    {
      Console.Clear();
      Console.WriteLine("Использование: [Command], [Option], [Arguments]");
      Colors.Yellow();
      Console.WriteLine("Аргументы не всегда обязательны к использованию.\n");
      Console.ResetColor();
      //System information
      Console.WriteLine("uname -r, --realese - (Версия ядра операционной системы.)\n");
    }
  }
}