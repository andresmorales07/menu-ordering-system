using MenuOrderingSystem.Models;
using static MenuOrderingSystem.Constants;

namespace MenuOrderingSystem;

public static class OrderHandler
{
  public static string TakeOrder(string order)
  {
    try
    {
      var parts = order.Split(" ");
      var menuType = ToMenuType(parts[0]);
      OrderBuilder orderBuilder;

      if (parts.Length == 1)
      {
        orderBuilder = new OrderBuilder(menuType, new List<int>());
        return orderBuilder.GetOrder();
      }

      var itemIds = parts[1].Split(",");
      var ids = itemIds.Select(int.Parse).ToList();
      orderBuilder = new OrderBuilder(menuType, ids);
      return orderBuilder.GetOrder();
    }
    catch (ArgumentException e)
    {
      return $"Unable to process: {e.Message}";
    }
  }

  private static MenuType ToMenuType(string stringVal)
  {
    if (string.IsNullOrWhiteSpace(stringVal)) throw new ArgumentException("Menu type cannot be null or empty");
    return stringVal switch
    {
      Breakfast => MenuType.Breakfast,
      Lunch => MenuType.Lunch,
      Dinner => MenuType.Dinner,
      _ => throw new ArgumentException($"{stringVal} is not a valid menu type")
    };
  }
}