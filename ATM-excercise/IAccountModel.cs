using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_excercise
{
    enum accountTypes { currentAccount, savingsAccount }
    internal interface IAccountModel
    {

        bool ConcurrentSessionsAllowed { get; }
        accountTypes AccountType { get; }

}
}
