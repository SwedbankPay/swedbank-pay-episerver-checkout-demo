using Atata;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite.Base;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite.Shipment
{
    using _ = ConfirmationPickListFramePage;

    public class ConfirmationPickListFramePage : BaseManagerPage<_>
    {
        [FindById("ctl01_Shipments_TrackingNumber_0")]
        public TextInput<_> TrackingNumber { get; private set; }

        [FindById("ctl01_btnSave")]
        public Clickable<_> Confirm { get; private set; }
    }
}
