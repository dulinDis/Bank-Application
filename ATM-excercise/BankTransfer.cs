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
    /// <summary>
    /// Possible Bank Transfer types.
    /// </summary>
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

        public Currency OriginalCurrency { get; set; }

        public override BankingOperationType BankingOperationType => BankingOperationType.BankTransfer;

        /// <summary>
        /// Creates new Bank Transfer.
        /// </summary>
        /// <param name="senderAccount">Account of a sending party.</param>
        /// <param name="recipientAccount">>Account of a reciving party.</param>
        /// <param name="transferType">Bank Transfer type.</param>
        /// <param name="originalCurrency">Outgoing transfer currency - original amount currncy from sender account.</param>
        /// <param name="amount">Transfer amount</param>
        /// <param name="currency">Inccoming transfer currency - original amount currncy o recipient account.</param>
        public BankTransfer (long senderAccount, long recipientAccount, BankTransferType transferType, Currency originalCurrency, decimal amount, Currency currency) : base(amount, currency)
        {
            SenderAccount = senderAccount;
            RecipientAccount = recipientAccount;
            BankTransferType = transferType;
            OriginalCurrency = originalCurrency;
        }
    }
}
