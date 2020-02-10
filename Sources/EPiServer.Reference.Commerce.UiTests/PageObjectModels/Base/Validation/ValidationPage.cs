using EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base
{
    public class ValidationPage<TOwner> : BaseManagerPage<TOwner> where TOwner : BaseManagerPage<TOwner> 
    {
        public ValidationMessageList<TOwner> ValidationMessages { get; private set; }

        public ValidationIconList<TOwner> ValidationIcons { get; private set; }

        public ValidationAmountIconList<TOwner> ValidationAmountIcons { get; private set; }

        public ValidationDropdownIconList<TOwner> ValidationDropdownIcons { get; private set; }

    }
}
