using Atata;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite.Base;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite.Order;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite.Shipment
{
    using _ = ShipmentsFramePage;

    public class ShipmentsFramePage : BaseManagerPage<_>
    {
        [FindById("ctl03_MyListView_MainListView_lvTable")]
        public Table<OrderRowItem<_>, _> OrderTable { get; private set; }

        [FindByContent("Add Shipment to Picklist")]
        public Button<_> AddShipmentToPickLlist { get; private set; }

        [FindById("McCommandHandlerFrameContainer_McCommandHandlerFrameIFrame")]
        public Frame<_> ShipmentConfirmationFrame { get; private set; }
    }
}
