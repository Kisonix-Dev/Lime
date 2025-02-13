using System;
using Lime.core;
namespace Lime
{
  //Operating system loader.
  class Run
  {
    public static string baseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "LimeOS");
    public static string projectFilePath = Path.Combine(baseDirectory, "database", "account", "userdata.json");
    static void Main()
    {
      Console.Title = "Lime";

      //Loading screen.
      Boot boot = new Boot()!;
      boot.FunctionBootSystem();

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
      //Cadence terminal.
      Cadence cadence = new Cadence()!;
      cadence.Terminal();
    }
  }
}