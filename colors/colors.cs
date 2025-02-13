using System;
namespace Lime.colors
{
  class Colors
  {
    public enum Color : byte
    {
      White,
      Black,
      Green,
      Yellow,
      Gray,
      Magenta,
      Blue,
      Red,
      Cyan
    }
    public static void SetColor(Color color)
    {
      switch (color)
      {
        case Color.White:
          Console.ForegroundColor = ConsoleColor.White;
          break;
        case Color.Black:
          Console.ForegroundColor = ConsoleColor.Black;
          break;
        case Color.Green:
          Console.ForegroundColor = ConsoleColor.Green;
          break;
        case Color.Yellow:
          Console.ForegroundColor = ConsoleColor.Yellow;
          break;
        case Color.Gray:
          Console.ForegroundColor = ConsoleColor.Gray;
          break;
        case Color.Magenta:
          Console.ForegroundColor = ConsoleColor.Magenta;
          break;
        case Color.Blue:
          Console.ForegroundColor = ConsoleColor.Blue;
          break;
        case Color.Red:
          Console.ForegroundColor = ConsoleColor.Red;
          break;
        case Color.Cyan:
          Console.ForegroundColor = ConsoleColor.Cyan;
          break;
      }
    }
    public static void White() => SetColor(Color.White);
    public static void Black() => SetColor(Color.Black);
    public static void Green() => SetColor(Color.Green);
    public static void Yellow() => SetColor(Color.Yellow);
    public static void Gray() => SetColor(Color.Gray);
    public static void Magenta() => SetColor(Color.Magenta);
    public static void Blue() => SetColor(Color.Blue);
    public static void Red() => SetColor(Color.Red);
    public static void Cyan() => SetColor(Color.Cyan);
  }
}