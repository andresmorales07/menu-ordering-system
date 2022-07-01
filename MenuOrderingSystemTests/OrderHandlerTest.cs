using static MenuOrderingSystem.OrderHandler;

namespace MenuOrderingSystemTests;

[TestClass]
public class OrderHandlerTest
{
  [DataTestMethod]
  [DataRow("Brekfast 1,2,3", "Unable to process: Brekfast is not a valid menu type")]
  [DataRow("Breakfast 1,2,3", "Eggs, Toast, Coffee")]
  [DataRow("Breakfast 3,2,1", "Eggs, Toast, Coffee")]
  [DataRow("Breakfast 1,2,3,3,3", "Eggs, Toast, Coffee(3)")]
  [DataRow("Breakfast 1,3,2,3,3", "Eggs, Toast, Coffee(3)")]
  [DataRow("Breakfast 1", "Unable to process: Side is missing")]
  [DataRow("Lunch 1,2,3", "Sandwich, Chips, Soda")]
  [DataRow("Lunch 1,2", "Sandwich, Chips, Water")]
  [DataRow("Lunch 1,1,2,3", "Unable to process: Sandwich cannot be ordered more than once")]
  [DataRow("Lunch 1,2,2", "Sandwich, Chips(2), Water")]
  [DataRow("Lunch", "Unable to process: Main is missing, side is missing")]
  [DataRow("Dinner 1,2,3,4", "Steak, Potatoes, Wine, Water, Cake")]
  [DataRow("Dinner 1,2,4", "Steak, Potatoes, Water, Cake")]
  [DataRow("Dinner 1,2,3", "Unable to process: Dessert is missing")]
  public void TestOrderHandler(string order, string expected)
  {
    Assert.AreEqual(expected, TakeOrder(order));
  }
}