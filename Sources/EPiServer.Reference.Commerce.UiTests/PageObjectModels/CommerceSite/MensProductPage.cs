﻿using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.CommerceSite
{
    using _ = MensProductPage;

    public class MensProductPage : BaseCommercePage<_>
    {
        [FindByClass("product-list")]
        public ControlList<ProductItem<_>, _> ProductList { get; private set; }

        [Wait(1, TriggerEvents.BeforeClick)]
        [FindByClass("jsAddToCart")]
        public Button<_> AddToCart { get; private set; }

        [Wait(1, TriggerEvents.BeforeClick)]
        [FindByClass("close")]
        public Button<_> CloseModalWindow { get; private set; }

    }
}
