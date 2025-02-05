using System;
using Lime.colors;
namespace Lime.core
{
  //Логотип для загрузочного экрана.
  public class Logo
  {
    public static void PrintLogo()
    {
      Colors.Green();
      string Art = @"
  ###      ####    ##   ##  #######
   ##       ##     ### ###   ##   #
   ##       ##     #######   ## #
   ##       ##     #######   ####
   ##       ##     ## # ##   ## #
   ##       ##     ##   ##   ##   #
  ####     ####    ##   ##  #######";
      Console.WriteLine(Art);
      Console.ResetColor();
    }
  }
}