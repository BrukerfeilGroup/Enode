using System;

namespace Brukerfeil.Enode.Common.Exceptions
{
    public class DifiException : EnodeExceptionBase
    {
        public DifiException()
        {

        }

        public DifiException(string message) : base(message)
        {

        }

        public DifiException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
