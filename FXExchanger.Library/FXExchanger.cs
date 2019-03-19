using FXExchange.Library;
using System;
using System.Collections.Generic;

namespace FXExchange.Library
{
    public class FXExchanger
    {
        public static Dictionary<string, decimal> _DKK100Rates
        {
            get
            {
                return Get100DKKRates();
            }
        }

        public static decimal Exchange(string currencyPair, decimal amount)
        {
            string main = currencyPair.Substring(0, 3);
            string money = currencyPair.Substring(4, 3);

            if (!(_DKK100Rates.ContainsKey(main)))
            {
                throw new CurrencyDoesNotExistException($"Currency of type {main} does not exist!");
            }

            if (!(_DKK100Rates.ContainsKey(money)))
            {
                throw new CurrencyDoesNotExistException($"Currency of type {money} does not exist!");
            }

            return Exchange(main, money, amount);
        }

        private static decimal Exchange(string main, string money, decimal amount)
        {
            if (main.Equals(money))
            {
                return amount;
            }

            if (money.Equals("DKK"))
            {
                var calculatedAmount = _DKK100Rates.GetValueOrDefault(main) * (amount / 100);
                return decimal.Round(calculatedAmount, 6);
            }

            if (main.Equals("DKK"))
            {
                var valueOfMoney = _DKK100Rates.GetValueOrDefault(money);
                var calculatedAmount = Decimal.Divide(1, valueOfMoney) * (amount / 100);
                return decimal.Round(calculatedAmount, 6);
            }

            var mainToDKK = _DKK100Rates.GetValueOrDefault(main);
            var moneyToDKK = _DKK100Rates.GetValueOrDefault(money);
            var mainMoneyRate = mainToDKK / moneyToDKK;
            var toReturn = mainMoneyRate * amount;

            return decimal.Round(toReturn, 6);
        }

        private static Dictionary<string, decimal> Get100DKKRates()
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
