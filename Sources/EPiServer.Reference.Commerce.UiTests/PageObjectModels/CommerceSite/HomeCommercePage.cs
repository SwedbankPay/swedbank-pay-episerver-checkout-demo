using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite
{
    using _ = HomeCommercePage;

    #if DEBUG
    [Url("https://payexepiserver001dev.azurewebsites.net/")]
    #elif RELEASE
    [Url("https://payexepiserver001tst.azurewebsites.net/")]
    #endif
    public class HomeCommercePage : BaseCommercePage<_>
    {

    }
}
