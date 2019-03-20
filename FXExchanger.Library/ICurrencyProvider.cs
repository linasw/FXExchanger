using System.Collections.Generic;

namespace FXExchange.Library
{
    public interface ICurrencyProvider
    {
        Dictionary<string, decimal> Get100DKKRates();
    }
}