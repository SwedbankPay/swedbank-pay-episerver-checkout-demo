using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite
{
    using _ = ProductPage;

    public class ProductPage : BaseCommercePage<_>
    {
        [Wait(1, TriggerEvents.BeforeClick)]
        [FindByClass("jsAddToCart")]
        public Button<_> AddToCart { get; private set; }
    }
}
