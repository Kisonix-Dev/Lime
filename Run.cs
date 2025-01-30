using System;
using System.IO;
using Lime.core;
using Lime.colors;

namespace Lime
{
  class Run
  {
    public static string projectFilePath = Path.Combine("database/account/userdata.json");
    public static string executableFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LimeOS/database/account/userdata.json");
    static void Main()
    {
      string projectDirectoryPath = Path.GetDirectoryName(projectFilePath)!;
      if (!Directory.Exists(projectDirectoryPath))
      {
        Directory.CreateDirectory(projectDirectoryPath);
      }

      string executableDirectoryPath = Path.GetDirectoryName(executableFilePath)!;
      if (!Directory.Exists(executableDirectoryPath))
      {
        Directory.CreateDirectory(executableDirectoryPath);
      }

      if (File.Exists(executableFilePath))
      {
        AuthenticationAccount.Authentication(executableFilePath);
      }
      else
      {
        CreateNewAccount.Register(executableFilePath);
      }

      if (File.Exists(executableFilePath))
      {
        File.Copy(executableFilePath, projectFilePath, true);
      }
      //While.
      //Все команды хранить исключительно в каталоге проекта: "command"
      //Switch Case.
      //Create command - Create new account & delete.
      //Create command - Create New file & directory & delete.
      //Create command - Rename Name account.
      //Create command - Rename file & directory.
      //Возможно добавить шифрование для пароля учётной записи пользователя.
      Console.ReadKey();
    }
  }
}
