using System;

namespace WorkTemplate.Croscutting.Contracts.CustomExceptions
{
    public class ExternalServiceException : Exception
    {
        public ExternalServiceException(string message) : base(message)
        {

        }
    }
}
