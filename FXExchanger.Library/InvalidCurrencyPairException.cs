using System;

namespace FXExchange.Library
{
    public class InvalidCurrencyPairException : Exception
    {
        public InvalidCurrencyPairException(string message) : base(message)
        { }
    }
}