using Atata;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.Payment;
using EPiServer.Reference.Commerce.UiTests.Services;

namespace EPiServer.Reference.Commerce.UiTests.Tests.Helpers
{
    public static class PaymentHelper
    {
        public static T PerformPaymentWithCard<T>(this PaymentFramePage frame, string amount) where T : PageObject<T>
        {
            var page = frame.PaymentMethods[x => x.Name == PaymentMethods.Card].Click()
                .PaymentMethods[x => x.Name == PaymentMethods.Card].PaymentFrame.SwitchTo<PayexCardFramePage>()
                .Pay.Content.Should.ContainAmount(amount)
                .CreditCardNumber.Set(TestDataService.CreditCardNumber)
                .ExpiryDate.Set(TestDataService.CreditCardExpiratioDate)
                .Cvc.Set(TestDataService.CreditCardCvc)
                .Pay.Click();

            // This works on Chrome, but ot on firefox due to : "Error: TypeError: can't access dead object"
            // The Payex add-on on the page is keeping references to the DOM even after the parent document is destroyed
            //.Pay.ClickAndGo<T>();

            AtataContext.Current.Driver.SwitchTo().DefaultContent();

            return page.SwitchToRoot<T>();
        }

        public static T PerformPaymentWithSwish<T>(this PaymentFramePage frame, string amount) where T : PageObject<T>
        {
            var page = frame.PaymentMethods[x => x.Name == PaymentMethods.Swish].Click()
                .PaymentMethods[x => x.Name == PaymentMethods.Swish].PaymentFrame.SwitchTo<PayexSwishFramePage>()
                .Pay.Content.Should.ContainAmount(amount)
                .SwishNumber.Set(TestDataService.SwishPhoneNumber)
                .Pay.Click();

            // This works on Chrome, but ot on firefox due to : "Error: TypeError: can't access dead object"
            // The Payex add-on on the page is keeping references to the DOM even after the parent document is destroyed
            //.Pay.ClickAndGo<T>();

            AtataContext.Current.Driver.SwitchTo().DefaultContent();

            return page.SwitchToRoot<T>();
        }
    }
}
