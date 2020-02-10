using Atata;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base.Attributes;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Payment
{
    using _ = PayexSwishFramePage;

    [WaitForLoader]
    public class PayexSwishFramePage : Page<_>
    {
        [Wait(1, TriggerEvents.BeforeClick)]
        [FindById("px-submit")]
        public Button<_> Pay { get; set; }

        [FindById("msisdnInput")] 
        public TelInput<_> SwishNumber { get; set; }
    }
}