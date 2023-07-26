using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcStatistics.Exceptions
{
    public class InvalidCalculationException : ApplicationException
    {
        public InvalidCalculationException(string reason) : base(reason)
        {
        }
    }
}
