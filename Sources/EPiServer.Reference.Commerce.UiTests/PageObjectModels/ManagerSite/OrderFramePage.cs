using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite
{
    using _ = OrderFramePage;

    public class OrderFramePage : BaseManagerPage<_>
    {
        #region Tabs

        [Wait(1, TriggerEvents.BeforeClick)]
        [FindByContent("Summary")]
        public ListItem<_> Summary { get; private set; }

        [Wait(1, TriggerEvents.BeforeClick)]
        [FindByContent("Details")]
        public ListItem<_> Details { get; private set; }

        [Wait(1, TriggerEvents.BeforeClick)]
        [FindByContent("Payments")]
        public ListItem<_> Payments { get; private set; }

        #endregion Tabs

        #region Summary

        [FindByXPath("//span[contains(text(),'/psp/paymentorders/')]")]
        public Text<_> PaymentLink { get; private set; }

        #endregion

        #region Details

        [FindByContent("Complete Shipment")]
        public Button<_> CompleteShipment { get; private set; }

        [FindByContent("Release Shipment")]
        public Button<_> ReleaseShipment { get; private set; }

        [FindByContent("Cancel Shipment")]
        public Button<_> CancelShipment { get; private set; }

        #endregion

        #region Payment

        [FindById("ctl03_xmlStruct_PaymentsGrid_MyListView_MainGrid")]
        public Table<PaymentRowItem<_>,_> TablePayment { get; private set; }

        #endregion
    }
}
