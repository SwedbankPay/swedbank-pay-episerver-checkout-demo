using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite
{
    using _ = MensProductPage;

    public class MensProductPage : BaseCommercePage<_>
    {
        [FindByClass("product-list")]
        public ItemsControl<ProductItem<_>, _> ProductList { get; private set; }
    }
}
