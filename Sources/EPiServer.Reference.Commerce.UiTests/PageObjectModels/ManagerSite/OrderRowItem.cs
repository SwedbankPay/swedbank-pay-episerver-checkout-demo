﻿using Atata;

namespace EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite
{
    [ControlDefinition("tr", ComponentTypeName = "Row")]
    public class OrderRowItem<TOwner> : TableRow<TOwner> where TOwner : BaseManagerPage<TOwner>
    {
        [FindFirst]
        public CheckBox<TOwner> CheckBox { get; private set; }

        [FindFirst]
        public Link<TOwner> Link { get; private set; }
    }
}