using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base.Attributes
{
    public class WaitForRotatingIconAttribute : WaitForElementAttribute
    {
        public WaitForRotatingIconAttribute(TriggerEvents on = TriggerEvents.Init)
            : base(WaitBy.Class, "c-icon--rotating", Until.VisibleThenMissingOrHidden, on)
        {
            PresenceTimeout = 3;
            ThrowOnPresenceFailure = false;
            AbsenceTimeout = 10;
            ThrowOnAbsenceFailure = false;
        }
    }
}
