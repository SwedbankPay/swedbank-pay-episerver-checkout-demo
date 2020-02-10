using Atata;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base.Attributes;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Payment
{
    using _ = PayexCardFramePage;

    [WaitForLoader]
    public class PayexCardFramePage : Page<_>
    {
        [FindById("panInput")] 
        public TelInput<_> CreditCardNumber { get; set; }

        [FindById(TermMatch.Contains, "cvcInput")]
        public TelInput<_> Cvc { get; set; }

        [FindById("expiryInput")] public TelInput<_> ExpiryDate { get; set; }

        [FindById("px-submit")] public Button<_> Pay { get; set; }

        public ValidationIconList<_> ValidationIcons { get; set; }
    }
}