﻿using System;
using FXExchange.Library;

namespace FXExchange
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: FXExchange <currency pair> <amount to change>");
                return;
            }

            if (args[0].Length != 7)
            {
                Console.WriteLine("<currency pair> length should be 7 (e.g. USD/EUR)");
                return;
            }

            if (!Decimal.TryParse(args[1], out decimal amount) || (amount <= 0))
            {
                Console.WriteLine("<amount to change> should be a positive number");
                return;
            }

            try
            {
                var result = FXExchanger.Exchange(args[0], amount);
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
