using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.S._2018.Popivnenko._14.DAL.Exception
{
    public class DalException : System.Exception
    {
        public DalException(string message) : base(message)
        {
        }
    }
}
