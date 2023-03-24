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

    public class BankTransfer : Transaction
    {
        public long SenderAccount { get; set; }

        public long RecipientAccount { get; set; }

        public BankTransferType BankTransferType { get; set; }

        //public Currency OriginalCurrency { get; set; }

        public override BankingOperationType BankingOperationType => BankingOperationType.BankTransfer;

        public BankTransfer (long senderAccount, long recipientAccount, BankTransferType transferType, decimal amount, Currency currencyOption) : base(amount, currencyOption)
        {
            SenderAccount = senderAccount;
            RecipientAccount = recipientAccount;
            BankTransferType = transferType;
        }
    }
}
