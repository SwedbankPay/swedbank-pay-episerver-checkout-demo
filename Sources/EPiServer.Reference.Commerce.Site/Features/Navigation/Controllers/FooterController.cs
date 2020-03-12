using System;
using System.Diagnostics;
using System.Reflection;
using EPiServer.Core;
using EPiServer.Reference.Commerce.Site.Features.Navigation.ViewModels;
using EPiServer.Reference.Commerce.Site.Features.Start.Pages;
using EPiServer.SpecializedProperties;
using System.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.Navigation.Controllers
{
    public class FooterController : Controller
    {
        private readonly IContentLoader _contentLoader;
        private static string _pluginVersion;
        private static string _sdkVersion;

        public FooterController(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        [ChildActionOnly]
        public ActionResult Index()
        {
            var viewModel = new FooterViewModel
            {
                FooterLinks = _contentLoader.Get<StartPage>(ContentReference.StartPage).FooterLinks ?? new LinkItemCollection()
            };

            return PartialView(viewModel);
        }

        [ChildActionOnly]
        public ActionResult Version()
        {
            if (string.IsNullOrWhiteSpace(_pluginVersion))
            {
                var pluginAssembly = typeof(SwedbankPay.Episerver.Checkout.SwedbankPayCheckoutService).Assembly;
                var pluginVersionInfo = FileVersionInfo.GetVersionInfo(pluginAssembly.Location);
                _pluginVersion = pluginVersionInfo.ProductVersion;
            }

            if (string.IsNullOrWhiteSpace(_sdkVersion))
            {
                var sdkAssembly = typeof(SwedbankPay.Sdk.SwedbankPayClient).Assembly;
                var sdkVersionInfo = FileVersionInfo.GetVersionInfo(sdkAssembly.Location);
                _sdkVersion = sdkVersionInfo.ProductVersion;
            }

            var versionViewModel = new VersionViewModel
            {
                SdkVersion = _sdkVersion,
                PluginVersion = _pluginVersion
            };
            return PartialView(versionViewModel);
        }
    }
}