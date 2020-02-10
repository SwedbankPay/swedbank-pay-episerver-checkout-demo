using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base
{
    [ControlDefinition("span[contains(concat(' ', normalize-space(@class), ' '), ' c-select__icon-error ')]")]
    public class ValidationDropdownIcon<TOwner> : Text<TOwner>
        where TOwner : PageObject<TOwner>
    {
    }
}
