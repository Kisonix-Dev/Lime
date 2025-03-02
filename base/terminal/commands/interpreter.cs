using System;
using NLua;
using KeraLua;
using Lime.colors;
using Lime.Core;
using Newtonsoft.Json;
using WhiteText;
using Lime.app.irc.chat;
using Lime.app.calculator;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Net;
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
      public void UserDelete()
      {
        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filePath = Path.Combine(homeDirectory, "LimeOS", "database", "account", "userdata.json");

        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Console.WriteLine("Введите 'exit' для выхода.");
          Colors.Blue();
          Console.Write("Введите имя пользователя для удаления: ");
          Console.ResetColor();
          string Input = Console.ReadLine()!;

          if (File.Exists(filePath))
          {
            var jsonData = File.ReadAllText(filePath);
            var users = JsonConvert.DeserializeObject<List<User>>(jsonData);

            if (users != null)
            {
              var userToRemove = users.FirstOrDefault(u => u.Username == Input);
              if (userToRemove != null)
              {
                users.Remove(userToRemove);
                File.WriteAllText(filePath, JsonConvert.SerializeObject(users));

                Console.CursorVisible = false;
                Console.Clear();
                Colors.Green();
                Console.WriteLine($"Пользователь {Input} успешно удален.");
                Console.ResetColor();
                Thread.Sleep(2000);
                Console.Clear();
                return;
              }
              if (string.IsNullOrWhiteSpace(Input))
              {
                Console.CursorVisible = false;
                Console.Clear();
                Colors.Red();
                Console.WriteLine("Пропущен аргумент. Введите имя пользователя!");
                Console.ResetColor();
                Thread.Sleep(2000);
                Console.Clear();
              }
              else
              {
                if (Input == "exit")
                {
                  Console.CursorVisible = false;
                  Console.Clear();
                  Colors.Yellow();
                  Console.WriteLine("Выход...");
                  Thread.Sleep(2000);
                  Console.Clear();
                  return;
                }
                Console.CursorVisible = false;
                Console.Clear();
                Colors.Red();
                Console.WriteLine($"Пользователь {Input} не найден.");
                Console.ResetColor();
                Thread.Sleep(2000);
                Console.Clear();
              }
            }
          }
          else
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Файл с данными пользователей не найден.");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            return;
          }
        }
      }
      class User
      {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
      }
      public void UserRename()
      {
        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filePath = Path.Combine(homeDirectory, "LimeOS", "database", "account", "userdata.json");

        List<User> users = LoadUsers(filePath);

        while (true)
        {
          string oldUsername;

          while (true)
          {
            Console.CursorVisible = true;
            Console.Clear();
            Console.WriteLine("Введите 'exit' для выхода.");
            Colors.Blue();
            Console.Write("Введите имя пользователя, которое нужно изменить: ");
            Console.ResetColor();
            oldUsername = Console.ReadLine()!;

            switch (oldUsername)
            {
              case "exit":
                Console.CursorVisible = false;
                Colors.Yellow();
                Console.WriteLine("Выход...");
                Console.ResetColor();
                Thread.Sleep(2000);
                Console.Clear();
                return;
            }

            if (!string.IsNullOrWhiteSpace(oldUsername))
            {
              break;
            }
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Вы пропустили аргумент! Пожалуйста, введите имя пользователя.");
            Console.ResetColor();
            Thread.Sleep(2000);
          }

          User userToChange = users.Find(u => u.Username!.Equals(oldUsername))!;

          if (userToChange != null)
          {
            string newUsername;

            while (true)
            {
              Console.CursorVisible = true;
              Console.Clear();
              Colors.Blue();
              Console.Write("Введите новое имя пользователя: ");
              Console.ResetColor();
              newUsername = Console.ReadLine()!;

              if (!string.IsNullOrWhiteSpace(newUsername))
              {
                break;
              }
              Console.CursorVisible = false;
              Console.Clear();
              Colors.Red();
              Console.WriteLine("Вы пропустили аргумент! Пожалуйста, введите новое имя пользователя.");
              Console.ResetColor();
              Thread.Sleep(2000);
            }

            userToChange.Username = newUsername;
            SaveUsers(filePath, users);

            Console.CursorVisible = false;
            Console.Clear();
            Colors.Green();
            Console.WriteLine("Имя пользователя успешно изменено!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            return;
          }
          else
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Пользователь с таким именем не найден!");
            Console.ResetColor();
            Thread.Sleep(2000);
          }
        }
      }

      static List<User> LoadUsers(string filePath)
      {
        if (!File.Exists(filePath))
        {
          return new List<User>();
        }
        string jsondata = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<User>>(jsondata) ?? new List<User>();
      }
      static void SaveUsers(string filePath, List<User> users)
      {
        string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
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
      public void Client()
      {
        Client client = new Client()!;
        client.Client_irc();
      }
      public void Server()
      {
        Server server = new Server()!;
        server.Server_irc();
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
      public void Mkdir()
      {
        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Cyan();
          Console.Write("Введите название для новой директории > ");
          Console.ResetColor();

          string NameDirectory = Console.ReadLine()!;

          switch (NameDirectory)
          {
            case "exit":
              Console.CursorVisible = false;
              Console.Clear();
              Colors.Yellow();
              Console.WriteLine("Выход...");
              Console.ResetColor();
              Thread.Sleep(2000);
              Console.Clear();
              return;
          }

          if (string.IsNullOrWhiteSpace(NameDirectory))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Пропущен аргумент. Директория не создана!");
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }
          else
          {
            string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string NewDirectoryPath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user", NameDirectory);

            try
            {
              Directory.CreateDirectory(NewDirectoryPath);

              Console.CursorVisible = false;
              Console.Clear();
              Colors.Green();
              Console.WriteLine($"Новая директория {NameDirectory} успешно создана!");
              Thread.Sleep(2000);
              Console.Clear();
              return;
            }
            catch (Exception ex)
            {
              Console.CursorVisible = false;
              Console.Clear();
              Colors.Red();
              Console.WriteLine($"Ошибка при создании новой директории: '{ex.Message}'");
              Console.ResetColor();
              Thread.Sleep(2000);
              Console.Clear();
              break;
            }
          }
        }
      }
      public void Touch()
      {
        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string NewDirectoryPath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя для нового файла > ");
          Console.ResetColor();

          string FileName = Console.ReadLine()!;
          string FullPath = Path.Combine(NewDirectoryPath, FileName);

          switch (FileName)
          {
            case "exit":
              Console.CursorVisible = false;
              Console.Clear();
              Colors.Yellow();
              Console.WriteLine("Выход...");
              Console.ResetColor();
              Thread.Sleep(2000);
              Console.Clear();
              return;
          }

          if (string.IsNullOrWhiteSpace(FileName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Пропущен аргумент, Введите имя файла!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

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
            Colors.Green();
            Console.WriteLine($"Файл '{FileName}' успешно создан по пути '{FullPath}");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            return;
          }
          catch (Exception ex)
          {
            Console.CursorVisible = false;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка при создании файла: '{ex.Message}'");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            break;
          }
        }
      }
      public void Cat()
      {

        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string NewDirectoryPath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя файла > ");
          Console.ResetColor();

          string FileName = Console.ReadLine()!;
          string FullPath = Path.Combine(NewDirectoryPath, FileName);

          switch (FileName)
          {
            case "exit":
              Console.CursorVisible = false;
              Console.Clear();
              Colors.Yellow();
              Console.WriteLine("Выход...");
              Console.ResetColor();
              Thread.Sleep(2000);
              Console.Clear();
              return;
          }

          if (string.IsNullOrWhiteSpace(FileName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Пропущен аргумент, введите имя файла!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          if (!File.Exists(FullPath))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine($"Файл: '{FileName}' не найден!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          try
          {
            string content = File.ReadAllText(FullPath);

            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine($"Содержимое файла: '{FileName}'\n");
            Console.WriteLine(content);
            Colors.Green();
            Console.WriteLine("\nНажмите на клавишу: 'Enter' для выхода.");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
          }
          catch (Exception ex)
          {
            Console.Clear();
            Colors.Red();
            Console.WriteLine($"Ошибка при создании файла: '{ex.Message}'");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            break;
          }
        }
      }
      public void RmD()
      {
        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string NewDirectoryPath = Path.Combine(homeDirectory, "LimeOS", "main", "root", "home", "user");

        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя директории для удаления > ");
          Console.ResetColor();

          string DirectoryName = Console.ReadLine()!;
          string FullPath = Path.Combine(NewDirectoryPath, DirectoryName);

          switch (DirectoryName)
          {
            case "exit":
              Console.CursorVisible = false;
              Console.Clear();
              Colors.Yellow();
              Console.WriteLine("Выход...");
              Thread.Sleep(2000);
              Console.Clear();
              return;
          }

          if (string.IsNullOrWhiteSpace(DirectoryName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Пропущен аргумент, директория не удалена!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          if (!Directory.Exists(FullPath))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine($"Директория: '{DirectoryName}' не найдена!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          try
          {
            Console.CursorVisible = false;
            Console.Clear();
            Directory.Delete(FullPath);
            Colors.Green();
            Console.WriteLine($"Директория: '{DirectoryName}' успешно удалена!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            return;
          }
          catch (Exception ex)
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine($"Ошибка при удалении директории: '{ex.Message}'");
            Thread.Sleep(2000);
            Console.Clear();
            break;
          }
        }
      }
      public void Rm()
      {
        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string NewDirectoryPath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя файла > ");
          Console.ResetColor();

          string FileName = Console.ReadLine()!;
          string FullPath = Path.Combine(NewDirectoryPath, FileName);

          switch (FileName)
          {
            case "exit":
              Console.CursorVisible = false;
              Console.Clear();
              Colors.Yellow();
              Console.WriteLine("Выход...");
              Console.ResetColor();
              Thread.Sleep(2000);
              Console.Clear();
              return;
          }

          if (string.IsNullOrWhiteSpace(FileName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Пропущен аргумент, введите имя файла!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          if (!File.Exists(FullPath))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine($"Файл: '{FileName}' не найден!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          try
          {
            Console.CursorVisible = false;
            Console.Clear();
            File.Delete(FullPath);
            Colors.Green();
            Console.WriteLine($"Файл: '{FileName}' успешно удалён.");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            return;
          }
          catch (Exception ex)
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine($"Ошибка при создании файла: '{ex.Message}'");
            Thread.Sleep(2000);
            Console.Clear();
            break;
          }
        }
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