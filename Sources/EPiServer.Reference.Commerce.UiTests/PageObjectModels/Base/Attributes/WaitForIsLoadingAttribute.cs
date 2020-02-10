using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base.Attributes
{
    public class WaitForIsLoadingAttribute : WaitForElementAttribute
    {
        public WaitForIsLoadingAttribute(TriggerEvents on = TriggerEvents.Init)
            : base(WaitBy.Class, "is-loading", Until.VisibleThenMissingOrHidden, on)
        {
            PresenceTimeout = 3;
            ThrowOnPresenceFailure = false;
            AbsenceTimeout = 20;
        }
    }

}
