using Atata;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base.Attributes;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Payment
{
    using _ = PaymentFramePage;

    [WaitForLoader]
    public class PaymentFramePage : BaseCommercePage<_>
    {
        [Wait(1, TriggerEvents.BeforeClick)]
        [FindByClass("paymentmenu-container")]
        public ControlList<PayexItem<_>, _> PaymentMethods { get; set; }

    }
}