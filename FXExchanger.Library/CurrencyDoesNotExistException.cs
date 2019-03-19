using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FXExchange.Library
{
    [Serializable]
    public class CurrencyDoesNotExistException : Exception
    {
        public CurrencyDoesNotExistException(string message) : base(message)
        { }

        protected CurrencyDoesNotExistException(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        { }
    }
}
