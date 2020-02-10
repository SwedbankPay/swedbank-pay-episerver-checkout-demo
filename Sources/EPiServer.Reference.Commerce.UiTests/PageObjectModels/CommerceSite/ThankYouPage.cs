using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite
{
    using _ = ThankYouPage;

    public class ThankYouPage : BaseCommercePage<_>
    {
        [FindByContent("Tack för din beställning!")]
        public Text<_> ThankYouMessage { get; private set; }
    }
}
