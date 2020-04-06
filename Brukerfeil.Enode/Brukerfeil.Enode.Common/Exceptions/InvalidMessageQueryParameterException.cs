using System;
namespace Brukerfeil.Enode.Common.Exceptions
{
    public class InvalidMessageQueryParameterException : EnodeExceptionBase
    {

        public InvalidMessageQueryParameterException(string message) : base(message)
        {
        }

        public InvalidMessageQueryParameterException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
