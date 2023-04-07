using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    public static class Utils
    {//tofix
        public static long ReadLong(string input)
        {
            long value;
            long.TryParse(input, out value); 
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
}
