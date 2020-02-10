using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base
{
    [ControlDefinition("span[@class = 'c-amounts__icon']")]
    public class UserSelectionAmountIcon<TOwner> : Control<TOwner>
        where TOwner : PageObject<TOwner>
    {
    }
}
