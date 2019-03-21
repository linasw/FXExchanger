using System.Collections.Generic;

namespace FXExchange.Library
{
    public class FXExchanger
    {
        private IArgumentParser _argumentParser;

        private ICurrencyProvider _currencyProvider;

        public FXExchanger(ICurrencyProvider currencyProvider, IArgumentParser argumentParser)
        {
            _currencyProvider = currencyProvider;
            _argumentParser = argumentParser;
        }

        public Dictionary<string, decimal> _DKK100Rates
        {
            get
            {
                return _currencyProvider.Get100DKKRates();
            }
        }

        public decimal Exchange(string currencyPair, decimal amount)
        {
            (string main, string money) = _argumentParser.ParseCurrencyPair(currencyPair);

            return Exchange(main, money, amount);
        }

        public decimal Exchange(string main, string money, decimal amount)
        {
            CurrencyExists(main, money);

            if (main.Equals(money))
            {
                return amount;
            }

            var mainToDKK = _DKK100Rates.GetValueOrDefault(main);
            var moneyToDKK = _DKK100Rates.GetValueOrDefault(money);
            var mainMoneyRate = mainToDKK / moneyToDKK;
            var toReturn = mainMoneyRate * amount;

            return toReturn;
        }

        private void CurrencyExists(string main, string money)
        {
            if (!(_DKK100Rates.ContainsKey(main)))
            {
                throw new CurrencyDoesNotExistException($"Currency of type {main} does not exist!");
            }

            if (!(_DKK100Rates.ContainsKey(money)))
            {
                throw new CurrencyDoesNotExistException($"Currency of type {money} does not exist!");
            }
        }
    }
}