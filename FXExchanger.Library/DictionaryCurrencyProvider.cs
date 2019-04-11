using System.Collections.Generic;

namespace FXExchange.Library
{
    public class DictionaryCurrencyProvider : ICurrencyProvider
    {
        private Dictionary<string, decimal> DKKRates;

        public DictionaryCurrencyProvider()
        {
            DKKRates = new Dictionary<string, decimal>();
            DKKRates.Add("EUR", 743.94M);
            DKKRates.Add("USD", 663.11M);
            DKKRates.Add("GBP", 852.85M);
            DKKRates.Add("SEK", 76.10M);
            DKKRates.Add("NOK", 78.40M);
            DKKRates.Add("CHF", 683.58M);
            DKKRates.Add("JPY", 5.9740M);
            DKKRates.Add("DKK", 100.00M);
        }

        public void CurrencyExists(string currency)
        {
            if (!DKKRates.ContainsKey(currency))
            {
                throw new CurrencyDoesNotExistException($"Currency of type {currency} does not exist!");
            }
        }

        public decimal GetRate(string currency)
        {
            return DKKRates.GetValueOrDefault(currency);
        }
    }
}