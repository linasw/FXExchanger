using System;
using System.Runtime.Serialization;

namespace FXExchange.Library
{
    public class CurrencyDoesNotExistException : Exception
    {
        public CurrencyDoesNotExistException(string message) : base(message)
        { }
    }
}