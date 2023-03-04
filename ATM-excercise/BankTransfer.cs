using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static ATM_excercise.ATMTransaction;

namespace ATM_excercise
{
    public enum BankTransferType
    {
        Incoming,
        Outgoing
    }

    internal class BankTransfer : Transaction
    {
        public long SenderAccount { get; set; }

        public long RecipientAccount { get; set; }

        public BankTransferType BankTransferType { get; set; }

        public override BankingOperationType BankingOperationType => BankingOperationType.BankTransfer;

        public BankTransfer (long senderAccount, long recipientAccount, BankTransferType transferType, decimal amount, Currency currencyOption) : base(amount, currencyOption)
        {
            SenderAccount = senderAccount;
            RecipientAccount = recipientAccount;
            BankTransferType = transferType;
        }

        public void DisplayBankTransferDetails()
        {
            if (BankTransferType == BankTransferType.Outgoing)
            {
                Console.WriteLine($"You sent on {CreatedAt}  {Amount * (-1)}  {Currency} to {RecipientAccount}");
                Console.WriteLine($"New bank transfer with transaction ID {TransactionId} created on {CreatedAt} for value {Amount * (-1)} {Currency}. Transaction type: {BankingOperationType}. Tranfer type {BankTransferType}. Sending party account: {SenderAccount} and receiving party account: {RecipientAccount}");
            }
            else
            {
                Console.WriteLine($"You received on {CreatedAt}  {Amount} {Currency} from {SenderAccount}");
                Console.WriteLine($"New bank transfer with transaction ID {TransactionId} created on {CreatedAt} for value {Amount} {Currency}. Transaction type: {BankingOperationType}. Tranfer type {BankTransferType}. Sending party account: {SenderAccount} and receiving party account: {RecipientAccount}");
            }
        }
    }
}
