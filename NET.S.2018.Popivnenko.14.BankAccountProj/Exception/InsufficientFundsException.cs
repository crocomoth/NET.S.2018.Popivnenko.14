using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.S._2018.Popivnenko._14.BankAccountProj.Exception
{
    public class InsufficientFundsException : System.Exception
    {
        public InsufficientFundsException(string message) : base(message)
        {
        }
    }
}
