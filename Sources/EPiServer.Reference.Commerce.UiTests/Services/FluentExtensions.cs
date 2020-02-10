using Atata;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EPiServer.Reference.Commerce.UiTests.Services
{
    public static class FluentExtensions
    {
        private static readonly Regex RegexAmount = new Regex(@"((\d+)[,.]*[\d]*) (\w+)");
        private static readonly Regex RegexNumericalValue = new Regex(@"([\d]+)");

        public static TOwner StoreValue<TOwner>(this UIComponent<TOwner> component, out string value)
            where TOwner : PageObject<TOwner>
        {
            value = component is TextInput<TOwner> input ? input.Value : component.Content.Value;

            return component.Owner;
        }

        public static TOwner StoreAmount<TOwner>(this UIComponent<TOwner> component, out string amount, out string currency)
            where TOwner : PageObject<TOwner>
        {
            string value = component is TextInput<TOwner> input ? input.Value : component.Content.Value;

            var result = RegexAmount.Match(value);

            amount = result.Groups[1].Value;
            currency = result.Groups[3].Value;

            return component.Owner;
        }

        public static TOwner StoreCurrency<TOwner>(this UIComponent<TOwner> component, out string currency)
            where TOwner : PageObject<TOwner>
        {
            var result = RegexAmount.Match(component.Content.Value);
            currency = result.Groups[3].Value;

            return component.Owner;
        }

        public static TOwner StoreNumericValue<TOwner>(this UIComponent<TOwner> component, out int value)
            where TOwner : PageObject<TOwner>
        {
            string tmp = component is TextInput<TOwner> input ? input.Value : component.Content.Value;

            value = int.Parse(RegexNumericalValue.Match(tmp).Value);

            return component.Owner;
        }

        public static TOwner ContainAmount<TOwner>(this IDataVerificationProvider<string, TOwner> should, string expected, string format = "{0:N2}") where TOwner : PageObject<TOwner>
        {
            var actual = should.DataProvider.Value;

            var actualResult = RegexAmount.Match(actual);
            string actualValue = actualResult.Groups[1].Value;
            string actualCurrency = actualResult.Groups[3].Value;

            var actualAmount = $"{string.Format(format, Convert.ToDecimal(actualValue.Replace(",", "."), new CultureInfo("en-US")))} {actualCurrency}";

            var expectedResult = RegexAmount.Match(expected);
            string expectedValue = expectedResult.Groups[1].Value;
            string expectedCurrency = expectedResult.Groups[3].Value;

            var expectedAmount = $"{string.Format(format, Convert.ToDecimal(expectedValue.Replace(",", "."), new CultureInfo("en-US")))} {expectedCurrency}";

            Assert.AreEqual(expectedAmount, actualAmount);

            return should.Owner;
        }

    }
}
