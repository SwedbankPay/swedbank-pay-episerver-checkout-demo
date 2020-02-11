using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite
{
    using _ = CheckoutPage;

    public class CheckoutPage : BaseCommercePage<_>
    {
        [FindById("paymentMenuFrame")]
        public Frame<_> PaymentFrame { get; private set; }

        [FindByClass("total-price", Index = 3)]
        public H4<_> ShippingAmount { get; private set; }

        [FindByClass("total-price", Index = 3)]
        public H4<_> TotalAmount { get; private set; }
    }
}
