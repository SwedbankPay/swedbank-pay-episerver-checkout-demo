using Atata;
using EPiServer.Reference.Commerce.UiTests.PageObjectModels.ManagerSite;
using EPiServer.Reference.Commerce.UiTests.Services;
using System;
using System.Collections.Generic;

namespace EPiServer.Reference.Commerce.UiTests.Tests.Helpers
{
    public static class ManagerHelper
    {
        public static ManagerPage ExpandOrders(this ManagerPage frame)
        {
            return frame
                .Do(x =>
                {
                    if (x.Today.IsVisible.Value == false)
                    {
                        x
                        .OrderManagement.DoubleClick()
                        .Today.IsVisible.WaitTo.BeTrue();
                    }
                });
        }

        public static ManagerPage ExpandShipmentsAndPickLists(this ManagerPage frame)
        {
            return frame
                .Do(x =>
                {
                    if (x.Shipments.IsVisible.Value == false)
                    {
                        x
                        .ShippingReceiving.DoubleClick()
                        .Shipments.IsVisible.WaitTo.BeTrue();
                    }
                    if (x.ReleasedForShipping.IsVisible.Value == false)
                    {
                        x
                        .Shipments.DoubleClick()
                        .ReleasedForShipping.IsVisible.WaitTo.BeTrue();
                    }
                });
        }

        public static ManagerPage CompleteAndReleaseShipment(this ManagerPage frame, string orderId)
        {
            return frame
                .ExpandOrders()
                .Today.DoubleClick()
                .RightFrame.SwitchTo<OrdersFramePage>()
                .OrderTable.IsVisible.WaitTo.BeTrue()
                .OrderTable.Rows[x => x.Link.Content.Value.Contains(orderId)].Link.ClickAndGo()
                .Details.Click()
                .CompleteShipment.Click()
                .ReleaseShipment.Click()
                .SwitchToRoot<ManagerPage>();
        }

        public static ManagerPage AddShipmentToPickList(this ManagerPage frame, string orderId)
        {
            return frame
                .ExpandShipmentsAndPickLists()
                .ReleasedForShipping.DoubleClick()
                .RightFrame.SwitchTo<ShipmentsFramePage>()
                .OrderTable.Rows[x => x.Link.Content.Value.Contains(orderId)].CheckBox.Check()
                .AddShipmentToPickLlist.Click()
                .ShipmentConfirmationFrame.SwitchTo<ConfirmationShipmentFramePage>()
                .Confirm.Click()
                .Confirm.IsVisible.WaitTo.BeFalse()
                .SwitchToRoot<ManagerPage>();
        }

        public static ManagerPage CompletePickListShipment(this ManagerPage frame, string orderId)
        {
            return frame
                .ExpandShipmentsAndPickLists()
                .PickLists.DoubleClick()
                .RightFrame.SwitchTo<PickListsFramePage>()
                .Do(x =>
                {
                    int count = x.OrderTable.Rows.Count.Value - 1;
                    x.OrderTable.Rows[count].Link.Click();
                })
                .OrderTable.Rows[x => x.Link.Content.Value.Contains(orderId)].CheckBox.Check()
                .CompleteShipment.Click()
                .ShipmentConfirmationFrame.SwitchTo<ConfirmationPickListFramePage>()
                .TrackingNumber.Set("123")
                .Confirm.Click()
                .Confirm.IsVisible.WaitTo.BeFalse()
                .SwitchToRoot<ManagerPage>();
        }
    
        public static ManagerPage OrderShouldContainsPayments(this ManagerPage frame, string orderId, List<Dictionary<string, string>> list, out Uri paymentLink)
        {
            return frame
                .ExpandOrders()
                .Today.DoubleClick()
                .RightFrame.SwitchTo<OrdersFramePage>()
                .OrderTable.IsVisible.WaitTo.BeTrue()
                .OrderTable.Rows[x => x.Link.Content.Value.Contains(orderId)].Link.ClickAndGo()
                .Payments.Click()
                .TablePayment.Rows.Count.Should.Equal(list.Count)
                .Do(x =>
                {
                    foreach (var dictionary in list)
                    {
                        x.TablePayment.Rows[y => y.TransactionType == dictionary[PaymentColumns.TransactionType]].Should.BeVisible();
                        x.TablePayment.Rows[y => y.Status == dictionary[PaymentColumns.Status]].Should.BeVisible();
                    }
                })
                .Summary.Click()
                .PaymentLink.StoreUri(out paymentLink)
                .SwitchToRoot<ManagerPage>();
        }
    }
}
