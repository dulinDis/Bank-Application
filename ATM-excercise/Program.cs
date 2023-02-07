using ATM_excercise;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;



Bank myBank = new Bank();
long accountNum = 0;
bool restart = true;

long acc1 = myBank.createAccount("Mathias", "Schuler", currencyOptions.PLN, 100);
long acc2 = myBank.createAccount("Paulina", "Okulska", currencyOptions.PLN, 100);


while (restart == true)
    
    {
        userWelcomeScreen();
        if (accountNum > 0)
        {
            loggedUserChoiceScreen(accountNum);
        }
    } 




currencyOptions readCurrencyOption()
{
    Console.WriteLine("Please provide the account currency:");
    Console.WriteLine("1 - USD");
    Console.WriteLine("2 - EUR");
    Console.WriteLine("3 - PLN");

    string userChoice = Console.ReadLine().Trim();
    switch (userChoice)
    {
        case "1":
            return currencyOptions.USD;
            break;
        case "2":
            return currencyOptions.EUR;
            break;
        case "3":
            return currencyOptions.PLN;
            break;
        default: return currencyOptions.USD;
    }
}

//programme-to-bank communication: instructions for user to trigger actions
void userWelcomeScreen()
{
    //restart = true;
    int userChoice;
    do
    {
        Console.WriteLine("Welcome to Bank. What would you like to do?");
        Console.WriteLine("To open account press 1");
        Console.WriteLine("To login to your account press 2");
        Console.WriteLine("To exit the bank press 3");

        Int32.TryParse(Console.ReadLine(), out userChoice);
        switch (userChoice)
        {
            case 1:
                accountNum = createNewUser(myBank);
                break;
            case 2:

                bool isLogin = loginUser();
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
                //break;
                return;
            default:
                Console.WriteLine("Error");
                break;
        }
    }
    while (userChoice!=3); 
}

void loggedUserChoiceScreen(long accountNumber)
{
    int userChoice;
    accountActions action;
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
        Int32.TryParse(Console.ReadLine(), out userChoice);

        switch (userChoice)
        {
            case 1:
                action = accountActions.CheckBalance;
                myBank.loggedUserActions(accountNumber, action);
                //accountNum = createNewUser(myBank);
                break;
            case 2:
                action = accountActions.TransactionHistory;
                myBank.loggedUserActions(accountNumber, action);
                break;
            case 3:
                action = accountActions.WithdrawATM;
                myBank.loggedUserActions(accountNumber, action);
                break;
            case 4:
                action = accountActions.DepositATM;
                myBank.loggedUserActions(accountNumber, action);
                break;
            case 5:
                action = accountActions.SendMoney;
                myBank.loggedUserActions(accountNumber, action);
                break;
            case 6:
                Console.Clear();
                myBank.logoutUser(accountNumber);
                accountNum = 0;
                Console.WriteLine("User successfully logged out.");

                //bool restar
                // userWelcomeScreen();
                return;
            default:
                Console.WriteLine("Error.");
                break;
        }
    } while (true && userChoice != 6);
}

//programme-to-bank communication: instructions for user account
long createNewUser(Bank myBank)
{
    string firstName = readString("First name:");
    string lastName = readString("Surname:");
    decimal balance = readDecimal("Inital deposit (optional):");
    currencyOptions currency = readCurrencyOption();
    long newAcc = myBank.createAccount(firstName, lastName, currency, balance);
    myBank.reportAccountDetails(newAcc);
    return newAcc;
};
bool loginUser()
{
    Console.WriteLine("Provide account number:");
    long accountNo = readLong("bank account");
    bool isLogin = myBank.loginUser(accountNo);
    if (isLogin)
    {
        accountNum = accountNo;
        return true;
    }
    else
    {
        return false;
    }
}

//data type check
decimal readDecimal(string inputDescription)
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
        catch (Exception)
        {
            Console.WriteLine("Value is not a decimal.");
        }
    } while (isCorrect == false);
    return userInput;
}
long readLong(string inputDescription)
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
        catch (Exception)
        {
            Console.WriteLine("Value is not a long.");
        }
    } while (isCorrect == false);
    return input;
}
string readString(string inputDescription)
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

