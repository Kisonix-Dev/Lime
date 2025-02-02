using System;
using System.IO;
using Lime.core;
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

      Cadence cadence = new Cadence()!;
      cadence.Terminal();
    }
  }
}