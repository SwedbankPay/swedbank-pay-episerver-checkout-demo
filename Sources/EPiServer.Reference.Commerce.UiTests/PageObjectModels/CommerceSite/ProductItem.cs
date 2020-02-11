using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite
{
    [ControlDefinition(ContainingClass = "jsProductTile", ComponentTypeName = "Product Item")]
    public class ProductItem<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByClass("product-price")]
        public Text<TOwner> Price { get; private set; }

        [FindByClass("product-title")]
        public Text<TOwner> Name { get; private set; }

        [FindByClass("link--black")]
        public Link<ProductPage, TOwner> Link { get; private set; }

        [FindFirst]
        public Button<TOwner> OpenModalWindow { get; private set; }
    }
}
