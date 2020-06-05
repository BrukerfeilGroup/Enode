using System;
using System.Collections.Generic;
using System.Text;

namespace Brukerfeil.Enode.Common.Exceptions
{
    public class ServiceUnavailableException : EnodeExceptionBase
    {
        public ServiceUnavailableException()
        {

        }

        public ServiceUnavailableException(string message) : base(message)
        {

        }

        public ServiceUnavailableException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
