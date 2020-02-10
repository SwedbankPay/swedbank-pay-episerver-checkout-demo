using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite
{
    using _ = ManagerPage;

    public class ManagerPage : BaseManagerPage<_>
    {
        [FindByContent("Order Management")]
        public Clickable<_> OrderManagement { get; private set; }

        [Wait(3, TriggerEvents.AfterClick)]
        [FindByContent("Today")]
        public Clickable<_> Today { get; private set; }

        [FindById("right")]
        public Frame<_> OrdersFrame { get; private set; }

        [FindByContent("Shipping/Receiving")]
        public Clickable<_> ShippingReceiving { get; private set; }

        [FindByContent("Released for Shipping")]
        public Clickable<_> ReleaseForShipping { get; private set; }
    }
}
