using System;
using System.Drawing;
using Lime.colors;
namespace Lime.core
{
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