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
using System.Drawing;
using System.Data;
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
            Console.WriteLine("Пропущен аргумент, введите имя директории которую нужно удалить!");
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
          if (IsDirectoryEmpty(FullPath))
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
          else
          {
            while (true)
            {
              Console.CursorVisible = true;
              Console.Clear();
              Colors.Yellow();
              Console.Write("Директория не пустая, вы точно? хотите удалить директорию: (yes) , (no) > ");
              Console.ResetColor();

              string Input = Console.ReadLine()!;

              if (string.IsNullOrWhiteSpace(Input))
              {
                Console.CursorVisible = false;
                Console.Clear();
                Colors.Red();
                Console.WriteLine("Вы пропустили аргумент! Введите: (yes) или (no)!");
                Console.ResetColor();
                Thread.Sleep(2000);
                Console.Clear();
              }

              switch (Input.ToLower())
              {
                case "yes":
                  Console.CursorVisible = false;
                  Console.Clear();
                  Directory.Delete(FullPath, true);
                  Colors.Green();
                  Console.WriteLine($"Директория: '{DirectoryName}' Со всем содержимым, успешно удалена!");
                  Console.ResetColor();
                  Thread.Sleep(2000);
                  Console.Clear();
                  return;
                case "no":
                  Console.CursorVisible = false;
                  Console.Clear();
                  Colors.Yellow();
                  Console.WriteLine("Директория не удалена! Выход...");
                  Console.ResetColor();
                  Thread.Sleep(2000);
                  Console.Clear();
                  return;
              }
            }
          }
        }
        //Method to check if a directory is empty.
        static bool IsDirectoryEmpty(string FullPath)
        {
          return Directory.GetFiles(FullPath).Length == 0 && Directory.GetDirectories(FullPath).Length == 0;
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
      public void MoveDirectory()
      {
        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string CurrentDirectoryPath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя директории для перемещения > ");
          Console.ResetColor();

          string DirectoryName = Console.ReadLine()!;
          string FullPath = Path.Combine(CurrentDirectoryPath, DirectoryName);

          switch (DirectoryName)
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

          if (string.IsNullOrWhiteSpace(DirectoryName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Вы пропустили аргумент, введите имя директории!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя директории куда вы хотите переместить выбранную директорию > ");
          Console.ResetColor();

          string DestinationDirectoryName = Console.ReadLine()!;
          string DestinationPath = Path.Combine(CurrentDirectoryPath, DestinationDirectoryName, DirectoryName);

          switch (DestinationDirectoryName)
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

          if (string.IsNullOrWhiteSpace(DestinationDirectoryName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Вы пропустили аргумент, введите имя директории!");
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
            Console.WriteLine("Исходная директория не существует!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          if (!Directory.Exists(Path.Combine(CurrentDirectoryPath, DestinationDirectoryName)))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Yellow();
            Console.WriteLine("Целевая директория не существует. Создание новой директории...");
            Console.ResetColor();

            Directory.CreateDirectory(Path.Combine(CurrentDirectoryPath, DestinationDirectoryName));

            Thread.Sleep(2000);
            Console.Clear();
            Colors.Green();
            Console.WriteLine("Новая директория успешно создана!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            return;
          }

          try
          {
            if (Directory.Exists(DestinationPath))
            {
              Console.CursorVisible = false;
              Console.Clear();
              Colors.Yellow();
              Console.WriteLine("Целевая диреткория уже существует. Удаление старой директории...");

              Directory.Delete(DestinationPath, true);

              Thread.Sleep(2000);
              Console.Clear();
              Colors.Green();
              Console.WriteLine("Старая директория успешно удалена!");
              Console.ResetColor();
              Thread.Sleep(2000);
              Console.Clear();
              return;
            }

            Directory.Move(FullPath, DestinationPath);

            Console.CursorVisible = false;
            Console.Clear();
            Colors.Green();
            Console.WriteLine("Директория успешно перемещена!");
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
            Console.WriteLine($"Ошибка при перемещении директории: '{ex.Message}'");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            break;
          }
        }
      }
      //Move logic for Directory.
      private static void MoveAll(string sourceDir, string targetDir)
      {
        Directory.CreateDirectory(targetDir);

        foreach (string file in Directory.GetFiles(sourceDir))
        {
          string destFile = Path.Combine(targetDir, Path.GetFileName(file));
          File.Move(file, destFile);
        }

        foreach (string dir in Directory.GetDirectories(sourceDir))
        {
          string destDir = Path.Combine(targetDir, Path.GetFileName(dir));
          MoveAll(dir, destDir);
        }

        Directory.Delete(sourceDir);
      }
      public void MoveFile()
      {
        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string CurrentFIlePath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя файла для перемещения > ");
          Console.ResetColor();

          string FileName = Console.ReadLine()!;
          string FullPath = Path.Combine(CurrentFIlePath, FileName);

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
            Console.WriteLine("Вы пропустили аргумент, введите имя файла!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя директории куда вы хотите переместить выбранный файл > ");
          Console.ResetColor();

          string DestinationDirectoryName = Console.ReadLine()!;
          string DestinationPath = Path.Combine(CurrentFIlePath, DestinationDirectoryName, FileName);

          switch (DestinationDirectoryName)
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

          if (string.IsNullOrWhiteSpace(DestinationDirectoryName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Вы пропустили аргумент, введите имя директории!");
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
            Console.WriteLine("Исходный файл не найден!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          if (!Directory.Exists(Path.Combine(CurrentFIlePath, DestinationDirectoryName)))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Yellow();
            Console.WriteLine("Целевая директория не существует. Создание новой директории...");
            Console.ResetColor();

            Directory.CreateDirectory(Path.Combine(CurrentFIlePath, DestinationDirectoryName));

            Thread.Sleep(2000);
            Console.Clear();
            Colors.Green();
            Console.WriteLine("Новая директория успешно создана!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            return;
          }

          try
          {
            if (Directory.Exists(DestinationPath))
            {
              Console.CursorVisible = false;
              Console.Clear();
              Colors.Yellow();
              Console.WriteLine("Целевая дииректория уже существует. Удаление старой директории...");

              Directory.Delete(DestinationPath, true);

              Thread.Sleep(2000);
              Console.Clear();
              Colors.Green();
              Console.WriteLine("Старая директория успешно удалена!");
              Console.ResetColor();
              Thread.Sleep(2000);
              Console.Clear();
              return;
            }

            File.Move(FullPath, DestinationPath);

            Console.CursorVisible = false;
            Console.Clear();
            Colors.Green();
            Console.WriteLine("Файл успешно перемещён!");
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
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            break;
          }
        }
      }
      public void CopyDirectory()
      {
        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string CurrentDirectoryPath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя директории для копирования > ");
          Console.ResetColor();

          string DirectoryName = Console.ReadLine()!;
          string FullPath = Path.Combine(CurrentDirectoryPath, DirectoryName);

          switch (DirectoryName)
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

          if (string.IsNullOrWhiteSpace(DirectoryName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Вы пропустили аргумент, введите имя директории!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя директории куда вы хотите скопировать выбранную директорию > ");

          string DestinationDirectoryName = Console.ReadLine()!;
          string DestinationPath = Path.Combine(CurrentDirectoryPath, DestinationDirectoryName, DirectoryName);

          switch (DestinationDirectoryName)
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

          if (string.IsNullOrWhiteSpace(DestinationDirectoryName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Вы пропустили аргумент, введите имя директории!");
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
            Console.WriteLine("Исходная директория не найдена!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          if (!Directory.Exists(Path.Combine(CurrentDirectoryPath, DestinationDirectoryName)))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Yellow();
            Console.WriteLine("Целевая директория не существует. Создание новой директории...");
            Console.ResetColor();

            Directory.CreateDirectory(Path.Combine(CurrentDirectoryPath, DestinationDirectoryName));

            Thread.Sleep(2000);
            Console.Clear();
            Colors.Green();
            Console.WriteLine("Новая директория успешно создана!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            return;
          }

          try
          {
            if (Directory.Exists(DestinationPath))
            {
              Console.CursorVisible = false;
              Console.Clear();
              Colors.Yellow();
              Console.WriteLine("Целевая директория уже существует. Удаление старой директории...");

              Directory.Delete(DestinationPath, true);

              Thread.Sleep(2000);
              Console.Clear();
              Colors.Green();
              Console.WriteLine("Старая директория успешно удалена!");
              Console.ResetColor();
              Thread.Sleep(2000);
              Console.Clear();
              return;
            }

            CopyDirectoryRecursive(FullPath, DestinationPath);

            Console.CursorVisible = false;
            Console.Clear();
            Colors.Green();
            Console.WriteLine("Директория успешно скопирована!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
          }
          catch (Exception ex)
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine($"Ошибка при копировании директории: '{ex.Message}'");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
          }
        }
      }
      //Copy logic for directory.
      private void CopyDirectoryRecursive(string sourceDir, string destinationDir)
      {
        Directory.CreateDirectory(destinationDir);

        foreach (string file in Directory.GetFiles(sourceDir))
        {
          string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
          File.Copy(file, destFile, true);
        }

        foreach (string dir in Directory.GetDirectories(sourceDir))
        {
          string destDir = Path.Combine(destinationDir, Path.GetFileName(dir));
          CopyDirectoryRecursive(dir, destDir);
        }
      }
      public void CopyFile()
      {
        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string CurrentFilePath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя вайла для перемещения > ");
          Console.ResetColor();

          string FileName = Console.ReadLine()!;
          string FullPath = Path.Combine(CurrentFilePath, FileName);

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
            Console.WriteLine("Вы пропустили аргумент, введите имя файла!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            return;
          }

          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя Директории куда вы хотите скопировать выбранный файл > ");
          Console.ResetColor();

          string DestinationDirectoryName = Console.ReadLine()!;
          string DestinationPath = Path.Combine(CurrentFilePath, DestinationDirectoryName, FileName);

          switch (DestinationDirectoryName)
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

          if (string.IsNullOrWhiteSpace(DestinationDirectoryName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Вы пропустили аргумент, введите имя директории!");
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
            Console.WriteLine("Исходный файл не существует");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          if (!Directory.Exists(Path.Combine(CurrentFilePath, DestinationDirectoryName)))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Yellow();
            Console.WriteLine("Целевая директория не существует. Создание новой директории...");
            Console.ResetColor();

            Directory.CreateDirectory(Path.Combine(CurrentFilePath, DestinationDirectoryName));

            Thread.Sleep(2000);
            Console.Clear();
            Colors.Green();
            Console.WriteLine("Новая директория успешно создана!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            return;
          }

          try
          {
            if (File.Exists(DestinationPath))
            {
              Console.CursorVisible = false;
              Console.Clear();
              Colors.Yellow();
              Console.WriteLine("Файл уже существует в целевой директории, Перезапись файла...");
              Console.ResetColor();
              Thread.Sleep(2000);
              Console.Clear();
            }

            File.Copy(FullPath, DestinationPath, true);

            Console.CursorVisible = false;
            Console.Clear();
            Colors.Green();
            Console.WriteLine("Файл успешно скопирован!");
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
            Console.WriteLine($"Ошиюка при копирование файла: '{ex.Message}'");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            break;
          }
        }
      }
      public void DirectoryR()
      {
        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string CurrentDirectoryPath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Blue();
          Console.Write("Введите текущее имя директории > ");
          Console.ResetColor();

          string oldDirectoryName = Console.ReadLine()!;

          switch (oldDirectoryName)
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

          if (string.IsNullOrWhiteSpace(oldDirectoryName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Вы пропустили аргумент, введите имя директории!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          Console.Clear();
          Colors.Blue();
          Console.Write("Введите новое имя для директории > ");
          Console.ResetColor();

          string newDirectoryName = Console.ReadLine()!;

          switch (newDirectoryName)
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

          if (string.IsNullOrWhiteSpace(newDirectoryName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Вы пропустили аргумент, введите новое имя для директории!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          Console.Clear();

          string oldDirectoryPath = Path.Combine(CurrentDirectoryPath, oldDirectoryName);
          string newDirectoryPath = Path.Combine(CurrentDirectoryPath, newDirectoryName);

          if (Directory.Exists(oldDirectoryPath))
          {
            try
            {
              Directory.Move(oldDirectoryPath, newDirectoryPath);

              Console.CursorVisible = false;
              Console.Clear();
              Colors.Green();
              Console.WriteLine($"Диреткория успешно переименована в: '{newDirectoryName}'");
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
              Console.WriteLine($"Ошибка при переименовании директории: '{ex.Message}'");
              Console.ResetColor();
              Thread.Sleep(2000);
              Console.Clear();
              break;
            }
          }
          else
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine($"Директория с имененм: '{oldDirectoryName}' не существует.");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }
        }
      }
      public void FileR()
      {
        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string CurrentFilePath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        while (true)
        {
          Console.CursorVisible = true;
          Console.Clear();
          Colors.Blue();
          Console.Write("Введите имя файла > ");
          Console.ResetColor();

          string oldFileName = Console.ReadLine()!;

          switch (oldFileName)
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

          if (string.IsNullOrWhiteSpace(oldFileName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Вы пропустили аргумент, введите имя файла");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          Console.Clear();
          Colors.Blue();
          Console.Write("Введите новое имя для файла > ");
          Console.ResetColor();

          string newFileName = Console.ReadLine()!;

          switch (newFileName)
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

          if (string.IsNullOrWhiteSpace(newFileName))
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Вы пропустили аргумент, введите новое имя для файла!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }

          Console.Clear();

          string oldFilePath = Path.Combine(CurrentFilePath, oldFileName);
          string newFilePath = Path.Combine(CurrentFilePath, newFileName);

          if (File.Exists(oldFilePath))
          {
            try
            {
              File.Move(oldFilePath, newFilePath);

              Console.CursorVisible = false;
              Console.Clear();
              Colors.Green();
              Console.WriteLine($"Файл успешно переименован в: '{newFileName}'");
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
              Console.WriteLine($"Ошибка при переименовании файла: '{ex.Message}'");
              Console.ResetColor();
              Console.Clear();
              break;
            }
          }
          else
          {
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine($"Файл с именеи: '{oldFileName} не существует.'");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            continue;
          }
        }
      }
      public void Ls()
      {
        string HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string CurrentDirectoryPath = Path.Combine(HomeDirectory, "LimeOS", "main", "root", "home", "user");

        if (!Directory.Exists(CurrentDirectoryPath))
        {
          Console.WriteLine($"Директория не найдена: '{CurrentDirectoryPath}'");
          return;
        }

        string[] files = Directory.GetFiles(CurrentDirectoryPath);
        string[] directories = Directory.GetDirectories(CurrentDirectoryPath);

        foreach (var dir in directories)
        {
          Console.WriteLine($"\x1b[34m{Path.GetFileName(dir)}\x1b[0m");
        }

        foreach (var file in files)
        {
          Console.WriteLine(Path.GetFileName(file));
        }
      }
      public void Cd()
      {
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