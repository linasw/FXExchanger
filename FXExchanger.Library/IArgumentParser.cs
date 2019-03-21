namespace FXExchange.Library
{
    public interface IArgumentParser
    {
        (string main, string money) ParseCurrencyPair(string currencyPair);
    }
}