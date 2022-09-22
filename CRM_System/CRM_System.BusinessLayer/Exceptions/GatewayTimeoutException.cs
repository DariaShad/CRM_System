using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_System.BusinessLayer.Exceptions
{
    public class GatewayTimeoutException : Exception
    {
        public GatewayTimeoutException(string message) : base(message)
        {

        }
    }
}
