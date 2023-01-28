using ATM_excercise;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;


Bank myBank = new Bank();
long accountNum = 0;

//Stack<Func<>> userAcions = new Stack<Func<>>;
//myBank.createAccount("Trolo", "lololo", currencyOptions.USD, 10);
long mattAccNum = myBank.createAccount("Mathias", "Okulska", currencyOptions.PLN,100);
long pauAccNum = myBank.createAccount("Paulina", "Okulska", currencyOptions.PLN, 100);
//long mattAccNum = myBank.createAccount("Matthias", "Flexk", currencyOptions.EUR, 30);
myBank.loginUser(mattAccNum);
accountNum = mattAccNum;

//myBank.loggedUserActions(1, accountActions.WithdrawATM);
//myBank.loggedUserActions(1, accountActions.TransactionHistory);



//userWelcomeScreen();

if (accountNum > 0)
{
    loggedUserChoiceScreen(accountNum);
    
}
//Console.WriteLine("Would you like to perform anoher operation?");
//userWelcomeScreen();




//for now we input all raw commands to bank here
//we say for example "deposit 10€ on account 4"
//and later on you can build an actual secure ATM that does that for you, verify your identity, keep a login session to an account
//and at that point the user only interacts with the bank
//for now we simulate that we are the ATM itself and we know that all actions (deposits withdrawals) are legit

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
            return currencyOptions.PLN;
            break;
        case "3":
            return currencyOptions.EUR;
            break;
        default: return currencyOptions.USD;

    }
}


void userWelcomeScreen()
{
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
                Console.WriteLine("Provide account number:");
                long accountNo = readLong("bank account");
                bool isLogin = myBank.loginUser(accountNo);
                if (isLogin)
                {
                    accountNum = accountNo;
                    return;
                }
                else
                {
                    break;
                }
            case 3:
                return;
            default:
                Console.WriteLine("Error");
                break;
        }
    }
    //while (userChoice != 1 && userChoice != 2);
    while (true); //we exit via return upon receiving an acceptable input

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
                action= accountActions.DepositATM;
                myBank.loggedUserActions(accountNumber, action);
                break;
            case 5:
                action = accountActions.SendMoney;
                myBank.loggedUserActions(accountNumber, action);
                return;
            case 6:
                Console.Clear();
                myBank.logoutUser(accountNumber);
                Console.WriteLine("User successfully logged out.");
                //bool restar
                userWelcomeScreen();
                return;
            default:
                Console.WriteLine("Error");
                break;         
        }
    } while (true && userChoice!=6);



}
long createNewUser(Bank myBank)
{
    string firstName = readString("first name");
    string lastName = readString("surname");
    decimal balance = readDecimal("inital deposit");
    currencyOptions currency = readCurrencyOption();

    long newAcc = myBank.createAccount(firstName, lastName, currency, balance);
    myBank.reportAccountDetails(1);

    return newAcc;
};

decimal readDecimal(string inputDescription)
{
    decimal input = 0;
    bool okay = false;
    do
    {
        Console.WriteLine("Please enter your " + inputDescription + " (decimal)");
        try
        {
            input = decimal.Parse(Console.ReadLine());
            okay = true;
        }
        catch (Exception)
        {
            Console.WriteLine("stupid rock, please try thinking before you write");
        }
    } while (okay == false);
    return input;
}


long readLong(string inputDescription)
{
    long input = 0;
    bool okay = false;
    do
    {
        Console.WriteLine("Please enter your " + inputDescription + " (long)");
        try
        {
            input = long.Parse(Console.ReadLine());
            okay = true;
        }
        catch (Exception)
        {
            Console.WriteLine("stupid rock, please try thinking before you write");
        }
    } while (okay == false);
    return input;
}

string readString(string inputDescription)
{
    string input = "";
    bool okay = false;
    do
    {
        Console.WriteLine("Please enter your " + inputDescription + " (text)");
        input = Console.ReadLine();
        if (input == null || String.IsNullOrEmpty(input.Trim()))
        {
            Console.WriteLine("stupid rock, please try hitting your keyboard before you press enter");
        }
        else
        {
            okay = true;
        }
    } while (!okay);
    return input;
}
