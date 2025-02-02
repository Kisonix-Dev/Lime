using System;
using Lime.colors;
namespace Lime.core
{
  class Interpreter
  {
    public class Command : Options
    {
      private readonly SystemInfo sysinfo;
      public Command()
      {
        sysinfo = new SystemInfo();
      }
      public void uname()
      {
        Colors.Green();
        Console.WriteLine($"Название ядра: {sysinfo.KernelName}");
        Console.ResetColor();
      }
      public void unameR()
      {
        base.unameR(sysinfo);
      }
      public void who()
      {
        var user = AuthenticationAccount.GetAuthenticatedUser();
        Colors.Green();
        Console.WriteLine($"Активный пользователь: {user!.Username}");
        Console.ResetColor();
      }
      public void exit()
      {
        Console.Clear();
        Colors.Yellow();
        Console.WriteLine($"Выход из системы!");
        Console.ResetColor();
        Thread.Sleep(1000);
      }
    }
    public class Options
    {
      public void unameR(SystemInfo sysinfo)
      {
        Colors.Green();
        Console.WriteLine($"Версия ядра: {sysinfo.KernelVersion}");
        Console.ResetColor();
      }
    }
  }
}