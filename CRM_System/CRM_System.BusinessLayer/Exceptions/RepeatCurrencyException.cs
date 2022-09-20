using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_System.BusinessLayer.Exceptions
{
    public class RepeatCurrencyException : Exception
    {
        public RepeatCurrencyException(string message) : base(message)
        {

        }
    }
}
