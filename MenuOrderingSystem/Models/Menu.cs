namespace MenuOrderingSystem.Models;

public class Menu
{
  public Dictionary<MenuItemType, string> MenuItems { get; set; } = new();
}