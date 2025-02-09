using System;
using Lime.colors;
namespace Lime.core
{
  //Main help
  class Help
  {
    public void DocHelp()
    {
      Console.Clear();
      Console.WriteLine("Использование: [Command], [Option], [Arguments]");
      Colors.Yellow();
      Console.WriteLine("Аргументы не всегда обязательны к использованию.\n");
      Console.ResetColor();
      //System information 
      Console.WriteLine("uname - (Название ядра операционной системы.)");
      //Informations about users 
      Console.WriteLine("who - (Активный пользователь.)");
      //User Managenet 
      Console.WriteLine("useradd - (Создание нового пользователя.)");
      //Working witch the file system
      Console.WriteLine("mkdir - 'Название нового каталога' - (Создание нового каталога.)");
      //Lua scriots 
      Console.WriteLine("test - (Тестовый скрипт...)");
      //Other commands 
      Console.WriteLine("clear - (Очистка терминала.)");
      Console.WriteLine("help - (Помощь по всем командам.)");
      Console.WriteLine("[Command] --help - (Помощь по определённой команде.)");
      //System management
      Console.WriteLine("poweroff - (Выключение операционной системы.)");
      //SoftWare 
      Console.WriteLine("whitetext, wt - (Открыть текстовой редактор)\n");
    }
  }
}