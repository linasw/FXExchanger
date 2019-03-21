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

            if (!Decimal.TryParse(args[args.Length - 1], out decimal amount) || (amount <= 0))
            {
                Console.WriteLine("<amount to change> should be a positive number");
                return;
            }

            FXExchanger exchanger = new FXExchanger(new CurrencyProvider(), new ArgumentParser());

            try
            {
                decimal result;
                if (args.Length == 2)
                {
                    result = exchanger.Exchange(args[0], amount);
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