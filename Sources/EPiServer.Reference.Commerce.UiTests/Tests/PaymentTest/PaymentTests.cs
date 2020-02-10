using Atata;
using NUnit.Framework;
using EPiServer.Reference.Commerce.UiTests.Tests.Base;
using EPiServer.Reference.Commerce.UiTests.Tests.Helpers;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.Payment;
using EPiServer.Reference.Commerce.UiTests.Services;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite;

namespace EPiServer.Reference.Commerce.UiTests.Tests.Varv
{
    [Category(TestCategory.Varv)]
    public class PaymentTests : TestBase
    {
        private string _amount;
        private string _currency;

        public PaymentTests(Browsers.Browser browser) : base(browser) { }

        #region Method Helpers

        public HomeCommercePage GoToCommerceHomePage()
        {
            return GoTo<HomeCommercePage>()
                .Market.Click()
                .MarketSelect.Set("Sweden")
                .RefreshPage();
        }

        public MensProductPage GoToMensProductPage()
        {
            return GoTo<HomeCommercePage>()
                .MensProductPage.Focus()
                .MensProductPage.ClickAndGo();
        }

        public ProductPage GoToProductPage()
        {
            return GoToMensProductPage()
                .ProductList.Items[x => x.Price.IsPresent].Link.ClickAndGo();
        }

        public CheckoutPage GoToCheckoutPage()
        {
            return GoToMensProductPage()
                .ProductList.Items[x => x.Price.IsPresent].Link.ClickAndGo()
                .AddToCart.Click()
                .Cart.Click()
                .ContinueToCheckout.ClickAndGo()
                .PaymentFrame.IsVisible.WaitTo.BeTrue()
                .TotalAmount.StoreAmount(out _amount, out _currency);
        }

        public ThankYouPage GoToThankYouPage(string paymentMethod = PaymentMethods.Card)
        {
            ThankYouPage page;

            var frame = GoToCheckoutPage()
                    .PaymentFrame.SwitchTo<PaymentFramePage>();

            switch (paymentMethod)
            {
                case PaymentMethods.Card:
                    page = frame.PerformPaymentWithCard<ThankYouPage>($"{_amount} {_currency}");
                    break;

                case PaymentMethods.Swish:
                    page = frame.PerformPaymentWithSwish<ThankYouPage>($"{_amount} {_currency}");
                    break;

                default:
                    page = frame.PerformPaymentWithCard<ThankYouPage>($"{_amount} {_currency}");
                    break;
            }

            return page.ThankYouMessage.IsVisible.WaitTo.Within(15).BeTrue();
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

        #region Component

        [Category(TestCategory.Component)]
        [Test]
        public void SelectedAmount_Should_Display()
        {
            //GoToThankYouPage(paymentMethod : PaymentMethods.Card);

            GoToManagerPage()
                .OrderManagement.DoubleClick()
                .Today.IsVisible.WaitTo.BeTrue()
                .Today.DoubleClick()
                .OrdersFrame.SwitchTo<OrdersFramePage>()
                .OrderTable.IsVisible.WaitTo.BeTrue()
                .Do(x =>
                {
                    foreach (var row in x.OrderTable.Rows)
                    {
                        row.CheckBox.Check();
                    }
                });
        }

        #endregion

        #region Validations

        [Category(TestCategory.Validation)]
        public void Amount_FreeAmount_ValidationError()
        {
            
        }

        #endregion

        #region Flow

        [Category(TestCategory.Flow)]
        [Test]
        public void SignWithBankId()
        {
        }

        #endregion
    }
}
