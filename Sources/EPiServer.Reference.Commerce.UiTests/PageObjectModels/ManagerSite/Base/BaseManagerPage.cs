using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite.Base
{
    [WaitForDocumentReadyState(Timeout = 10)]
    public abstract class BaseManagerPage<TOwner> : Page<TOwner>
        where TOwner : BaseManagerPage<TOwner>
    {

    }
}
