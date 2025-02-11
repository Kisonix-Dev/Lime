using System;
using Lime.colors;
using System.Threading;
namespace Lime.core
{
  //Text editor.
  public class WhiteText
  {
    //WriteText menu
    public void WhiteTextMenu()
    {
      var Version = "1.0";

      while (true)
      {
        Console.CursorVisible = true;
        Console.Clear();
        Colors.Blue();
        Console.WriteLine($"Добро пожаловать в текстовой редактор WhiteText v.{Version}\n");
        Console.ResetColor();
        Colors.Yellow();
        Console.WriteLine("1) Создать текстовой файл.\n2) Открыть текстовой файл.\n3) Удалить текстовой файл.\n4) Выход.\n");
        Console.ResetColor();

        Console.Write("Выберите пункт > ");
        string Input = Console.ReadLine()!;
        switch (Input)
        {
          case "1":
            CreateTextFile();
            break;
          case "2":
            OpenTextFile();
            break;
          case "3":
            DeleteTextFile();
            break;
          case "4":
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Yellow();
            Console.WriteLine("Выход из WhiteText...");
            Thread.Sleep(1000);
            Console.Clear();
            return;
          default:
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Выберите пункт в меню!");
            Console.ResetColor();
            Thread.Sleep(1000);
            break;
        }
      }
    }
    //Create new text file.
    private void CreateTextFile()
    {
      Console.Clear();

      string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      string newDirectoryPath = Path.Combine(homeDirectory, "LimeOS", "main", "root", "home", "user");

      Colors.Blue();
      Console.Write("Введите имя файла (с расширением .txt):");
      Console.ResetColor();

      string FileName = Console.ReadLine()!;
      string FullPath = Path.Combine(newDirectoryPath, FileName);

      try
      {
        if (!Directory.Exists(newDirectoryPath))
        {
          Directory.CreateDirectory(newDirectoryPath);
        }

        using (StreamWriter writer = new StreamWriter(FullPath))
        {
          Console.WriteLine("\nВведите текст (для завершения нажмите Enter дважды):\n");
          string line;
          while ((line = Console.ReadLine()!) != "")
          {
            writer.WriteLine(line);
          }
        }
        Console.CursorVisible = false;
        Colors.Green();
        Console.WriteLine($"Файл '{FileName}' успешно создан по пути '{FullPath}");
      }
      catch (Exception ex)
      {
        Console.CursorVisible = false;
        Console.Clear();
        Colors.Red();
        Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
        Console.ResetColor();
      }
      Console.CursorVisible = false;
      Console.WriteLine("\nНажмите любую клавишу для продолжения...");
      Console.ReadKey();
    }

    //Open text file.
    private void OpenTextFile()
    {
      Console.Clear();

      string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      string directoryPath = Path.Combine(homeDirectory, "LimeOS", "main", "root", "home", "user");

      Console.Write("Введите имя файла (с расширением .txt): ");

      string FileName = Console.ReadLine()!;
      string FullPath = Path.Combine(directoryPath, FileName);

      try
      {
        string content = File.ReadAllText(FullPath);
        Console.Clear();
        Console.WriteLine($"Содержимое файла '{FileName}':\n");
        Console.WriteLine(content);
      }
      catch (Exception ex)
      {
        Console.CursorVisible = false;
        Console.Clear();
        Colors.Red();
        Console.WriteLine($"Ошибка при открытии файла: {ex.Message}");
        Console.ResetColor();
      }
      Console.CursorVisible = false;
      Console.WriteLine("\nНажмите любую клавишу для продолжения...");
      Console.ReadKey();
    }

    //Delete text file.
    private void DeleteTextFile()
    {
      Console.Clear();

      string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      string directoryPath = Path.Combine(homeDirectory, "LimeOS", "main", "root", "home", "user");

      Console.Write("Введите имя файла (с расширением .txt): ");

      string FileName = Console.ReadLine()!;
      string FullPath = Path.Combine(directoryPath, FileName);

      try
      {
        Console.CursorVisible = false;
        Console.Clear();
        File.Delete(FullPath);
        Colors.Green();
        Console.WriteLine($"Файл '{FileName}' успешно удалён!");
        Console.ResetColor();
        Thread.Sleep(2000);
        Console.Clear();
      }
      catch (Exception ex)
      {
        Console.CursorVisible = false;
        Console.Clear();
        Colors.Red();
        Console.WriteLine($"Ошибка при удалении файла: {ex.Message}");
        Console.ResetColor();
      }
      Console.CursorVisible = false;
      Console.WriteLine("\nНажмите любую клавишу для продолжения...");
      Console.ReadKey();
    }
  }
}

