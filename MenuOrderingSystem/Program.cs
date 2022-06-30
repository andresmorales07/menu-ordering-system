using System.Diagnostics.CodeAnalysis;
using static MenuOrderingSystem.OrderHandler;

namespace MenuOrderingSystem;

public static class Program
{
  [SuppressMessage("ReSharper", "UnusedParameter.Global")]
  public static void Main(string[] args)
  {
    Console.WriteLine("Welcome to the menu ordering system, may I take your order? (Enter DONE to exit)");
    var done = false;

    do
    {
      var order = Console.ReadLine();

      if (order == "DONE")
        done = true;
      else
        Console.WriteLine(TakeOrder(order ?? ""));
    } while (!done);

    Console.WriteLine("Have a nice day!");
  }
}