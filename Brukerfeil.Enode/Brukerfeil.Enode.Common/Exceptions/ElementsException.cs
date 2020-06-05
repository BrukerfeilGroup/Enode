using System;
using System.Collections.Generic;
using System.Text;

namespace Brukerfeil.Enode.Common.Exceptions
{
    public class ElementsException : EnodeExceptionBase
    {
        public ElementsException()
        {

        }

        public ElementsException(string message) : base(message)
        {

        }

        public ElementsException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
