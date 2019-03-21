using System;

namespace FXExchange.Library
{
    public class AmountShouldBePositiveNumberException : Exception
    {
        public AmountShouldBePositiveNumberException(string message) : base(message)
        { }
    }
}