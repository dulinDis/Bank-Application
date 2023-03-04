using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    public static class Validation
    {
        //method to check the correct format
        public static bool IsAccountNumberCorrect(long accountNumber) 
        { 
            if (accountNumber <= 0 || accountNumber < 1000) 
                return false;
            else
                return true;
        }

        //check if string contains only characters 
        public static bool CheckIfAllCharacters()
        {
            string userInput = "";
            bool isCorrect;

            if (userInput == null || String.IsNullOrEmpty(userInput.Trim()) || userInput.All(Char.IsLetter) != true)
            {
                Console.WriteLine("value incorrect");
                isCorrect = false;   
            }
            else
            {
                isCorrect=true;
            }

            return isCorrect;
        }

        //method to check if account already exist in the database
    }
}
