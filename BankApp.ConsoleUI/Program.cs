using System.Security.Cryptography;
using System.Security.Principal;
using ATM_excercise;

namespace BankApp.ConsoleUI
{
    internal class Program
    {
        static Menu MainMenu = new Menu()
        {
            //hierarchiczne menu?
            //class SubmenuItem : MenuItem?
            //class ActionItem : MenuItem?
            //w sumie może mogłoby by być submenu i action w ramach jednej klasy bez subklas
            //a może MenuItemAction = () => { subMenuAction.RunMenu() } ?
            //może każde menu powinno dziedziczyć z bazowego Menu żeby podzielić UI na klasy?

            Items =
            {
                new MenuItem("Create account", CreateAccount),
                new MenuItem("Login", LogIn),
                new MenuItem("Exit", Exit),
            }
        };

        static Menu LoggedUserMenu = new Menu()
        {
            Items =
            {
                new MenuItem("Check balance", CheckBalance),
                new MenuItem("Retrieve transaction history", RetrieveTransactionHistory),
                new MenuItem("Logout", LogOut)
            }
        };

        static BankService bankService = new BankService();
        static long loggedUserAccountNum = 0; //use Account instead of long here

        static void Main(string[] args)
        {
            Console.WriteLine("Hello stranger!");
            MainMenu.Run();
        }

        static void CheckBalance()
        {
        }

        static void RetrieveTransactionHistory()
        {
        }

        static void ReportAccountDetails() 
        {
            Account storedAccount = bankService.FindAccount(loggedUserAccountNum);
            Console.WriteLine($"Account no: {storedAccount.AccountNumber}.");
            Console.WriteLine($"Customer: {storedAccount.UserName} {storedAccount.UserSurname}.");
            Console.WriteLine($"Account currency: {storedAccount.AccountCurrency}.");
            Console.WriteLine($"Initial balance deposited upon account creation: {storedAccount.Balance}  {storedAccount.AccountCurrency}.");
        }

        static void LogIn()
        {
            Console.WriteLine("Provide account number:");
            long accountNum = Utils.ReadLong();

            Account account;
            if (!bankService.LogUserIntoAccount(accountNum, out account))
            {
                Console.WriteLine("The login attempt failed. The user with this account number does not exist.");
                MainMenu.Run();
            }
            else
            {
                loggedUserAccountNum = accountNum;
                Console.WriteLine($"The login attempt successful. Welcome {account.UserName} {account.UserSurname}.");
                LoggedUserMenu.Run();
            }
        }

        static void Exit()
        {
        }

        static void CreateAccount()
        {
        }

        static void LogOut()
        {
            MainMenu.Run();
        }
    }

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
                var line = Console.ReadLine();

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

    static class Utils
    {
        public static long ReadLong()
        {
            long value;

            while (!long.TryParse(Console.ReadLine(), out value))
                Console.WriteLine("Invalid input format");

            return value;
        }

        public static int ReadInt()
        {
            int value;

            while (!int.TryParse(Console.ReadLine(), out value))
                Console.WriteLine("Invalid input format");

            return value;
        }
    }

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