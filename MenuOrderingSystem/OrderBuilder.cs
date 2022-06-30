using MenuOrderingSystem.Models;
using static MenuOrderingSystem.Constants;

namespace MenuOrderingSystem;

public class OrderBuilder
{
  private static readonly Dictionary<MenuType, Menu> Menus = GenerateMenus();
  private List<string> _errors;
  private List<int> _items;
  private MenuType _menuType;

  public OrderBuilder(MenuType type, List<int> items)
  {
    _items = items;
    _menuType = type;
    _errors = new List<string>();
  }

  public OrderBuilder SetOrder(MenuType menuType, List<int> items)
  {
    _items = items;
    _menuType = menuType;
    return this;
  }

  public string GetOrder()
  {
    _errors = new List<string>();
    var hasMain = _items.Contains((int)MenuItemType.Main);
    var hasSide = _items.Contains((int)MenuItemType.Side);
    var hasDesert = _items.Contains((int)MenuItemType.Dessert);

    if (!hasMain) _errors.Add("main is missing");
    if (!hasSide) _errors.Add("side is missing");
    if (!hasDesert && _menuType == MenuType.Dinner) _errors.Add("dessert is missing");
    if (hasDesert && _menuType != MenuType.Dinner) _errors.Add("dessert is only for dinner");

    if (_errors.Any()) throw new ArgumentException(GenerateErrorMessage(_errors));

    var hasMultipleMains = HasMultiple(MenuItemType.Main);
    var hasMultipleSides = HasMultiple(MenuItemType.Side);
    var hasMultipleDrinks = HasMultiple(MenuItemType.Drink);
    var hasMultipleDeserts = HasMultiple(MenuItemType.Dessert);

    if (hasMultipleMains)
      _errors.Add($"{GetDishName(MenuItemType.Main)} cannot be ordered more than once");
    else if (hasMultipleDeserts)
      _errors.Add($"{GetDishName(MenuItemType.Dessert)} cannot be ordered more than once");
    else if (hasMultipleSides && _menuType != MenuType.Lunch)
      _errors.Add($"{GetDishName(MenuItemType.Side)} cannot be ordered more than once");
    else if (hasMultipleDrinks && _menuType != MenuType.Breakfast)
      _errors.Add($"{GetDishName(MenuItemType.Drink)} cannot be ordered more than once");

    if (_errors.Any()) throw new ArgumentException(GenerateErrorMessage(_errors));

    var orderOutput = $"{GetDishName(MenuItemType.Main)}, {GetDishName(MenuItemType.Side)}{(hasMultipleSides ? "(" + GetDishCount(MenuItemType.Side) + ")" : "")}, {GetDishName(MenuItemType.Drink)}{(hasMultipleDrinks ? "(" + GetDishCount(MenuItemType.Drink) + ")" : "")}{(_menuType == MenuType.Dinner ? ", Water, " + GetDishName(MenuItemType.Dessert) : "")}";
    return orderOutput;
  }

  private bool HasMultiple(MenuItemType type)
  {
    return GetDishCount(type) > 1;
  }

  private string GetDishName(MenuItemType type)
  {
    if (type == MenuItemType.Drink && GetDishCount(MenuItemType.Drink) < 1) return Water;
    var name = Menus[_menuType].MenuItems[type];
    return name;
  }

  private int GetDishCount(MenuItemType type)
  {
    return _items.Count(i => i == (int)type);
  }

  private static string GenerateErrorMessage(IEnumerable<string> errors)
  {
    var msg = string.Join(", ", errors);
    msg = msg.ToLower();
    msg = char.ToUpper(msg[0]) + msg[1..];
    return msg;
  }

  private static Dictionary<MenuType, Menu> GenerateMenus()
  {
    var menus = new Dictionary<MenuType, Menu>
    {
      {
        MenuType.Breakfast, GenerateBreakfastMenu()
      },
      {
        MenuType.Lunch, GenerateLunchMenu()
      },
      {
        MenuType.Dinner, GenerateDinnerMenu()
      }
    };

    return menus;
  }

  private static Menu GenerateBreakfastMenu()
  {
    var breakfastMenu = new Menu();
    breakfastMenu.MenuItems.Add(MenuItemType.Main, "Eggs");
    breakfastMenu.MenuItems.Add(MenuItemType.Side, "Toast");
    breakfastMenu.MenuItems.Add(MenuItemType.Drink, "Coffee");
    return breakfastMenu;
  }

  private static Menu GenerateLunchMenu()
  {
    var lunchMenu = new Menu();
    lunchMenu.MenuItems.Add(MenuItemType.Main, "Sandwich");
    lunchMenu.MenuItems.Add(MenuItemType.Side, "Chips");
    lunchMenu.MenuItems.Add(MenuItemType.Drink, "Soda");
    return lunchMenu;
  }

  private static Menu GenerateDinnerMenu()
  {
    var dinnerMenu = new Menu();
    dinnerMenu.MenuItems.Add(MenuItemType.Main, "Steak");
    dinnerMenu.MenuItems.Add(MenuItemType.Side, "Potatoes");
    dinnerMenu.MenuItems.Add(MenuItemType.Drink, "Wine");
    dinnerMenu.MenuItems.Add(MenuItemType.Dessert, "Cake");
    return dinnerMenu;
  }
}