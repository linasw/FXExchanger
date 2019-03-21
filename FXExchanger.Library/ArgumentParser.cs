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
                throw new InvalidCurrencyPairException("<currency pair> should incluse two currencies");
            }
            return (splits[0], splits[1]);
        }
    }
}