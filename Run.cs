using System;
using Lime.core;
namespace Lime
{
  class Run
  {
    static void Main()
    {
      Boot func = new Boot()!;
      func.Function_boot_system();
      //Добавить проверку на созданную учётную запись. 
      Register.RegistrationUser();
    }
  }
}