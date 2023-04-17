using ATM_excercise;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{


    public static class CurrencyConverter
    {
        public static readonly Dictionary<Currency, double> currenciesInUSD = new Dictionary<Currency, double>();
        /// <summary>
        /// Adds curren exchange rates: base currency is USD. Currently hardcoded.
        /// </summary>
        public static void AddCurrencyExchangeRates()
        {
            currenciesInUSD.Add(Currency.EUR, 1.08);
            currenciesInUSD.Add(Currency.PLN, 0.23);
            currenciesInUSD.Add(Currency.USD, 1);
        }
        /// <summary>
        /// Converts amoun from one currency to another.
        /// </summary>
        /// <param name="amountTransfered">Amount transferred.</param>
        /// <param name="senderCurrency">Currency - sender account.</param>
        /// <param name="recipientCurrency">Corrency - recipient account.</param>
        /// <returns>Convered amount.</returns>
        public static decimal ConvertBetweenCurrencies(decimal amountTransfered, Currency senderCurrency, Currency recipientCurrency)
        {
            AddCurrencyExchangeRates();

            if (senderCurrency == recipientCurrency) return amountTransfered;

            if (senderCurrency == Currency.USD)
            {
                decimal convertedAmount = amountTransfered / (decimal)currenciesInUSD[recipientCurrency];
                return convertedAmount;
            }

            if (recipientCurrency == Currency.USD)
            {
                decimal convertedAmount = amountTransfered * (decimal)currenciesInUSD[senderCurrency];
                return convertedAmount;
            }

            if (senderCurrency == Currency.EUR && recipientCurrency == Currency.PLN || senderCurrency == Currency.PLN && recipientCurrency == Currency.EUR)
            {
                decimal convertedAmount = amountTransfered * (decimal)currenciesInUSD[senderCurrency] / (decimal)currenciesInUSD[recipientCurrency];
                return convertedAmount;
            }
            return amountTransfered;
        }

    }
}