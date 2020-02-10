using Atata;
using OpenQA.Selenium;
using System;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.Base
{
    public class UserSelectionIconList<TOwner> : ControlList<UserSelectionIcon<TOwner>, TOwner>
        where TOwner : PageObject<TOwner>
    {
        public UserSelectionIcon<TOwner> this[Func<TOwner, IControl<TOwner>> controlSelector]
        {
            get { return For(controlSelector); }
        }

        public UserSelectionIcon<TOwner> For(Func<TOwner, IControl<TOwner>> controlSelector)
        {
            var validationMessageDefinition = UIComponentResolver.GetControlDefinition(typeof(UserSelectionIcon<TOwner>));

            IControl<TOwner> boundControl = controlSelector(Component.Owner);

            PlainScopeLocator scopeLocator = new PlainScopeLocator(By.XPath("descendant::" + validationMessageDefinition.ScopeXPath))
            {
                SearchContext = boundControl.Scope
            };

            return Component.Controls.Create<UserSelectionIcon<TOwner>>(boundControl.ComponentName, scopeLocator);
        }
    }
}
