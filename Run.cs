using System;
using System.IO;
using Lime.core;
using Lime.colors;
namespace Lime
{
  class Run
  {
    public static string baseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "LimeOS");
    public static string projectFilePath = Path.Combine(baseDirectory, "database", "account", "userdata.json");
    static void Main()
    {
      Boot func = new Boot()!;
      func.Function_boot_system();

      string projectDirectoryPath = Path.GetDirectoryName(projectFilePath)!;
      if (!Directory.Exists(projectDirectoryPath))
      {
        Directory.CreateDirectory(projectDirectoryPath);
      }

      if (File.Exists(projectFilePath))
      {
        AuthenticationAccount.Authentication(projectFilePath);
      }
      else
      {
        CreateNewAccount.Register(projectFilePath);
      }
      Console.ReadKey();
      //While.
      //Switch Case.
      //Create command - Create new account & delete.
      //Create command - Create New file & directory & delete.
      //Create command - Rename Name account.
      //Create command - Rename file & directory.
    }
  }
}