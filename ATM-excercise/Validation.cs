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
        public static bool IsAccountNumberCorrect(long accountNumber) { 
            if (accountNumber <= 0 || accountNumber < 1000) 
            {
                return false;
            }
            else return true;
        }

        //method to check if already exist in the database
    }
}
