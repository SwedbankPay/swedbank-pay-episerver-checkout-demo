using Atata;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite.Base;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite.Shipment
{
    using _ = ConfirmationShipmentFramePage;

    public class ConfirmationShipmentFramePage : BaseManagerPage<_>
    {
        [FindByContent(TermMatch.Contains, "OK")]
        public Button<_> Confirm { get; private set; }
    }
}
