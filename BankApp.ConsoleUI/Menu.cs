namespace BankApp.ConsoleUI
{
    class Menu
    {
        public Menu()
        {
            Items = new List<MenuItem>();
        }

        public List<MenuItem> Items { get; }

        public void Run()
        {
            DisplayMenuItems();
            var chosenMenuItem = ReadUserChoice();
            chosenMenuItem.MenuItemAction();
        }

        public void DisplayMenuItems()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Console.WriteLine($"{(i + 1).ToString()}. {Items[i].Description}");
            }
        }

        public MenuItem ReadUserChoice()
        {
            while (true)
            {
                Console.Write("Please provide a number corresponding to menu item you want to choose: ");
                var choice = Utils.ReadInt();

                if (choice < 1 || choice > Items.Count)
                {
                    Console.WriteLine("Invalid number - out of menu range");
                    continue;
                }

                return Items[choice - 1];
            }
        }
    }



}