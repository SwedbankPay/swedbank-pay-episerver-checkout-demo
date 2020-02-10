using Atata;
using OpenQA.Selenium;
using System;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base
{
    public class ValidationDropdownIconList<TOwner> : ControlList<ValidationDropdownIcon<TOwner>, TOwner>
        where TOwner : PageObject<TOwner>
    {
        public ValidationDropdownIcon<TOwner> this[Func<TOwner, IControl<TOwner>> controlSelector]
        {
            get { return For(controlSelector); }
        }

        public ValidationDropdownIcon<TOwner> For(Func<TOwner, IControl<TOwner>> controlSelector)
        {
            var validationMessageDefinition = UIComponentResolver.GetControlDefinition(typeof(ValidationDropdownIcon<TOwner>));

            IControl<TOwner> boundControl = controlSelector(Component.Owner);

            PlainScopeLocator scopeLocator = new PlainScopeLocator(By.XPath("child::" + validationMessageDefinition.ScopeXPath))
            {
                SearchContext = boundControl.Scope
            };

            return Component.Controls.Create<ValidationDropdownIcon<TOwner>>(boundControl.ComponentName, scopeLocator);
        }
    }
}
