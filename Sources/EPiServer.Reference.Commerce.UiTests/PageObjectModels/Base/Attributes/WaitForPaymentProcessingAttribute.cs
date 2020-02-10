using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base.Attributes
{
    public class WaitForPaymentProcessingAttribute : WaitForElementAttribute
    {
        public WaitForPaymentProcessingAttribute(TriggerEvents on = TriggerEvents.AfterClick)
            : base(WaitBy.Class, "loader", Until.VisibleThenMissing, on)
        {
            PresenceTimeout = 3;
            ThrowOnPresenceFailure = false;
            AbsenceTimeout = 20;
        }
    }
}
