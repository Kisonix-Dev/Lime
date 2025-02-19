using System;
using NLua;
using KeraLua;
using Lime.colors;
using Lime.Core;
using WhiteText;
using Lime.app.calculator;
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
      public void UnameRHelp()
      {
        Uname uname = new Uname()!;
        uname.UnameR();
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
      public void UserAdd()
      {
        Console.CursorVisible = false;
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
      public void Clear()
      {
        Console.Clear();
      }
      public void Help()
      {
        Help help = new Help()!;
        help.DocHelp();
      }
      //Note:
      /*I know it's bad manners to duplicate code, but for now I've decided to leave it as is. (Temporarily).
      I'll create a separate class for the path later.*/
      public void Logout()
      {
        Console.CursorVisible = false;
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
      public void Date()
      {
        DateTime CurrentDate = DateTime.Now;
        Console.WriteLine($"Текущая дата и время: {CurrentDate}");
      }
      public void PowerOff()
      {
        Console.CursorVisible = false;
        Console.Clear();
        Colors.Yellow();
        Console.WriteLine("Выход из системы...");
        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
        Console.CursorVisible = true;
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
      //SoftWare.
      public void WhiteText()
      {
        //TextEditor texteditor = new TextEditor()!;
        //texteditor.Editor();

        WhiteText whitetext = new WhiteText()!;
        whitetext.WhiteTextMenu();
      }
      public void Calc()
      {
        Calculator calculator = new Calculator();
        calculator.Calc_Menu();
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
          string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
          string NewDirectoryPath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user", NameDirectory);

          Directory.CreateDirectory(NewDirectoryPath);
          Console.Clear();
          Colors.Green();
          Console.WriteLine($"Новая директория {NameDirectory} успешно создана!");
        }
      }
      public void Touch()
      {
        Console.Clear();

        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string NewDirectoryPath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        Colors.Blue();
        Console.Write("Введите имя файла > ");
        Console.ResetColor();

        string FileName = Console.ReadLine()!;
        string FullPath = Path.Combine(NewDirectoryPath, FileName);

        try
        {
          if (!Directory.Exists(NewDirectoryPath))
          {
            Directory.CreateDirectory(NewDirectoryPath);
          }

          using (StreamWriter writer = new StreamWriter(FullPath))
          {
            Console.WriteLine("\nВведите текст (для завершения нажмите дважны на 'Enter'.):\n");
            string line;
            while ((line = Console.ReadLine()!) != "")
            {
              writer.WriteLine(line);
            }
          }
          Console.CursorVisible = false;
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine($"Файл '{FileName}' успешно создан по пути '{FullPath}");
          Console.ResetColor();
        }
        catch (Exception)
        {
          Console.CursorVisible = false;
          Console.Clear();
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine($"Пропущен аргумент. Ошибка при создании файла.");
          Console.ResetColor();
        }
        Console.CursorVisible = false;
        Colors.Green();
        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ResetColor();
        Console.ReadKey();
        Thread.Sleep(1000);
        Console.Clear();
      }
      public void Cat()
      {
        Console.Clear();

        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string NewDirectoryPath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        Colors.Blue();
        Console.Write("Введите имя файла > ");
        Console.ResetColor();

        string FileName = Console.ReadLine()!;
        string FullPath = Path.Combine(NewDirectoryPath, FileName);

        if (!File.Exists(FullPath))
        {
          Console.CursorVisible = false;
          Console.Clear();
          Colors.Red();
          Console.WriteLine($"Файл: '{FileName}' не найден!");
          Console.ResetColor();
          Thread.Sleep(1000);
          Console.Clear();
          return;
        }

        try
        {
          string content = File.ReadAllText(FullPath);
          Console.Clear();
          Console.WriteLine($"Содержимое файла: '{FileName}'\n");
          Console.WriteLine(content);
        }
        catch (Exception)
        {
          Console.Clear();
          Colors.Red();
          Console.WriteLine($"Пропущен аргумент. Ошибка при создании файла.");
          Console.ResetColor();
        }
        Console.CursorVisible = false;
        Colors.Green();
        Console.WriteLine("\nНажмите на клавишу 'Enter' для продолжения!");
        Console.ResetColor();
        Console.ReadKey();
        Thread.Sleep(1000);
        Console.Clear();
      }
      public void Rm()
      {
        Console.Clear();

        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string NewDirectoryPath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        Colors.Blue();
        Console.Write("Введите имя файла > ");
        Console.ResetColor();

        string FileName = Console.ReadLine()!;
        string FullPath = Path.Combine(NewDirectoryPath, FileName);

        if (!File.Exists(FullPath))
        {
          Console.CursorVisible = false;
          Console.Clear();
          Colors.Red();
          Console.WriteLine($"Файл: '{FileName}' не найден!");
          Console.ResetColor();
          Thread.Sleep(1000);
          Console.Clear();
          return;
        }

        try
        {
          Console.CursorVisible = false;
          Console.Clear();
          File.Delete(FullPath);
          Colors.Green();
          Console.WriteLine($"Файл: '{FileName}' успешно удалён.");
          Console.ResetColor();
          Thread.Sleep(1000);
          Console.Clear();
        }
        catch (Exception)
        {
          Console.Clear();
          Colors.Red();
          Console.WriteLine($"Пропущен аргумент. Ошибка при создании файла.");
          Colors.Red();
          Thread.Sleep(1000);
          Console.Clear();
        }
        Console.CursorVisible = false;
        Console.Clear();
        Colors.Green();
        Console.WriteLine("Нажмите на 'Enter' для продолжения!");
        Console.ResetColor();
        Console.ReadKey();
        Thread.Sleep(1000);
        Console.Clear();
      }
      //Even more code duplication. :D
      public void Addition()
      {
        try
        {
          Console.Clear();
          Colors.Yellow();
          Console.Write("Введите первое число > ");
          Console.ResetColor();
          int NumberOne = Convert.ToInt16(Console.ReadLine());

          Console.Clear();
          Colors.Yellow();
          Console.Write("Введите второе число > ");
          Console.ResetColor();
          int NumberTwo = Convert.ToInt16(Console.ReadLine());

          Console.Clear();
          Colors.Green();
          Console.WriteLine($"Результат: {NumberOne + NumberTwo}\n");
          Console.ResetColor();

          Console.CursorVisible = false;
          Console.Write("Нажмите на клавишу: 'Enter' для продолжения...");
          Console.ReadKey();
          Console.Clear();
        }
        catch (Exception)
        {
          Console.CursorVisible = false;
          Console.Clear();
          Colors.Red();
          Console.WriteLine($"Пропущен аргумент. Введите число.");
          Console.ResetColor();
          Thread.Sleep(2000);
          Console.Clear();
        }
      }
      public void Subtraction()
      {
        try
        {
          Console.Clear();
          Colors.Yellow();
          Console.Write("Введите первое число > ");
          Console.ResetColor();
          int NumberOne = Convert.ToInt16(Console.ReadLine());

          Console.Clear();
          Colors.Yellow();
          Console.Write("Введите второе число > ");
          Console.ResetColor();
          int NumberTwo = Convert.ToInt16(Console.ReadLine());

          Console.Clear();
          Colors.Green();
          Console.WriteLine($"Результат: {NumberOne - NumberTwo}");
          Console.ResetColor();

          Console.CursorVisible = false;
          Console.Write("Нажмите на клавишу: 'Enter' для продолжения...");
          Console.ReadKey();
          Console.Clear();
        }
        catch (Exception)
        {
          Console.CursorVisible = false;
          Console.Clear();
          Colors.Red();
          Console.WriteLine($"Пропущен аргумент. Введите число.");
          Console.ResetColor();
          Thread.Sleep(2000);
          Console.Clear();
        }
      }
      public void Multiplication()
      {
        try
        {
          Console.Clear();
          Colors.Yellow();
          Console.Write("Введите первое число > ");
          Console.ResetColor();
          int NumberOne = Convert.ToInt16(Console.ReadLine());

          Console.Clear();
          Colors.Yellow();
          Console.Write("Введите второе число > ");
          Console.ResetColor();
          int NumberTwo = Convert.ToInt16(Console.ReadLine());

          Console.Clear();
          Colors.Green();
          Console.WriteLine($"Результат: {NumberOne * NumberTwo}");
          Console.ResetColor();

          Console.CursorVisible = false;
          Console.Write("Нажмите на клавишу: 'Enter' для продолжения...");
          Console.ReadKey();
          Console.Clear();
        }
        catch (Exception)
        {
          Console.CursorVisible = false;
          Console.Clear();
          Colors.Red();
          Console.WriteLine($"Пропущен аргумент. Введите число.");
          Console.ResetColor();
          Thread.Sleep(2000);
          Console.Clear();
        }
      }
      public void Division()
      {
        try
        {
          Console.Clear();
          Colors.Yellow();
          Console.Write("Введите первое число > ");
          Console.ResetColor();
          int NumberOne = Convert.ToInt16(Console.ReadLine());

          Console.Clear();
          Colors.Yellow();
          Console.Write("Введите второе число > ");
          Console.ResetColor();
          int NumberTwo = Convert.ToInt16(Console.ReadLine());

          Console.Clear();
          Colors.Green();
          Console.WriteLine($"Результат: {NumberOne / NumberTwo}");
          Console.ResetColor();

          Console.CursorVisible = false;
          Console.Write("Наэмите на клавишу: 'Enter' для продолжения...");
          Console.ReadLine();
          Console.Clear();
        }
        catch (Exception)
        {
          Console.CursorVisible = false;
          Console.Clear();
          Colors.Red();
          Console.WriteLine($"Пропущен аргумент. Введите число.");
          Console.ResetColor();
          Thread.Sleep(2000);
          Console.Clear();
        }
      }
    }
  }
}