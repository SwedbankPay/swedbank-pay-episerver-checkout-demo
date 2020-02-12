using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite
{
    using _ = CreateRefundFramePage;

    public class CreateRefundFramePage : Page<_>
    {
        [FindById("ctl01_tbAmount")]
        public TextInput<_> Amount { get; private set; }

        [FindById("ctl01_btnSave")]
        public Button<_> Confirm { get; private set; }
    }
}
