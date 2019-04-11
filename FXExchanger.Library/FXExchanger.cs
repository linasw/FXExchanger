using System.Collections.Generic;

namespace FXExchange.Library
{
    public class FXExchanger
    {
        private ICurrencyProvider _currencyProvider;

        public FXExchanger(ICurrencyProvider currencyProvider)
        {
            _currencyProvider = currencyProvider;
        }

        public decimal Exchange(string main, string money, decimal amount)
        {
            CurrencyExists(main, money);

            if (main.Equals(money))
            {
                return amount;
            }

            var mainToDKK = _currencyProvider.GetRate(main);
            var moneyToDKK = _currencyProvider.GetRate(money);
            var mainMoneyRate = mainToDKK / moneyToDKK;
            var toReturn = mainMoneyRate * amount;

            return toReturn;
        }

        private void CurrencyExists(string main, string money)
        {
            _currencyProvider.CurrencyExists(main);
            _currencyProvider.CurrencyExists(money);
        }
    }
}