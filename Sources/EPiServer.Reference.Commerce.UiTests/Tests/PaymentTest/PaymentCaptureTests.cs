using NUnit.Framework;
using EPiServer.Reference.Commerce.UiTests.Tests.Base;
using EPiServer.Reference.Commerce.UiTests.Tests.Helpers;
using System.Collections.Generic;
using SwedbankPay.Sdk;

namespace EPiServer.Reference.Commerce.UiTests.Tests.PaymentTest
{
    public class PaymentCaptureTests : PaymentTests
    {
        public PaymentCaptureTests(Browsers.Browser browser) : base(browser) { }

        [Test]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Card })]
        public void Capture_With_Card(Product[] products, PayexInfo payexInfo)
        {
            GoToThankYouPage(products, payexInfo);

            GoToManagerPage()
                .CompleteAndReleaseShipment(_orderId)
                .AddShipmentToPickList(_orderId)
                .CompletePickListShipment(_orderId)
                .OrderShouldContainsPayments(_orderId, new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string> { { PaymentColumns.TransactionType, TransactionType.Authorization.ToString() }, { PaymentColumns.Status, PaymentStatus.Processed } },
                    new Dictionary<string, string> { { PaymentColumns.TransactionType, TransactionType.Capture.ToString() },       { PaymentColumns.Status, PaymentStatus.Processed } },
                },
                out _paymentOrderLink);
        }
    }
}
