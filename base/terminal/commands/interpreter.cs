using System;
using NLua;
using KeraLua;
using Lime.colors;
namespace Lime.core
{
  //Interpreter for the Cadence terminal.
  class Interpreter
  {
    //Processing terminal commands.
    public class Commands : Options
    {
      private readonly SystemInfo sysinfo;
      public Commands()
      {
        sysinfo = new SystemInfo();
      }
      public void Uname()
      {
        Colors.Green();
        Console.WriteLine($"Название ядра: {sysinfo.KernelName}");
        Console.ResetColor();
      }
      public void UnameR()
      {
        base.UnameR(sysinfo);
      }
      /*public void unameRealese()
      {
      base.unameR(sysinfo);
      }*/
      public void Who()
      {
        var user = AuthenticationAccount.GetAuthenticatedUser();
        Colors.Green();
        Console.WriteLine($"Активный пользователь: {user!.Username}");
        Console.ResetColor();
      }
      public void Clear()
      {
        Console.Clear();
      }
      //Note:
      /*I know it's bad manners to duplicate code, but for now I've decided to leave it as is. (Temporarily).
      I'll create a separate class for the path later.*/
      public void UserAdd()
      {
        Console.Clear();
        Colors.Yellow();
        Console.WriteLine("Выход из текущего профиля...");
        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filePath = Path.Combine(homeDirectory, "LimeOS", "database", "account", "userdata.json");
        CreateNewAccount.Register(filePath);
      }
      public void Logout()
      {
        Console.Clear();
        Colors.Yellow();
        Console.WriteLine("Выход из текущего профиля...");
        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filePath = Path.Combine(homeDirectory, "LimeOS", "database", "account", "userdata.json");
        AuthenticationAccount.Authentication(filePath);
      }
      public void PowerOff()
      {
        Console.Clear();
        Colors.Yellow();
        Console.WriteLine("Выход из системы...");
        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
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
    //Processing options.
    public class Options : Arguments
    {
      public void UnameR(SystemInfo sysinfo)
      {
        Colors.Green();
        Console.WriteLine($"Версия ядра: {sysinfo.KernelVersion}");
        Console.ResetColor();
      }
      /*public void unameRealese(SystemInfo sysinfo)
      {
      Colors.Green();
      Console.WriteLine($"Версия ядра: {sysinfo.KernelVersion}");
      Console.ResetColor();
      }*/
    }
    //Processing arguments.
    public class Arguments
    {
      public void MkdirA()
      {
        Colors.Cyan();
        Console.Write("Название новой директории > ");
        Console.ResetColor();
        string NameDirectory = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(NameDirectory))
        {
          Console.Clear();
          Colors.Red();
          Console.WriteLine("Пропущен аргумент. Директория не создана!");
          Colors.Gray();
          Console.WriteLine("Введите 'mkdir --help' для подробной информации по этой команде.\n");
          Console.ResetColor();
          Thread.Sleep(1000);
        }
        else
        {
          string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
          string newDirectoryPath = Path.Combine(homeDirectory, "LimeOS", "main", "root", "home", "user", NameDirectory);

          Directory.CreateDirectory(newDirectoryPath);
          Console.Clear();
          Colors.Green();
          Console.WriteLine($"Новая директория {NameDirectory} успешно создана!");
        }
      }
    }
  }
}