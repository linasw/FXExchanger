using System.Collections.Generic;

namespace FXExchange.Library
{
    public interface ICurrencyProvider
    {
        decimal GetRate(string currency);
        void CurrencyExists(string curreny);
    }
}