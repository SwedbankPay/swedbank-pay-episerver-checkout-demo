using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base.Attributes
{
    public class ControlDefinitionAutomationAttribute : ControlDefinitionAttribute
    {
        public ControlDefinitionAutomationAttribute(string automation) : base($"*[@automation='{automation}']")
        {
        }
    }
}
