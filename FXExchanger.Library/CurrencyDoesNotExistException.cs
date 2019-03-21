using System;

namespace FXExchange.Library
{
    public class CurrencyDoesNotExistException : Exception
    {
        public CurrencyDoesNotExistException(string message) : base(message)
        { }
    }
}