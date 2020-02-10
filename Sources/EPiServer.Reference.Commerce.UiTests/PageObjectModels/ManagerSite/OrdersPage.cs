namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite
{
    using Atata;
    using _ = OrderFramePage;

    public class OrderFramePage : BaseManagerPage<_>
    {
        [FindByContent("Summary")]
        public ListItem<_> Summary { get; private set; }

        [FindByContent("Payments")]
        public ListItem<_> Details { get; private set; }

        [FindByContent("Payments")]
        public ListItem<_> Payments { get; private set; }

        [FindByContent("Complete Shipment")]
        public Button<_> CompleteShipment { get; private set; }

        [FindByContent("Release Shipment")]
        public Button<_> ReleaseShipment { get; private set; }

        [FindByContent("Cancel Shipment")]
        public Button<_> CancelShipment { get; private set; }

        [FindById("ctl03_xmlStruct_PaymentsGrid_MyListView_MainGrid")]
        public Table<_> TablePayment { get; private set; }
    }
}
