using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base
{
    [ControlDefinition("div[contains(concat(' ', normalize-space(@class), ' '), ' c-amounts__input-wrapper ')]//div[contains(concat(' ', normalize-space(@class), ' '), ' c-amounts__icon--error ')]")]
    public class ValidationAmountIcon<TOwner> : Text<TOwner>
        where TOwner : PageObject<TOwner>
    {
    }
}
