using System;
using System.Threading;
using Lime.colors;
namespace Lime.app.calculator
{
  class Calculator
  {
    public string Version = "1.0";

    public void Calc_Menu()
    {
      while (true)
      {
        Console.CursorVisible = true;
        Console.Clear();
        Colors.Yellow();
        Console.WriteLine($"Добро пожаловать в калькулятор: V.{Version}\n");
        Console.ResetColor();

        Console.WriteLine("1) +\n2) -\n3) *\n4) /\n5) Выход\n");
        Console.Write("Выберите действие > ");
        string Input = Console.ReadLine()!;

        switch (Input)
        {
          case "1":
            Addition();
            break;
          case "2":
            Subtraction();
            break;
          case "3":
            Multiplication();
            break;
          case "4":
            Division();
            break;
          case "5":
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Yellow();
            Console.WriteLine("Выход из калькулятора...");
            Console.ResetColor();
            Thread.Sleep(1000);
            Console.Clear();
            return;
          default:
            Console.CursorVisible = false;
            Console.Clear();
            Colors.Red();
            Console.WriteLine("Вы не выбрали действие!");
            Console.ResetColor();
            Thread.Sleep(1000);
            Console.Clear();
            break;
        }
      }
    }
    public void Addition()
    {
      Console.Clear();
      Colors.Yellow();
      Console.Write("Введите первое число > ");
      Console.ResetColor();
      int NumberOne = Convert.ToInt16(Console.ReadLine());

      Console.Clear();
      Colors.Yellow();
      Console.Write("Введите второе число > ");
      Console.ResetColor();
      int NumberTwo = Convert.ToInt16(Console.ReadLine());

      Console.Clear();
      Colors.Green();
      Console.WriteLine($"Результат: {NumberOne + NumberTwo}\n");
      Console.ResetColor();

      Console.CursorVisible = false;
      Console.Write("Нажмите на клавишу: 'Enter' для продолжения...");
      Console.ReadKey();
      Console.Clear();
    }
    public void Subtraction()
    {
      Console.Clear();
      Colors.Yellow();
      Console.Write("Введите первое число > ");
      Console.ResetColor();
      int NumberOne = Convert.ToInt16(Console.ReadLine());

      Console.Clear();
      Colors.Yellow();
      Console.Write("Введите второе число > ");
      Console.ResetColor();
      int NumberTwo = Convert.ToInt16(Console.ReadLine());

      Console.Clear();
      Colors.Green();
      Console.WriteLine($"Результат: {NumberOne - NumberTwo}\n");
      Console.ResetColor();

      Console.CursorVisible = false;
      Console.Write("Нажмите на клавишу: 'Enter' для продолжения...");
      Console.ReadKey();
      Console.Clear();
    }
    public void Multiplication()
    {
      Console.Clear();
      Colors.Yellow();
      Console.Write("Введите первое число > ");
      Console.ResetColor();
      int NumberOne = Convert.ToInt16(Console.ReadLine());

      Console.Clear();
      Colors.Yellow();
      Console.Write("Введите второе число > ");
      Console.ResetColor();
      int NumberTwo = Convert.ToInt16(Console.ReadLine());

      Console.Clear();
      Colors.Green();
      Console.WriteLine($"Результат: {NumberOne * NumberTwo}\n");
      Console.ResetColor();

      Console.CursorVisible = false;
      Console.Write("Нажмите на клавишу: 'Enter' для продолжения...");
      Console.ReadKey();
      Console.Clear();
    }
    public void Division()
    {
      Console.Clear();
      Colors.Yellow();
      Console.Write("Введите первое число > ");
      Console.ResetColor();
      int NumberOne = Convert.ToInt16(Console.ReadLine());

      Console.Clear();
      Colors.Yellow();
      Console.Write("Введите второе число > ");
      Console.ResetColor();
      int NumberTwo = Convert.ToInt16(Console.ReadLine());

      Console.Clear();
      Colors.Green();
      Console.WriteLine($"Результат: {NumberOne / NumberTwo}\n");
      Console.ResetColor();

      Console.CursorVisible = false;
      Console.Write("Нажмите на клавишу: 'Enter' для продолжения...");
      Console.ReadKey();
      Console.Clear();
    }
  }
}