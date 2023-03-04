using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    internal class BankDeposit : Transaction
    {
        public long RecipientAccount { get; set; }

        public override BankingOperationType BankingOperationType => BankingOperationType.BankDeposit;

        public BankDeposit(long recipientAccount, decimal amount, Currency currencyOption) : base(amount, currencyOption)
        {
            RecipientAccount = recipientAccount;
        }

        public void DisplayBankTransferDetails()
        {
            Console.WriteLine($"New bank deposit with transaction ID {TransactionId} created on {CreatedAt} for value {Amount} {Currency}. Transaction type: {BankingOperationTypeDisplayText}. Receiving party account: {RecipientAccount}.");
        }
    }
}
