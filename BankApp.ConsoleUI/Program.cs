using System.Security.Cryptography;
using System.Security.Principal;
using System.Transactions;
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
                new MenuItem("Withdraw money in ATM", WithdrawFromATM),
                new MenuItem("Deposit money in ATM", DepositToATM),
                new MenuItem("Transfer money between accounts", TransferToAccount),
                new MenuItem("Logout", LogOut),
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
            Account storedAccount = bankService.GetAccount(loggedUserAccountNum);
            Console.WriteLine("Account balance:");
            Console.WriteLine($"{storedAccount.Balance} {storedAccount.AccountCurrency}");

            LoggedUserMenu.Run();
        }

        static void RetrieveTransactionHistory()
        {
            Account storedAccount = bankService.GetAccount(loggedUserAccountNum);
            Console.WriteLine("Transaction history:");
            if (storedAccount.TransactionHistory.Count > 0)
            {
                foreach (var transaction in storedAccount.TransactionHistory)
                {
                    Console.WriteLine($"Transaction made on {transaction.CreatedAt}. Value: {transaction.Amount} {transaction.Currency}. Type: {transaction.BankingOperationTypeDisplayText}");
                }
            }
            else
            {
                Console.WriteLine("No transactions registered on this account.");
            }
            LoggedUserMenu.Run();
        }

        static void DisplayBankTransferDetails(BankTransfer bankTransfer)
        {
            if (bankTransfer.BankTransferType == BankTransferType.Outgoing)
            {
                Console.WriteLine($"You sent on {bankTransfer.CreatedAt}  {bankTransfer.Amount * (-1)}  {bankTransfer.Currency} to {bankTransfer.RecipientAccount}");
                Console.WriteLine($"New bank transfer with transaction ID {bankTransfer.TransactionId} created on {bankTransfer.CreatedAt} for value {bankTransfer.Amount * (-1)} {bankTransfer.Currency}. Transaction type: {bankTransfer.BankingOperationType}. Tranfer type {bankTransfer.BankTransferType}. Sending party account: {bankTransfer.SenderAccount} and receiving party account: {bankTransfer.RecipientAccount}");
            }
            else
            {
                Console.WriteLine($"You received on {bankTransfer.CreatedAt}  {bankTransfer.Amount} {bankTransfer.Currency} from {bankTransfer.SenderAccount}");
                Console.WriteLine($"New bank transfer with transaction ID {bankTransfer.TransactionId} created on {bankTransfer.CreatedAt} for value {bankTransfer.Amount} {bankTransfer.Currency}. Transaction type: {bankTransfer.BankingOperationType}. Tranfer type {bankTransfer.BankTransferType}. Sending party account: {bankTransfer.SenderAccount} and receiving party account: {bankTransfer.RecipientAccount}");
            }
        }


        public void DisplayBankDepositDetails(BankDeposit bankDeposit)
        {
            Console.WriteLine($"New bank deposit with transaction ID {bankDeposit.TransactionId} created on {bankDeposit.CreatedAt} for value {bankDeposit.Amount} {bankDeposit.Currency}. Transaction type: {bankDeposit.BankingOperationTypeDisplayText}. Receiving party account: {bankDeposit.RecipientAccount}.");
        }

        static public void DepositToATM()
        {
            Account storedAccount = bankService.GetAccount(loggedUserAccountNum);
            Console.WriteLine("How much would you like deposit (decimal)?");
            var amount = Utils.ReadDec();

            if (Utils.IsPositive(amount))
            {
                ATMTransaction transaction = bankService.DepositToATM(storedAccount.AccountNumber, amount);
                DisplayATMTransactionDetails(transaction);
            }
            else
            {
                Console.WriteLine("Invalid operation.");
                Console.WriteLine($"Your tried to deposit negative number.");
                Console.WriteLine($"Try again.");
            }
            LoggedUserMenu.Run();
        }

        static public void TransferToAccount()
        
        {
            Account senderAccount = bankService.GetAccount(loggedUserAccountNum);

            Console.WriteLine("How much would you like to send (decimal)?");
            var amount = Utils.ReadDec();

            Console.WriteLine("What is the recipient you would like to send to? Provide account number (long)");
            long recipientAccountNumber = Utils.ReadLong();
            
            while (loggedUserAccountNum == recipientAccountNumber)
            {
                Console.WriteLine("Invalid recipient number. You can't send money to yourself. Provide another account number (long)");
                Console.WriteLine("What is the recipient you would like to send to? Provide account number (long)");
                recipientAccountNumber = Utils.ReadLong();
            };
            Account recipientAccount = bankService.GetAccount(recipientAccountNumber);


            while (recipientAccount == null)
            {
                Console.WriteLine("Invalid recipient number. Provide account number (long)");
                Console.WriteLine("What is the recipient you would like to send to? Provide account number (long)");
                recipientAccountNumber = Utils.ReadLong();
                recipientAccount = bankService.GetAccount(recipientAccountNumber);
            }
           


            if (!Utils.IsPositive(amount) || amount >= senderAccount.Balance)
            {
                Console.WriteLine("Invalid operation.");
                Console.WriteLine($"Try again.");
            }
            else
            {
                BankTransfer bankTransfer = bankService.TransferToAccount(senderAccount, recipientAccount, amount);
                DisplayBankTransferDetails(bankTransfer);
            }
            LoggedUserMenu.Run();
        }

        static public void WithdrawFromATM()
        {

            Account storedAccount = bankService.GetAccount(loggedUserAccountNum);
            Console.WriteLine("how much would you like to withdraw? (decimal)? To escape press esc on your keyboard");
            var amount = Utils.ReadDec();

            if (!Utils.IsPositive(amount) || amount >= storedAccount.Balance)
            {
                Console.WriteLine("invalid operation.");
                Console.WriteLine($"Your  current balannce: {storedAccount.Balance} {storedAccount.AccountCurrency}. You tried to withdraw {amount} {storedAccount.AccountCurrency}");
                Console.WriteLine($"Try again.");
            }
            else
            {
                ATMTransaction transaction = bankService.WithdrawFromATM(loggedUserAccountNum, amount);
                DisplayATMTransactionDetails(transaction);
            }
            LoggedUserMenu.Run();
        }

        static public void DisplayATMTransactionDetails(ATMTransaction transaction)
        {
            Console.WriteLine($"New transaction with transaction ID {transaction.TransactionId} created on {transaction.CreatedAt} for value {transaction.Amount} {transaction.Currency}. Transaction type: {transaction.ATMTransactionType}");
        }

        static void ReportAccountDetails(long loggedUserAccountNum)
        {
            Account storedAccount = bankService.GetAccount(loggedUserAccountNum);
            Console.WriteLine($"Account no: {storedAccount.AccountNumber}.");
            Console.WriteLine($"Customer: {storedAccount.UserName} {storedAccount.UserSurname}.");
            Console.WriteLine($"Account currency: {storedAccount.AccountCurrency}.");
            Console.WriteLine($"Initial balance deposited upon account creation: {storedAccount.Balance}  {storedAccount.AccountCurrency}.");
        }

        static void LogIn()
        {
            Console.WriteLine("Provide account number:");
            long accountNum = Utils.ReadLong();
            Account account = null;
            bankService.LogUserIntoAccount(accountNum, out account);

            if (account == null)
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
            System.Environment.Exit(1);
        }

        static Currency ReadCurrencyOption()
        {
            Console.WriteLine("Please provide the account currency:");
            Console.WriteLine("1 - USD");
            Console.WriteLine("2 - EUR");
            Console.WriteLine("3 - PLN");

            string userChoice = Console.ReadLine().Trim();

            switch (userChoice)
            {
                case "1":
                    return Currency.USD;
                case "2":
                    return Currency.EUR;
                case "3":
                    return Currency.PLN;
                default:
                    return Currency.USD;
            }
        }
        static void CreateAccount()
        {
            Console.WriteLine("First name:");
            string firstName = Console.ReadLine().Trim();
            Console.WriteLine("Surname:");
            string lastName = Console.ReadLine().Trim();
            Console.WriteLine("Inital deposit (decimal):");
            decimal balance = Utils.ReadDec();
            Currency currency = ReadCurrencyOption();

            long newAccountNum = bankService.CreateAccount(firstName, lastName, currency, balance);
            Account storedAccount = bankService.GetAccount(newAccountNum);
            ReportAccountDetails(storedAccount.AccountNumber);//take argument??
            MainMenu.Run();
        }

        static void LogOut()
        {
            Account account;
            bankService.LogUserOutFromAccount(loggedUserAccountNum, out account);
            Console.WriteLine($"Ravendb account, {account.AccountNumber}, {account.Id}, {account.UserName}, logged in:{account.IsLoggedIn}");
            Console.WriteLine("User successfully logged out.");
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
    {//tofix
        public static long ReadLong()
        {
            long value;

            while (!long.TryParse(Console.ReadLine(), out value) || value < 0)
                Console.WriteLine("Invalid input format. Please provide a long.");

            return value;
        }

        public static int ReadInt()
        {
            int value;

            while (!int.TryParse(Console.ReadLine(), out value) || value < 0)
                Console.WriteLine("Invalid input format. Please provide an int.");

            return value;
        }


        public static decimal ReadDec()
        {
            decimal value;

            while (!decimal.TryParse(Console.ReadLine(), out value) || value < 0)
                Console.WriteLine("Invalid input format. Please provide a decimal");

            return value;
        }


        public static bool IsPositive(decimal amount)
        {
            return amount >= 0;
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