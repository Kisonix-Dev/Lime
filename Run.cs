using System;
using Lime.core;
namespace Lime
{
  class Run
  {
    public static string filePath = "database/account/userdata.json";
    static void Main()
    {
      Boot func = new Boot()!;
      func.Function_boot_system();

      if (File.Exists(filePath))
      {
        AuthenticationAccount.Authentication(filePath);
      }
      else
      {
        CreateNewAccount.Register(filePath);
      }
      //While
      //Switch Case
      //Create new two account
      Console.ReadKey();
    }
  }
}