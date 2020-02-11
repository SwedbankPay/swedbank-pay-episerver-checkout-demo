using NUnit.Framework;
using EPiServer.Reference.Commerce.UiTests.Tests.Base;
using EPiServer.Reference.Commerce.UiTests.Tests.Helpers;
using System.Collections.Generic;
using SwedbankPay.Sdk;

namespace EPiServer.Reference.Commerce.UiTests.Tests.PaymentTest
{
    public class PaymentSaleTests : PaymentTests
    {
        public PaymentSaleTests(Browsers.Browser browser) : base(browser) { }

        [Test]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Swish })]
        public void Authorization_With_Card(Product[] products, PayexInfo payexInfo)
        {
            GoToThankYouPage(products, payexInfo);

            GoToManagerPage()
                .OrderShouldContainsPayments(_orderId, new List<Dictionary<string, string>> 
                {
                    new Dictionary<string, string> { { PaymentColumns.TransactionType, TransactionType.Sale.ToString() }, { PaymentColumns.Status, PaymentStatus.Processed }}
                },
                out _paymentOrderLink);
        }
    }
}
