﻿using System.Collections.Generic;

namespace EPiServer.Reference.Commerce.UiTests.Tests.Base
{
    public class Devices
    {
        public enum Device
        {
            Android,
            Iphone,
            NotApplicable
        }

        public static Dictionary<Device, string> DeviceNames =>
            new Dictionary<Device, string>
            {
                {Device.Android, "Nexus 9"},
                {Device.Iphone, "iPhone"},
                {Device.NotApplicable, null}
            };
    }
}