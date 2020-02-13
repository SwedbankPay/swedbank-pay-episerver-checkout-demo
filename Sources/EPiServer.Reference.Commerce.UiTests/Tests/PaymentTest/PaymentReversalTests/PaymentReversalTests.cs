using NUnit.Framework;
using EPiServer.Reference.Commerce.UiTests.Tests.Base;
using EPiServer.Reference.Commerce.UiTests.Tests.Helpers;
using System.Collections.Generic;
using SwedbankPay.Sdk;
using System.Threading.Tasks;
using System.Linq;

namespace EPiServer.Reference.Commerce.UiTests.Tests.PaymentTest.PaymentReversalTests
{
    public class PaymentReversalTests : PaymentTests
    {
        public PaymentReversalTests(Browsers.Browser browser) : base(browser) { }

        [Test]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Card })]
        public async Task PartialReversal_With_CardAsync(Product[] products, PayexInfo payexInfo)
        {
            // "Credit" is the terminology used in EPiServer to qualify a Reversal
            var expected = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { PaymentColumns.TransactionType, TransactionType.Authorization.ToString()}, { PaymentColumns.Status, PaymentStatus.Processed } },
                new Dictionary<string, string> { { PaymentColumns.TransactionType, TransactionType.Capture.ToString() },      { PaymentColumns.Status, PaymentStatus.Processed } },
                new Dictionary<string, string> { { PaymentColumns.TransactionType, "Credit" },                                { PaymentColumns.Status, PaymentStatus.Processed } },
            };

            GoToThankYouPage(products, payexInfo);

            GoToManagerPage()
                .CreateCapture(_orderId)
                .CreateReversal(_orderId, new Product[] { products[0] }, partial: true)
                .AssertPaymentOrderTransactions(_orderId, expected, out var paymentOrderLink);

            var order = await SwedbankPayClient.PaymentOrder.Get(paymentOrderLink, SwedbankPay.Sdk.PaymentOrders.PaymentOrderExpand.All);

            // Operations
            Assert.That(order.Operations[LinkRelation.CreatePaymentOrderCancel], Is.Null);
            Assert.That(order.Operations[LinkRelation.CreatePaymentOrderCapture], Is.Null);
            Assert.That(order.Operations[LinkRelation.CreatePaymentOrderReversal], Is.Not.Null);
            Assert.That(order.Operations[LinkRelation.PaidPaymentOrder], Is.Not.Null);

            // Transactions
            Assert.That(order.PaymentOrderResponse.CurrentPayment.Payment.Transactions.TransactionList.Count, Is.EqualTo(expected.Count));
            Assert.That(order.PaymentOrderResponse.CurrentPayment.Payment.Transactions.TransactionList.First(x => x.Type == TransactionType.Authorization).State,
                        Is.EqualTo(State.Completed));
            Assert.That(order.PaymentOrderResponse.CurrentPayment.Payment.Transactions.TransactionList.First(x => x.Type == TransactionType.Capture).State,
                        Is.EqualTo(State.Completed));
            Assert.That(order.PaymentOrderResponse.CurrentPayment.Payment.Transactions.TransactionList.First(x => x.Type == TransactionType.Reversal).State,
                        Is.EqualTo(State.Completed));
        }

        [Test]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Card })]
        public async Task FullReversal_With_CardAsync(Product[] products, PayexInfo payexInfo)
        {
            // "Credit" is the terminology used in EPiServer to qualify a Reversal
            var expected = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { PaymentColumns.TransactionType, TransactionType.Authorization.ToString()}, { PaymentColumns.Status, PaymentStatus.Processed } },
                new Dictionary<string, string> { { PaymentColumns.TransactionType, TransactionType.Capture.ToString() },      { PaymentColumns.Status, PaymentStatus.Processed } },
                new Dictionary<string, string> { { PaymentColumns.TransactionType, "Credit" },                                { PaymentColumns.Status, PaymentStatus.Processed } },
            };

            // Arrange
            GoToThankYouPage(products, payexInfo);


            // Act
            GoToManagerPage()
                .CreateCapture(_orderId)
                .CreateReversal(_orderId, products, partial: false)
                .AssertPaymentOrderTransactions(_orderId, expected, out var paymentOrderLink);


            // Assert
            var order = await SwedbankPayClient.PaymentOrder.Get(paymentOrderLink, SwedbankPay.Sdk.PaymentOrders.PaymentOrderExpand.All);

            // Operations
            Assert.That(order.Operations[LinkRelation.CreatePaymentOrderCancel], Is.Null);
            Assert.That(order.Operations[LinkRelation.CreatePaymentOrderCapture], Is.Null);
            Assert.That(order.Operations[LinkRelation.CreatePaymentOrderReversal], Is.Null);
            Assert.That(order.Operations[LinkRelation.PaidPaymentOrder], Is.Not.Null);

            // Transactions
            Assert.That(order.PaymentOrderResponse.CurrentPayment.Payment.Transactions.TransactionList.Count, Is.EqualTo(expected.Count));
            Assert.That(order.PaymentOrderResponse.CurrentPayment.Payment.Transactions.TransactionList.First(x => x.Type == TransactionType.Authorization).State,
                        Is.EqualTo(State.Completed));
            Assert.That(order.PaymentOrderResponse.CurrentPayment.Payment.Transactions.TransactionList.First(x => x.Type == TransactionType.Capture).State,
                        Is.EqualTo(State.Completed));
            Assert.That(order.PaymentOrderResponse.CurrentPayment.Payment.Transactions.TransactionList.First(x => x.Type == TransactionType.Reversal).State,
                        Is.EqualTo(State.Completed));
        }

        [Test]
        [TestCaseSource(nameof(TestData), new object[] { false, PaymentMethods.Swish })]
        public async Task FullReversal_With_SwishAsync(Product[] products, PayexInfo payexInfo)
        {
            // "Credit" is the terminology used in EPiServer to qualify a Reversal
            var expected = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { PaymentColumns.TransactionType, TransactionType.Sale.ToString()}, { PaymentColumns.Status, PaymentStatus.Processed } },
                new Dictionary<string, string> { { PaymentColumns.TransactionType, "Credit" },                       { PaymentColumns.Status, PaymentStatus.Processed } },
            };

            // Arrange
            GoToThankYouPage(products, payexInfo);


            // Act
            GoToManagerPage()
                .CompleteSale(_orderId)
                .CreateReversal(_orderId, products, partial: false)
                .AssertPaymentOrderTransactions(_orderId, expected, out var paymentOrderLink);


            // Assert
            var order = await SwedbankPayClient.PaymentOrder.Get(paymentOrderLink, SwedbankPay.Sdk.PaymentOrders.PaymentOrderExpand.All);

            // Operations
            Assert.That(order.Operations[LinkRelation.CreatePaymentOrderCancel], Is.Null);
            Assert.That(order.Operations[LinkRelation.CreatePaymentOrderCapture], Is.Null);
            Assert.That(order.Operations[LinkRelation.CreatePaymentOrderReversal], Is.Null);
            Assert.That(order.Operations[LinkRelation.PaidPaymentOrder], Is.Not.Null);

            // Transactions
            Assert.That(order.PaymentOrderResponse.CurrentPayment.Payment.Transactions.TransactionList.Count, Is.EqualTo(3));
            Assert.That(order.PaymentOrderResponse.CurrentPayment.Payment.Transactions.TransactionList.First(x => x.Type == TransactionType.Sale).State,
                        Is.EqualTo(State.Completed));
            Assert.That(order.PaymentOrderResponse.CurrentPayment.Payment.Transactions.TransactionList.First(x => x.Type == TransactionType.Reversal).State,
                        Is.EqualTo(State.Completed));
        }

    }
}
