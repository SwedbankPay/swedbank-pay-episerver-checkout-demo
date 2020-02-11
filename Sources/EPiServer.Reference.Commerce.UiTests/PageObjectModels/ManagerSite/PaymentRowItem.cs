using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite
{
    [ControlDefinition("tr", ComponentTypeName = "Row")]
    public class PaymentRowItem<TOwner> : TableRow<TOwner> where TOwner : BaseManagerPage<TOwner>
    {
        [FindFirst]
        public CheckBox<TOwner> CheckBox { get; private set; }

        [FindByClass("serverGridInner", Index = 2)]
        public Text<TOwner> Name { get; private set; }

        [FindByClass("serverGridInner", Index = 3)]
        public Text<TOwner> TransactionType { get; private set; }

        [FindByClass("serverGridInner", Index = 4)]
        public Text<TOwner> Amount { get; private set; }

        [FindByClass("serverGridInner", Index = 5)]
        public Text<TOwner> Status { get; private set; }


    }
}