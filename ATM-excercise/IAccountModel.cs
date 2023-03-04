using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    enum AccountTypes 
    { 
        CurrentAccount, 
        SavingsAccount 
    }

    internal interface IAccountModel
    { 
        bool ConcurrentSessionsAllowed { get; }
        AccountTypes AccountType { get; }
    }
}
