using EPiServer.Commerce.Order;
using EPiServer.Framework.Localization;
using EPiServer.Reference.Commerce.Site.Features.Cart.Services;
using EPiServer.Reference.Commerce.Site.Features.Market.Services;
using EPiServer.Reference.Commerce.Site.Features.Payment.Services;
using EPiServer.ServiceLocation;

using Mediachase.Commerce.Markets;
using Mediachase.Commerce.Orders;

using SwedbankPay.Episerver.Checkout;
using SwedbankPay.Episerver.Checkout.Common;
using SwedbankPay.Sdk.PaymentOrders;

using System;
using System.ComponentModel;
using SwedbankPay.Sdk;
using PaymentType = Mediachase.Commerce.Orders.PaymentType;
using TransactionType = Mediachase.Commerce.Orders.TransactionType;

namespace EPiServer.Reference.Commerce.Site.Features.Payment.PaymentMethods
{

    [ServiceConfiguration(typeof(IPaymentMethod))]
    public class SwedbankPayCheckoutPaymentMethod : PaymentMethodBase, IDataErrorInfo
    {
        private readonly IOrderGroupFactory _orderGroupFactory;
        private readonly LanguageService _languageService;
        private readonly ICartService _cartService;
        private readonly IMarketService _marketService;
        private readonly ISwedbankPayCheckoutService _swedbankPayCheckoutService;
        private bool _isInitalized;

        public SwedbankPayCheckoutPaymentMethod()
            : this(
                LocalizationService.Current,
                ServiceLocator.Current.GetInstance<IOrderGroupFactory>(),
                ServiceLocator.Current.GetInstance<LanguageService>(),
                ServiceLocator.Current.GetInstance<IPaymentManagerFacade>(),
                ServiceLocator.Current.GetInstance<ICartService>(),
                ServiceLocator.Current.GetInstance<IMarketService>(),
                ServiceLocator.Current.GetInstance<ISwedbankPayCheckoutService>())
        {
        }

        public SwedbankPayCheckoutPaymentMethod(
            LocalizationService localizationService,
            IOrderGroupFactory orderGroupFactory,
            LanguageService languageService,
            IPaymentManagerFacade paymentManager,
            ICartService cartService,
            IMarketService marketService,
            ISwedbankPayCheckoutService swedbankPayCheckoutService)
            : base(localizationService, orderGroupFactory, languageService, paymentManager)
        {
            _orderGroupFactory = orderGroupFactory;
            _languageService = languageService;
            _cartService = cartService;
            _marketService = marketService;
            _swedbankPayCheckoutService = swedbankPayCheckoutService;
        }

        public void InitializeValues()
        {
            InitializeValues(_cartService.DefaultCartName);
        }

        public void InitializeValues(string cartName)
        {
            if (_isInitalized)
            {
                return;
            }

            var cart = _cartService.LoadCart(cartName);
            var market = _marketService.GetMarket(cart.MarketId);

            CheckoutConfiguration = _swedbankPayCheckoutService.LoadCheckoutConfiguration(market);
            Culture = _languageService.GetCurrentLanguage().TextInfo.CultureName;

            var orderId = cart.Properties[Constants.SwedbankPayCheckoutOrderIdCartField]?.ToString();
            if (!string.IsNullOrWhiteSpace(orderId) || CheckoutConfiguration.UseAnonymousCheckout)
            {
                GetCheckoutJavascriptSource(cart, $"description cart {cart.OrderLink.OrderGroupId}");
            }
            else
            {
                GetCheckInJavascriptSource(cart);
            }

            _isInitalized = true;
        }

        private void GetCheckoutJavascriptSource(ICart cart, string description)
        {
            var orderData = _swedbankPayCheckoutService.CreateOrUpdatePaymentOrder(cart, description);
            JavascriptSource = orderData.Operations.View?.Href;
            UseCheckoutSource = true;
        }

        private void GetCheckInJavascriptSource(ICart cart)
        {
            string email = "PayexTester@payex.com";
            string phone = "+46739000001";
            string ssn = "199710202392";

            var orderData = _swedbankPayCheckoutService.InitiateConsumerSession(_languageService.GetCurrentLanguage(), email, phone, ssn);
            JavascriptSource = orderData.Operations.ViewConsumerIdentification?.Href;
        }

        public override IPayment CreatePayment(decimal amount, IOrderGroup orderGroup)
        {
            var paymentOrder = _swedbankPayCheckoutService.GetPaymentOrder(orderGroup, PaymentOrderExpand.All);
            var currentPayment = paymentOrder.PaymentOrderResponse.CurrentPayment.Payment;

            var payment = orderGroup.CreatePayment(_orderGroupFactory);
            payment.PaymentType = PaymentType.Other;
            payment.PaymentMethodId = PaymentMethodId;
            payment.PaymentMethodName = Constants.SwedbankPayCheckoutSystemKeyword;
            payment.Amount = amount;
            var isSwishPayment = currentPayment.Instrument.Equals(PaymentInstrument.Swish);
            payment.Status = isSwishPayment ? PaymentStatus.Processed.ToString() : PaymentStatus.Pending.ToString();
            payment.TransactionType = isSwishPayment ? TransactionType.Sale.ToString() : TransactionType.Authorization.ToString();
            return payment;
        }

        public override bool ValidateData()
        {
            return true;
        }

        public override string SystemKeyword => Constants.SwedbankPayCheckoutSystemKeyword;

        public string this[string columnName] => string.Empty;

        public string Error { get; }
        public CheckoutConfiguration CheckoutConfiguration { get; set; }
        public string Culture { get; set; }
        public Uri JavascriptSource { get; set; }
        public bool UseCheckoutSource { get; set; }
    }
}
