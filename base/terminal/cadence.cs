using System;
using Lime.colors;
namespace Lime.core
{
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
        Colors.Cyan();
        Console.Write($"{user!.Username} | {PC} | > ");
        Console.ResetColor();
        string Input = Console.ReadLine()!;

        switch (Input)
        {
          case "uname":
            cmd.uname();
            break;
          case "uname -r":
            cmd.unameR();
            break;
          case "uname --realese":
            cmd.unameR();
            //cmd.unameRealese();
            break;
          case "who":
            cmd.who();
            break;
          case "clear":
            cmd.clear();
            break;
          case "exit":
            cmd.exit();
            return;
          case "test": //Soon...
            cmd.Test();
            break;
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