namespace BankApp.ConsoleUI
{
    delegate void MenuItemAction();

    class MenuItem
    {
        public MenuItem(string description, MenuItemAction menuItemAction)
        {
            Description = description;
            MenuItemAction = menuItemAction;
        }

        public string Description { get; }
        public MenuItemAction MenuItemAction { get; }

    }



}