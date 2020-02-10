using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base
{
    [ControlDefinition("div[contains(concat(' ', normalize-space(@class), ' '), ' c-text__input-wrapper ')]//div[contains(concat(' ', normalize-space(@class), ' '), ' c-text__icon--error ')]")]
    public class ValidationIcon<TOwner> : Text<TOwner>
        where TOwner : PageObject<TOwner>
    {
    }
}
