using System;
using System.Text.RegularExpressions;

namespace FXExchange.Library
{
    public class ArgumentParser : IArgumentParser
    {
        public (string main, string money) ParseCurrencyPair(string currencyPair)
        {
            var splits = Regex.Split(currencyPair, @"\W+");
            if (splits.Length != 2)
            {
                throw new InvalidCurrencyPairException("<currency pair> should include two currencies");
            }
            return (splits[0], splits[1]);
        }

        public decimal ParseAmount(string amount)
        {
            if (!Decimal.TryParse(amount, out decimal parsedAmount) || (parsedAmount <= 0))
            {
                throw new AmountShouldBePositiveNumberException("<amount to change> should be a positive number");
            }

            return parsedAmount;
        }
    }
}