using FXExchange.Library;
using System;

namespace FXExchange
{
    public class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 2 && args.Length != 3)
            {
                Console.WriteLine("Usage: FXExchange <currency pair> <amount to change>");
                return;
            }

            FXExchanger exchanger = new FXExchanger(new DictionaryCurrencyProvider());
            ArgumentParser argumentParser = new ArgumentParser();

            try
            {
                decimal result, amount;
                string main, money;

                amount = argumentParser.ParseAmount(args[1]);

                if (args.Length == 2)
                {
                    (main, money) = argumentParser.ParseCurrencyPair(args[0]);
                    result = exchanger.Exchange(main, money, amount);
                }
                else
                {
                    result = exchanger.Exchange(args[0], args[1], amount);
                }

                Console.WriteLine(result.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return;
        }
    }
}