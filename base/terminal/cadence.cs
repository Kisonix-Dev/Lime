using System;
using System.IO;
using Lime.app.calculator;
using Lime.colors;
using WhiteText;
namespace Lime.core
{
  //Cadence Terminal.
  class Cadence
  {
    private readonly string PC = "PC-0";
    private readonly string CurrentDirectory = "/";
    public void Terminal()
    {
      Console.Title = "Lime";
      var user = AuthenticationAccount.GetAuthenticatedUser();
      var cmd = new Interpreter.Commands()!;

      while (true)
      {
        Console.CursorVisible = true;
        user = AuthenticationAccount.GetAuthenticatedUser();
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
          case "uname --help":
            cmd.UnameRHelp();
            break;
          //Information about users.
          case "who":
            cmd.Who();
            break;
          //User Management 
          case "useradd":
            cmd.UserAdd();
            break;
          case "user -d":
            cmd.UserDelete();
            break;
          case "user --delete":
            cmd.UserDelete();
            break;
          case "user -r":
            cmd.UserRename();
            break;
          case "user --rename":
            cmd.UserRename();
            break;
          //Working with the file system.
          case "touch":
            cmd.Touch();
            break;
          case "cat":
            cmd.Cat();
            break;
          case "rm":
            cmd.Rm();
            break;
          case "mpd":
            cmd.MoveDirectory();
            break;
          case "mp":
            cmd.MoveFile();
            break;
          case "cpd":
            cmd.CopyDirectory();
            break;
          case "cp":
            cmd.CopyFile();
            break;
          case "rmd":
            cmd.RmD();
            break;
          case "mkdir":
            cmd.Mkdir();
            break;
          case "directory -r":
            cmd.DirectoryR();
            break;
          case "directory --rename":
            cmd.DirectoryR();
            break;
          case "file -r":
            cmd.FileR();
            break;
          case "file --rename":
            cmd.FileR();
            break;
          case "ls":
            cmd.Ls();
            break;
          case "Cd":
            cmd.Cd();
            break;
          //Lua Scripts.
          case "test": //Soon...
            cmd.Test();
            break;
          //other commands.
          case "clear":
            cmd.Clear();
            break;
          case "help":
            cmd.Help();
            break;
          case "logout":
            cmd.Logout();
            break;
          case "date":
            cmd.Date();
            break;
          //System management
          case "poweroff":
            cmd.PowerOff();
            return;
          //SoftWare 
          case "whitetext":
            cmd.WhiteText();
            break;
          case "calc":
            cmd.Calc();
            break;
          case "+":
            cmd.Addition();
            break;
          case "-":
            cmd.Subtraction();
            break;
          case "*":
            cmd.Multiplication();
            break;
          case "/":
            cmd.Division();
            break;
          case "wt":
            cmd.WhiteText();
            break;
          case "client":
            cmd.Client();
            break;
          case "server":
            cmd.Server();
            break;
          default:
            if (string.IsNullOrWhiteSpace(Input)) { }
            else
            {
              Colors.Red();
              Console.WriteLine($"{Input}: Команда не найдена!");
              Console.ResetColor();
            }
            continue;
        }
      }
    }
  }
}