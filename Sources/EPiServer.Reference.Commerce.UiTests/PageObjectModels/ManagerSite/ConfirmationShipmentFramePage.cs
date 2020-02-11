using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite
{
    using _ = ConfirmationShipmentFramePage;

    public class ConfirmationShipmentFramePage : BaseManagerPage<_>
    {
        [FindByContent(TermMatch.Contains, "OK")]
        public Button<_> Confirm { get; private set; }
    }
}
