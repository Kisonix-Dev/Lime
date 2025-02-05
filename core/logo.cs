using System;
using Lime.colors;
namespace Lime.core
{
  //Logo for loading screen.
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