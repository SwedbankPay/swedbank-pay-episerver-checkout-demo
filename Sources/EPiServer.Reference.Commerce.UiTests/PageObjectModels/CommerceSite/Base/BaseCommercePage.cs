using Atata;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite.Checkout;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite.Products;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite.Base
{
    [WaitForDocumentReadyState(Timeout = 10)]
    public abstract class BaseCommercePage<TOwner> : Page<TOwner>
        where TOwner : BaseCommercePage<TOwner>
    {
        [FindByXPath("//li[@class='dropdown'][2]")]
        public Link<TOwner> Market { get; private set; }

        [Wait(1, TriggerEvents.AfterSet)]
        [FindById("MarketId")]
        public Select<TOwner> MarketSelect { get; private set; }

        [FindByContent("Mens", "Män")]
        public Link<MensProductPage, TOwner> MensProductPage { get; private set; }

        [Wait(1, TriggerEvents.BeforeClick)]
        [FindByClass("btn-cart")]
        public Button<TOwner> Cart { get; private set; }

        [Wait(1, TriggerEvents.BeforeClick)]
        [FindByContent("Fortsätt till utcheckningen", "Proceed to checkout")]
        public Button<CheckoutPage, TOwner> ContinueToCheckout { get; private set; }

    }
}
