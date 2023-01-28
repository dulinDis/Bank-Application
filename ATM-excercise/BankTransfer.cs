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
    public enum transferType {incoming, outgoing };
    internal class BankTransfer : Transaction, ITransactionModel
    {

        private long senderAccount;
        private long recipientAccount;
        private transferType transferType;
        public string type="Transfer";
        public long SenderAccount
        {
            get { return senderAccount; }
            set
            {
 
                    senderAccount = value;

            }
        }
        public long RecipientAccount
        {
            get { return recipientAccount; }
            set
            {
               
                    recipientAccount = value;
                
            }
        }

        public transferType TransferType
        {
            get;
            set;

        }

        public BankTransfer (long senderAccount, long recipientAccount, transferType transferType, decimal amount, currencyOptions currencyOption) : base(amount, currencyOption)
        {
            SenderAccount = senderAccount;
            RecipientAccount = recipientAccount;
            TransferType = transferType;
        }

        public void displayBankTransferDetails()
        {
            if (TransferType == transferType.outgoing)
            {
                Console.WriteLine($"You sent on {CreatedAt}  {Amount} {Currency} to {RecipientAccount}");
                Console.WriteLine($"New bank transfer with transaction ID {TransactionID} created on {CreatedAt} for value {Amount} {Currency}. Transaction type: {type}. Tranfer type {TransferType}. Sending party account: {SenderAccount} and receiving party account: {RecipientAccount}");

            }
            else {
                Console.WriteLine($"You received on {CreatedAt}  {Amount} {Currency} from {SenderAccount}");
                Console.WriteLine($"New bank transfer with transaction ID {TransactionID} created on {CreatedAt} for value {Amount} {Currency}. Transaction type: {type}. Tranfer type {TransferType}. Sending party account: {SenderAccount} and receiving party account: {RecipientAccount}");


              }
        }
    //    public void performBankTransaction(BankTransfer transaction)
    //    {
     //       Console.WriteLine("bank transfer performed");
     //   }
    }
}
