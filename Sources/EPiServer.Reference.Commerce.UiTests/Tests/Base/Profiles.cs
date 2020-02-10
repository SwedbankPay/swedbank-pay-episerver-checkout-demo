using System.Collections;

namespace EPiServer.Reference.Commerce.UiTests.Tests.Base
{
    public class Profiles
    {
        public class ProfileDEV : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { Platforms.Platform.Windows, Devices.Device.NotApplicable,   Browsers.Browser.Chrome};
                yield return new object[] { Platforms.Platform.Windows, Devices.Device.NotApplicable,   Browsers.Browser.Firefox};
            }
        }

        public class ProfileTEST : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { Platforms.Platform.Windows, Devices.Device.NotApplicable,   Browsers.Browser.Chrome };
                yield return new object[] { Platforms.Platform.Windows, Devices.Device.NotApplicable,   Browsers.Browser.Firefox };
                yield return new object[] { Platforms.Platform.Osx,     Devices.Device.NotApplicable,   Browsers.Browser.Safari };
                yield return new object[] { Platforms.Platform.Android, Devices.Device.Android,         Browsers.Browser.Chrome };
                yield return new object[] { Platforms.Platform.Ios,     Devices.Device.Iphone,          Browsers.Browser.Safari };
            }
        }

        public class ProfileINTE : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { Platforms.Platform.Windows, Devices.Device.NotApplicable,   Browsers.Browser.Chrome };
                yield return new object[] { Platforms.Platform.Windows, Devices.Device.NotApplicable,   Browsers.Browser.Firefox };
                yield return new object[] { Platforms.Platform.Osx,     Devices.Device.NotApplicable,   Browsers.Browser.Safari };
                //yield return new object[] { Platforms.Platform.Android, Devices.Device.Android,         Browsers.Browser.Chrome };
                //yield return new object[] { Platforms.Platform.Ios,     Devices.Device.Iphone,          Browsers.Browser.Safari };
            }
        }

        public class ProfilePREP : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { Platforms.Platform.Windows, Devices.Device.NotApplicable,   Browsers.Browser.Chrome };
                yield return new object[] { Platforms.Platform.Windows, Devices.Device.NotApplicable,   Browsers.Browser.Firefox };
                yield return new object[] { Platforms.Platform.Osx,     Devices.Device.NotApplicable,   Browsers.Browser.Safari };
                yield return new object[] { Platforms.Platform.Android, Devices.Device.Android,         Browsers.Browser.Chrome };
                yield return new object[] { Platforms.Platform.Ios,     Devices.Device.Iphone,          Browsers.Browser.Safari };
            }
        }
    }
}
