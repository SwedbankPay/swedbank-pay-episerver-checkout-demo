using Atata;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.Payment;
using EPiServer.Reference.Commerce.UiTests.Services;
using EPiServer.Reference.Commerce.UiTests.Tests.Base;
using EPiServer.Reference.Commerce.UiTests.Tests.Helpers;
using NUnit.Framework;
using SwedbankPay.Sdk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EPiServer.Reference.Commerce.UiTests.Tests.PaymentTest
{
    public abstract class PaymentTests : TestBase
    {
        protected string _totalAmount;
        protected string _shippingAmount;
        protected string _currency;
        protected string _orderId;

        protected SwedbankPayClient SwedbankPayClient { get; private set; }

        public PaymentTests(Browsers.Browser browser) : base(browser) { }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            #if DEBUG
            var baseUri = new Uri("https://api.externalintegration.payex.com");
            var bearer = "588431aa485611f8fce876731a1734182ca0c44fcad6b8d989e22f444104aadf"; // ConfigurationManager.AppSettings["payexTestToken"];
            #elif RELEASE
            var baseUri = new Uri(Environment.GetEnvironmentVariable("Payex.Api.Url", EnvironmentVariableTarget.User));
            var bearer = Environment.GetEnvironmentVariable("Payex.Api.Token", EnvironmentVariableTarget.User);
            #endif

            var httpClient = new HttpClient()
            {
                BaseAddress = baseUri
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

            SwedbankPayClient = new SwedbankPayClient(httpClient);
        }

        #region Method Helpers

        public HomeCommercePage GoToCommerceHomePage()
        {
            return GoTo<HomeCommercePage>()
                .Market.Click()
                .MarketSelect.Set("Sweden")
                .PageUrl.WaitTo.Contain("sv");
        }

        public MensProductPage SelectProducts(Product[] products)
        {
            return GoToCommerceHomePage()
                .MensProductPage.Focus()
                .MensProductPage.ClickAndGo()
                .Do(x => 
                {
                    foreach (var product in products)
                    {
                        var index = x.ProductList.IndexOf(y => !products.Any(p => p.Name == y.Name.Value) && (y.Price.IsPresent)).Value;

                        x.ProductList[index].Price.StoreNumericValue(out var price);
                        x.ProductList[index].Name.StoreValue(out var name);

                        product.UnitPrice = price;
                        product.Name = name;

                        x.ProductList[index].Name.Hover();

                        x.ProductList[index].OpenModalWindow.Click()
                        .AddToCart.IsVisible.WaitTo.BeTrue();

                        for(int i= 0; i < product.Quantity; i++)
                        {
                            x.AddToCart.Click();
                        }

                        x.CloseModalWindow.Click();
                    }
                });
        }

        public CheckoutPage GoToCheckoutPage(Product[] products)
        {
            return SelectProducts(products)
                .Cart.Click()
                .ContinueToCheckout.ClickAndGo()
                .PaymentFrame.IsVisible.WaitTo.BeTrue()
                .TotalAmount.StoreAmount(out _totalAmount, out _currency)
                .ShippingAmount.StoreAmount(out _shippingAmount, out _currency);
        }

        public ThankYouPage GoToThankYouPage(Product[] products, PayexInfo payexInfo)
        {
            ThankYouPage page;

            var frame = GoToCheckoutPage(products)
                    .PaymentFrame.SwitchTo<PaymentFramePage>();

            page = payexInfo switch
            {
                PayexCardInfo _ => frame.PerformPaymentWithCard<ThankYouPage>($"{_totalAmount} {_currency}"),
                PayexSwishInfo _ => frame.PerformPaymentWithSwish<ThankYouPage>($"{_totalAmount} {_currency}"),
                _ => frame.PerformPaymentWithCard<ThankYouPage>($"{_totalAmount} {_currency}"),
            };

            return page.ThankYouMessage.IsVisible.WaitTo.Within(15).BeTrue()
                .OrderId.StoreOrderId(out _orderId);
        }

        public HomeManagerPage GoToManagerHomePage()
        {
            return GoTo<HomeManagerPage>();
        }

        public ManagerPage GoToManagerPage()
        {
            return GoTo<HomeManagerPage>()
                .UserName.Set(TestDataService.ManagerUsername)
                .Password.Set(TestDataService.ManagerPassword)
                .Login.ClickAndGo();
        }

        #endregion

        protected static IEnumerable TestData(bool singleProduct = true, string paymentMethod = PaymentMethods.Card)
        {
            var data = new List<object>();

            if (singleProduct)
                data.Add(new[]
                {
                    new Product { Name = "", Quantity = 1 }
                });
            else
                data.Add(new[]
                {
                    new Product { Name = "", Quantity = 3 },
                    new Product { Name = "", Quantity = 2 }
                });

            switch (paymentMethod)
            {
                case PaymentMethods.Card:
                    data.Add(new PayexCardInfo(TestDataService.CreditCardNumber, TestDataService.CreditCardExpiratioDate,
                                               TestDataService.CreditCardCvc));
                    break;

                case PaymentMethods.Swish:
                    data.Add(new PayexSwishInfo(TestDataService.SwishPhoneNumber));
                    break;
            }

            yield return data.ToArray();
        }

    }
}
