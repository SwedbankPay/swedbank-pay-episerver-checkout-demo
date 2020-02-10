using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base
{
    [ControlDefinition("span[@class = 'c-btn-radio__icon']")]
    public class UserSelectionIcon<TOwner> : Control<TOwner>
        where TOwner : PageObject<TOwner>
    {
    }
}
