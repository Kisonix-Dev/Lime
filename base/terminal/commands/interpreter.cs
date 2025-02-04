using System;
using NLua;
using KeraLua;
using Lime.colors;
namespace Lime.core
{
  class Interpreter
  {
    public class Commands : Options
    {
      private readonly SystemInfo sysinfo;
      public Commands()
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
      //public void unameRealese()
      //{
      //base.unameR(sysinfo);
      //}
      public void who()
      {
        var user = AuthenticationAccount.GetAuthenticatedUser();
        Colors.Green();
        Console.WriteLine($"Активный пользователь: {user!.Username}");
        Console.ResetColor();
      }
      public void clear()
      {
        Console.Clear();
      }
      public void exit()
      {
        Console.Clear();
        Colors.Yellow();
        Console.WriteLine($"Выход из системы!");
        Console.ResetColor();
        Thread.Sleep(1000);
      }
      public void Test()
      {
        using (NLua.Lua lua = new())
        {
          lua.DoFile("scripts/test.lua");
          var Func = lua.GetFunction("add");
          var result = Func.Call();
          if (result.Length > 0)
          {
            Console.WriteLine($"Результат: {result[0]}");
          }
        }
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
      //public void unameRealese(SystemInfo sysinfo)
      //{
      //Colors.Green();
      //Console.WriteLine($"Версия ядра: {sysinfo.KernelVersion}");
      //Console.ResetColor();
      //}
    }
  }
}