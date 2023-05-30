using System;


namespace ExceptionNS
{

    public class HandymanException : Exception
    {
        public HandymanException(string message) : base(message)
        {
        }

        public HandymanException(string message, Exception t) : base(message, t)
        {
        }
    }

    public class AbortException : HandymanException
    {
        public AbortException(string message) : base(message)
        {
        }
    }

}