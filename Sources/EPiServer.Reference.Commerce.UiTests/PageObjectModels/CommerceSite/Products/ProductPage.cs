using Atata;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite.Base;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite.Products
{
    using _ = ProductPage;

    public class ProductPage : BaseCommercePage<_>
    {
        [Wait(1, TriggerEvents.BeforeClick)]
        [FindByClass("jsAddToCart")]
        public Button<_> AddToCart { get; private set; }
    }
}
