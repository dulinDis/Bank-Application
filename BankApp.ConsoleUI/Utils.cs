namespace BankApp.ConsoleUI
{
    static class Utils
    {
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

        public static decimal ReadDecimal()
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