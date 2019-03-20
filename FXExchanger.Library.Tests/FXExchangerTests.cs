using NUnit.Framework;
using System.Collections.Generic;

namespace FXExchange.Library.Tests
{
    [TestFixture]
    public class FXExchangerTests
    {
        private readonly FXExchanger _exchanger;

        public FXExchangerTests()
        {
            _exchanger = new FXExchanger(new CurrencyProvider());
        }

        [Test]
        public void Exchanger_WhenMainAndMoneyAreEqual_ReturnsPassedAmount(
            [Values("EUR/EUR", "DKK/DKK", "JPY/JPY")] string currencyPair,
            [Values(98.87, 12.754, 50.0)] decimal amount)
        {
            decimal exhangedAmount = _exchanger.Exchange(currencyPair, amount);
            Assert.AreEqual(exhangedAmount, amount);
        }

        [Test]
        public void Exchanger_WhenMoneyCurrencyDoesNotExist_ReturnsCurrencyNotFoundException(
            [Values("EUR/PLN", "EUR/LTU")] string currencyPair,
            [Values(98.87)] decimal amount)
        {
            string money = currencyPair.Substring(4, 3);
            var exception = Assert.Throws<CurrencyDoesNotExistException>(() => _exchanger.Exchange(currencyPair, amount));
            Assert.That(exception.Message, Is.EqualTo($"Currency of type {money} does not exist!"));
        }

        [Test]
        public void Exchanger_WhenMainCurrencyDoesNotExist_ReturnsCurrencyNotFoundException(
            [Values("PLN/EUR", "LTU/EUR")] string currencyPair,
            [Values(98.87)] decimal amount)
        {
            string main = currencyPair.Substring(0, 3);
            var exception = Assert.Throws<CurrencyDoesNotExistException>(() => _exchanger.Exchange(currencyPair, amount));
            Assert.That(exception.Message, Is.EqualTo($"Currency of type {main} does not exist!"));
        }

        [Test]
        public void Exchanger_WhenMoneyDKKAndAmount100_ReturnsMainRate(
            [Values("EUR/DKK", "USD/DKK", "GBP/DKK", "SEK/DKK", "DKK/DKK")] string currencyPair,
            [Values(100)] decimal amount)
        {
            string main = currencyPair.Substring(0, 3);
            Dictionary<string, decimal> rates = _exchanger._DKK100Rates;

            var result = _exchanger.Exchange(currencyPair, amount);

            Assert.AreEqual(result, rates.GetValueOrDefault(main));
        }

        [TestCase("EUR/DKK", 100, ExpectedResult = 743.94)]
        [TestCase("USD/DKK", 50, ExpectedResult = 331.555)]
        [TestCase("GBP/DKK", 25, ExpectedResult = 213.2125)]
        [TestCase("SEK/DKK", 10, ExpectedResult = 7.610)]
        [TestCase("DKK/DKK", 8, ExpectedResult = 8)]
        public decimal Exchanger_WhenMoneyDKKAndAmountAny_ReturnsExchangedCurrency(
            string currencyPair, decimal amount)
        {
            return _exchanger.Exchange(currencyPair, amount);
        }

        [TestCase("DKK/EUR", 100, ExpectedResult = 0.001344)]
        [TestCase("DKK/USD", 50, ExpectedResult = 0.000754)]
        [TestCase("DKK/GBP", 25, ExpectedResult = 0.000293)]
        [TestCase("DKK/SEK", 10, ExpectedResult = 0.001314)]
        [TestCase("DKK/DKK", 8, ExpectedResult = 8)]
        public decimal Exchanger_WhenMainDKKAndAmountAny_ReturnsExchangedCurrency(
            string currencyPair, decimal amount)
        {
            return _exchanger.Exchange(currencyPair, amount);
        }

        [TestCase("EUR/USD", 100, ExpectedResult = 112.189531)]
        [TestCase("USD/EUR", 50, ExpectedResult = 44.567438)]
        [TestCase("GBP/JPY", 170, ExpectedResult = 24269.250084)]
        public decimal Exchanger_WhenMainNotDKKAndMoneyNotDKKAndAmountAny_ReturnsExchangedCurrency(
            string currencyPair, decimal amount)
        {
            return _exchanger.Exchange(currencyPair, amount);
        }
    }
}