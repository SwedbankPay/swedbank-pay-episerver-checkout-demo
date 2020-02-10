using Atata;
using OpenQA.Selenium;
using System;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base
{
    public class UserSelectionAmountIconList<TOwner> : ControlList<UserSelectionAmountIcon<TOwner>, TOwner>
        where TOwner : PageObject<TOwner>
    {
        public UserSelectionAmountIcon<TOwner> this[Func<TOwner, IControl<TOwner>> controlSelector]
        {
            get { return For(controlSelector); }
        }

        public UserSelectionAmountIcon<TOwner> For(Func<TOwner, IControl<TOwner>> controlSelector)
        {
            var validationMessageDefinition = UIComponentResolver.GetControlDefinition(typeof(UserSelectionAmountIcon<TOwner>));

            IControl<TOwner> boundControl = controlSelector(Component.Owner);

            PlainScopeLocator scopeLocator = new PlainScopeLocator(By.XPath("descendant::" + validationMessageDefinition.ScopeXPath))
            {
                SearchContext = boundControl.Scope
            };

            return Component.Controls.Create<UserSelectionAmountIcon<TOwner>>(boundControl.ComponentName, scopeLocator);
        }
    }
}
