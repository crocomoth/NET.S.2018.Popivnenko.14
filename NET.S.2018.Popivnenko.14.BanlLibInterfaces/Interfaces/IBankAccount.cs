using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.S._2018.Popivnenko._14.BanlLibInterfaces.Interfaces
{
    public interface IBankAccount
    {
        void AddFunds(decimal funds);

        void WithdrawFunds(decimal funds);

        void SetUnactive();

        void ChangePoints(Func<int, int> functionDelegate);

        string GetHolderName();

        string GetHolderSurname();
    }
}
