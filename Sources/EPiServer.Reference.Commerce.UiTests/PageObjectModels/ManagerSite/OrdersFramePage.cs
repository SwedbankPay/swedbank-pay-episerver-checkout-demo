using Atata;
namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite
{
    using _ = OrdersFramePage;

    public class OrdersFramePage : BaseManagerPage<_>
    {
        [FindById("ctl03_MyListView_MainListView_lvTable")]
        public Table<OrderRowItem<_>, _> OrderTable { get; private set; }

        [FindByContent("Delete Selected")]
        public Button<_> DeleteSelected { get; private set; }
    }
}
