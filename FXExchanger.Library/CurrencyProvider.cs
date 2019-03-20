using System.Collections.Generic;

namespace FXExchange.Library
{
    public class CurrencyProvider : ICurrencyProvider
    {
        public Dictionary<string, decimal> Get100DKKRates()
        {
            Dictionary<string, decimal> temp = new Dictionary<string, decimal>();
            temp.Add("EUR", 743.94M);
            temp.Add("USD", 663.11M);
            temp.Add("GBP", 852.85M);
            temp.Add("SEK", 76.10M);
            temp.Add("NOK", 78.40M);
            temp.Add("CHF", 683.58M);
            temp.Add("JPY", 5.9740M);
            temp.Add("DKK", 100.00M);

            return temp;
        }
    }
}