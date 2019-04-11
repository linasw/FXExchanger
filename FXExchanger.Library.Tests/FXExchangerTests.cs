using NUnit.Framework;
using System.Collections.Generic;

namespace FXExchange.Library.Tests
{
    [TestFixture]
    public class FXExchangerTests
    {
        private readonly FXExchanger _exchanger;
        private readonly ICurrencyProvider _currencyProvider;

        public FXExchangerTests()
        {
            _exchanger = new FXExchanger(new DictionaryCurrencyProvider());
            _currencyProvider = new DictionaryCurrencyProvider();
        }

        [TestCase("EUR", "EUR", 98.87, ExpectedResult = 98.87)]
        [TestCase("DKK", "DKK", 12.754, ExpectedResult = 12.754)]
        [TestCase("JPY", "JPY", 500000000000.121212, ExpectedResult = 500000000000.121212)]
        public decimal Exchanger_WhenMainAndMoneyAreEqual_ReturnsPassedAmount(string main, string money, decimal amount)
        {
            return _exchanger.Exchange(main, money, amount);
        }

        [Test]
        public void Exchanger_WhenMoneyCurrencyDoesNotExist_ReturnsCurrencyNotFoundException(
            [Values("EUR", "EUR")] string main,
            [Values("PLN", "LTU")] string money,
            [Values(98.87)] decimal amount)
        {
            var exception = Assert.Throws<CurrencyDoesNotExistException>(() => _exchanger.Exchange(main, money, amount));
            Assert.That(exception.Message, Is.EqualTo($"Currency of type {money} does not exist!"));
        }

        [Test]
        public void Exchanger_WhenMainCurrencyDoesNotExist_ReturnsCurrencyNotFoundException(
            [Values("PLN", "LTU")] string main,
            [Values("EUR", "EUR")] string money,
            [Values(98.87)] decimal amount)
        {
            var exception = Assert.Throws<CurrencyDoesNotExistException>(() => _exchanger.Exchange(main, money, amount));
            Assert.That(exception.Message, Is.EqualTo($"Currency of type {main} does not exist!"));
        }

        [Test]
        public void Exchanger_WhenMoneyDKKAndAmount100_ReturnsMainRate(
            [Values("EUR", "USD", "GBP", "SEK", "DKK")] string main,
            [Values("DKK", "DKK", "DKK", "DKK", "DKK")] string money,
            [Values(100)] decimal amount)
        {
            var mainRate = _currencyProvider.GetRate(main);

            var result = _exchanger.Exchange(main, money, amount);

            Assert.AreEqual(result, mainRate);
        }

        [TestCase("EUR", "DKK", 100, ExpectedResult = 743.94)]
        [TestCase("USD", "DKK", 50, ExpectedResult = 331.555)]
        [TestCase("GBP", "DKK", 25, ExpectedResult = 213.2125)]
        [TestCase("SEK", "DKK", 10, ExpectedResult = 7.610)]
        [TestCase("DKK", "DKK", 8, ExpectedResult = 8)]
        public decimal Exchanger_WhenMoneyDKKAndAmountAny_ReturnsExchangedCurrency(
            string main, string money, decimal amount)
        {
            return _exchanger.Exchange(main, money, amount);
        }

        [TestCase("DKK", "EUR", 100)]
        [TestCase("DKK", "USD", 50)]
        [TestCase("DKK", "GBP", 25)]
        [TestCase("DKK", "SEK", 10)]
        [TestCase("DKK", "DKK", 8)]
        public void Exchanger_WhenMainDKKAndAmountAny_ReturnsExchangedCurrency(
            string main, string money, decimal amount)
        {
            var expectedResult = (_currencyProvider.GetRate(main) / _currencyProvider.GetRate(money)) * amount;
            var actualResult = _exchanger.Exchange(main, money, amount);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("EUR", "USD", 100)]
        [TestCase("USD", "EUR", 50)]
        [TestCase("GBP", "JPY", 170)]
        public void Exchanger_WhenMainNotDKKAndMoneyNotDKKAndAmountAny_ReturnsExchangedCurrency(
            string main, string money, decimal amount)
        {
            var expectedResult = (_currencyProvider.GetRate(main) / _currencyProvider.GetRate(money)) * amount;
            var actualResult = _exchanger.Exchange(main, money, amount);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}