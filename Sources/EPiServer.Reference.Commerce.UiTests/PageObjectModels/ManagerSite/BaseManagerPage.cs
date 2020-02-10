using Atata;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite
{
    [WaitForDocumentReadyState(Timeout = 10)]
    public abstract class BaseManagerPage<TOwner> : Page<TOwner>
        where TOwner : BaseManagerPage<TOwner>
    {
        
    }
}
