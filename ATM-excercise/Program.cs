using ATM_excercise;
using System;
using System.Collections.Generic;
using System.Numerics;

class Program
{
    static void Main()
    {
        BankService myBank = new BankService();

        long loggedUserAccountNum = 0;
        bool restart = true;

        long accountNum1 = myBank.CreateAccount("Milosz", "Schuler", Currency.PLN, 100);
        long accountNum2 = myBank.CreateAccount("Paulina", "Okulska", Currency.PLN, 100);

        while (restart == true)
        {
            ReadUserWelcomeChoice();

            if (loggedUserAccountNum > 0)
            {
                ReadLoggedUserChoice(loggedUserAccountNum);
            }
        }

        Currency ReadCurrencyOption()
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

        void ReadUserWelcomeChoice()
        {
            int userChoice;

            do
            {
                Console.WriteLine("Welcome to Bank. What would you like to do?");
                Console.WriteLine("To open account press 1");
                Console.WriteLine("To login to your account press 2");
                Console.WriteLine("To exit the bank press 3");

                int.TryParse(Console.ReadLine(), out userChoice);

                switch (userChoice)
                {
                    case 1:
                        loggedUserAccountNum = CreateNewUserAccount(myBank);
                        break;

                    case 2:
                        bool isLogin = LogUserIn();
                        if (isLogin)
                        {
                            return;
                        }
                        else
                        {
                            break;
                        }

                    case 3:
                        restart = false;
                        return;

                    default:
                        Console.WriteLine("Error");
                        break;
                }
            }
            while (userChoice != 3);
        }

        void ReadLoggedUserChoice(long accountNumber)
        {
            int userChoice;
            AccountActions action;

            do
            {
                //  Console.Clear();
                Console.WriteLine("What would you like to do now?");
                Console.WriteLine("To check your balance press 1");
                Console.WriteLine("To check your transaction history press 2");
                Console.WriteLine("To withdraw money in ATM press 3");
                Console.WriteLine("To deposit money in ATM press 4");
                Console.WriteLine("To transfer monney between accounts press 5");
                Console.WriteLine("To logout press 6");

                int.TryParse(Console.ReadLine(), out userChoice);

                switch (userChoice)
                {
                    case 1:
                        action = AccountActions.CheckBalance;
                        myBank.LoggedUserActions(accountNumber, action);
                        //accountNum = createNewUser(myBank);
                        break;
                    case 2:
                        action = AccountActions.TransactionHistory;
                        myBank.LoggedUserActions(accountNumber, action);
                        break;
                    case 3:
                        action = AccountActions.WithdrawATM;
                        myBank.LoggedUserActions(accountNumber, action);
                        break;
                    case 4:
                        action = AccountActions.DepositATM;
                        myBank.LoggedUserActions(accountNumber, action);
                        break;
                    case 5:
                        action = AccountActions.SendMoney;
                        myBank.LoggedUserActions(accountNumber, action);
                        break;
                    case 6:
                        Console.Clear();
                        myBank.LogUserOutFromAccount(accountNumber);
                        loggedUserAccountNum = 0;
                        Console.WriteLine("User successfully logged out.");
                        return;
                    default:
                        Console.WriteLine("Error.");
                        break;
                }
            } while (true && userChoice != 6);
        }

        #region Programme-to-bank communication

        long CreateNewUserAccount(BankService myBank)
        {
            string firstName = ReadString("First name:");
            string lastName = ReadString("Surname:");
            decimal balance = ReadDecimal("Inital deposit (optional):");

            Currency currency = ReadCurrencyOption();

            long newAccountNum = myBank.CreateAccount(firstName, lastName, currency, balance);
            //myBank.ReportAccountDetails(newAccountNum);

            return newAccountNum;
        }

        bool LogUserIn()
        {
            Console.WriteLine("Provide account number:");

            long accountNum = ReadLong("bank account");
            bool isLoggedIn = myBank.LogUserIntoAccount(accountNum, out var _);

            if (isLoggedIn)
            {
                loggedUserAccountNum = accountNum;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Input data validation

        decimal ReadDecimal(string inputDescription)
        {
            decimal userInput = 0;
            bool isCorrect = false;

            do
            {
                Console.WriteLine("Please enter your " + inputDescription + " (decimal)");
                try
                {
                    userInput = decimal.Parse(Console.ReadLine());
                    isCorrect = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Value is not a decimal.");
                }
            } while (isCorrect == false);

            return userInput;
        }

        long ReadLong(string inputDescription)
        {
            long input = 0;
            bool isCorrect = false;

            do
            {
                Console.WriteLine("Please enter your " + inputDescription + " (long)");
                try
                {
                    input = long.Parse(Console.ReadLine());
                    isCorrect = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Value is not a long.");
                }
            } while (isCorrect == false);

            return input;
        }

        string ReadString(string inputDescription)
        {
            string userInput = "";
            bool isCorrect = false;

            do
            {
                Console.WriteLine("Please enter your " + inputDescription + " (text)");
                userInput = Console.ReadLine();
                if (userInput == null || String.IsNullOrEmpty(userInput.Trim()))
                {
                    Console.WriteLine("Value is not a string.");
                }
                else
                {
                    isCorrect = true;
                }
            } while (!isCorrect);

            return userInput;
        }

        #endregion
    }
}