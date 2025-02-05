using System;
using System.IO;
using Lime.colors;
namespace Lime.core
{
  //Cadence Terminal.
  class Cadence
  {
    private readonly string PC = "PC-0";
    public void Terminal()
    {
      Console.Title = "Lime";
      var user = AuthenticationAccount.GetAuthenticatedUser();
      var cmd = new Interpreter.Commands()!;

      while (true)
      {
        user = AuthenticationAccount.GetAuthenticatedUser();
        string CurrentDirectory = "/";
        Colors.Cyan();
        Console.Write($"{user!.Username}@{PC}:{CurrentDirectory}$ ");
        Console.ResetColor();
        string Input = Console.ReadLine()!;

        switch (Input)
        {
          //System information.
          case "uname":
            cmd.Uname();
            break;
          case "uname -r":
            cmd.UnameR();
            break;
          case "uname --realese":
            cmd.UnameR();
            //cmd.unameRealese();
            break;
          //Information about users.
          case "who":
            cmd.Who();
            break;
          //Working with the file system.
          case "mkdir":
            cmd.MkdirA();
            break;
          //Lua Scripts.
          case "test": //Soon...
            cmd.Test();
            break;
          //other commands.
          case "clear":
            cmd.Clear();
            break;

          case "user --add":
            cmd.UserAdd();
            break;
          case "logout":
            cmd.Logout();
            break;

          case "poweroff":
            cmd.PowerOff();
            return;
          default:
            if (string.IsNullOrWhiteSpace(Input)) { }
            else
            {
              Colors.Red();
              Console.WriteLine($"{Input}: Команда не найдена");
              Console.ResetColor();
            }
            continue;
        }
        /*Create command - Create new account & delete.
          Create command - Create New file & directory & delete.
          Create command - Rename Name account.
          Create command - Rename file & directory.
          Create command - ls, cd, cat...
          Create more...*/
      }
    }
  }
}