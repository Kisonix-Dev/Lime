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
      Console.WriteLine("Использование: [Command], [Option], [Arguments].");
      Colors.Yellow();
      Console.WriteLine("Аргументы не всегда обязательны к использованию.\n");
      Console.ResetColor();
      //System information 
      Console.WriteLine("uname - Название ядра операционной системы.");
      //Informations about users 
      Console.WriteLine("who - Активный пользователь.");
      //User Managenet 
      Console.WriteLine("useradd - Создание нового пользователя.");
      Console.WriteLine("user -d, --delete - Удаление пользователя.");
      Console.WriteLine("user -r, --rename - Изменение имя пользователя.");
      //Working witch the file system
      Console.WriteLine("mkdir - [Название нового каталога] - Создание нового каталога.");
      Console.WriteLine("touch [Имя файла] - Создание текстового файла.");
      Console.WriteLine("cat [Имя файла] - Вывести содержимое текстового файла на экран терминала.");
      Console.WriteLine("rm [Имя файла] - Удаление текстового файла.");
      Console.WriteLine("rmd [Имя директории] - Удаление директории.");
      Console.WriteLine("mpd [Имя директории], [Имя новой директории] - Перемещение директории.");
      Console.WriteLine("mp [Имя файла], [Имя директории] - Перемещение файла в директорию.");
      Console.WriteLine("cpd [Имя директории], [Имя новой директории] - Копирование директории.");
      Console.WriteLine("cp [Имя файла], [Имя директории] - Копирование файла в директорию.");
      Console.WriteLine("directory -r, --rename [Исходное имя директории] [Новое имя для директории] - Переименование директории.");
      Console.WriteLine("file -r, --rename [Исходное имя файла] [Новое имя для файла] - Переименование файла.");
      Console.WriteLine("ls - Вывод всех файлов и директорий пользователя.");
      //Lua scripts 
      Console.WriteLine("test - Тестовый скрипт...");
      //Other commands
      Console.WriteLine("clear - Очистка терминала.");
      Console.WriteLine("help - Помощь по всем командам.");
      Console.WriteLine("[Command] --help - Помощь по определённой команде.");
      Console.WriteLine("date - Показать актуальную дату и время.");
      //System management
      Console.WriteLine("poweroff - Выключение операционной системы.");
      //SoftWare 
      Console.WriteLine("whitetext, wt - Открыть текстовой редактор.");
      Console.WriteLine("calc, +,-,*,/ - Открыть калькулятор\n");
    }
  }
}