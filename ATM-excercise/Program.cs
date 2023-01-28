using ATM_excercise;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

//Stack<Func<>> userAcions = new Stack<Func<>>;

Bank myBank = new Bank();
long accountNum = 0;

long acc1 = myBank.createAccount("Mathias", "Schuler", currencyOptions.PLN, 100);
long acc2 = myBank.createAccount("Paulina", "Okulska", currencyOptions.PLN, 100);

//myBank.loginUser(acc1);
//accountNum = acc1;


//  Console.Clear();


/*if (accountNum > 0)
{
    loggedUserChoiceScreen(accountNum);
}*/


bool restart=true;


userWelcomeScreen();
if (accountNum > 0)
{
    loggedUserChoiceScreen(accountNum);
}

do
{

    userWelcomeScreen();

    if (accountNum > 0)
    {
        loggedUserChoiceScreen(accountNum);
    }

} while (restart == true);











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
            return currencyOptions.EUR;
            break;
        case "3":
            return currencyOptions.PLN;
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

           bool isLogin = loginUser();
                if (isLogin)
                {
                   
                    return;
                }
                else
                {
                    break;
                } 
          /*  Console.WriteLine("Provide account number:");
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
             }*/
            case 3:
                restart = false;
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
                Console.WriteLine("Error");
                break;         
        }
    } while (true && userChoice!=6);



}

//void seedInitialCustomers(){}


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
            Console.WriteLine("stupid rock, please try thinking before you write");
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
            Console.WriteLine("stupid rock, please try thinking before you write");
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
            Console.WriteLine("stupid rock, please try hitting your keyboard before you press enter");
        }
        else
        {
            isCorrect = true;
        }
    } while (!isCorrect);
    return userInput;
}

