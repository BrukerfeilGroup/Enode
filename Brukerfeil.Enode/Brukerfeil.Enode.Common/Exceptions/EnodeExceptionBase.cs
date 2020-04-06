using System;
namespace Brukerfeil.Enode.Common.Exceptions
{
    public class EnodeExceptionBase : Exception
    {
        public EnodeExceptionBase()
        {
        }

        public EnodeExceptionBase(string message) : base(message)
        {

        }
        public EnodeExceptionBase(string message, Exception inner) : base(message,inner)
        {

        }
    }
}
