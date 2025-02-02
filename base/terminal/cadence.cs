using System;
using System.Diagnostics;
using Lime.colors;
namespace Lime.core
{
  class Cadence
  {
    public void Terminal()
    {
      var user = AuthenticationAccount.GetAuthenticatedUser();
      var cmd = new Interpreter.Command()!;

      while (true)
      {
        Console.Write($"PC | {user!.Username} | > ");
        string Input = Console.ReadLine()!;

        switch (Input)
        {
          case "uname":
            cmd.uname();
            break;
          case "uname -r":
            cmd.unameR();
            break;
          case "who":
            cmd.who();
            break;
          case "exit":
            cmd.exit();
            return;
          default:
            Colors.Red();
            Console.WriteLine("Неверная команда!");
            Console.ResetColor();
            continue;
        }
        //Create command - Create new account & delete.
        //Create command - Create New file & directory & delete.
        //Create command - Rename Name account.
        //Create command - Rename file & directory.
        //Create command - ls, cd, cat...
        //Create more...
      }
    }
  }
}