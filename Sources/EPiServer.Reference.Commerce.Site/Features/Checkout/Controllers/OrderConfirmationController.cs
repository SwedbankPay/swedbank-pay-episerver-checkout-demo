using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.Editor;
using EPiServer.Reference.Commerce.Site.Features.AddressBook.Services;
using EPiServer.Reference.Commerce.Site.Features.Cart.Services;
using EPiServer.Reference.Commerce.Site.Features.Checkout.Pages;
using EPiServer.Reference.Commerce.Site.Features.Checkout.Services;
using EPiServer.Reference.Commerce.Site.Features.Recommendations.Services;
using EPiServer.Reference.Commerce.Site.Infrastructure.Facades;
using EPiServer.Web.Mvc.Html;

using Mediachase.Commerce.Markets;

using SwedbankPay.Episerver.Checkout;
using SwedbankPay.Episerver.Checkout.Common;

using System.Threading.Tasks;
using System.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.Checkout.Controllers
{
    public class OrderConfirmationController : OrderConfirmationControllerBase<OrderConfirmationPage>
    {
        private readonly CheckoutService _checkoutService;
        private readonly ICartService _cartService;
        private readonly IOrderRepository _orderRepository;
        private readonly IRecommendationService _recommendationService;
        private readonly ISwedbankPayCheckoutService _swedbankPayCheckoutService;

        public OrderConfirmationController(
            ConfirmationService confirmationService,
            AddressBookService addressBookService,
            CustomerContextFacade customerContextFacade,
            CheckoutService checkoutService,
            ICartService cartService,
            IOrderGroupCalculator orderGroupCalculator,
            IOrderRepository orderRepository,
            IMarketService marketService,
            IRecommendationService recommendationService,
            ISwedbankPayCheckoutService swedbankPayCheckoutService)
            : base(confirmationService, addressBookService, customerContextFacade, orderGroupCalculator, marketService)
        {
            _checkoutService = checkoutService;
            _cartService = cartService;
            _orderRepository = orderRepository;
            _recommendationService = recommendationService;
            _swedbankPayCheckoutService = swedbankPayCheckoutService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(OrderConfirmationPage currentPage, string notificationMessage, int? orderNumber, string payeeReference)
        {
            IPurchaseOrder order = null;
            if (PageEditing.PageIsInEditMode)
            {
                order = ConfirmationService.CreateFakePurchaseOrder();
            }
            else if (orderNumber.HasValue)
            {
                order = ConfirmationService.GetOrder(orderNumber.Value);
                if (order != null)
                {
                    await _recommendationService.TrackOrderAsync(HttpContext, order);
                }
            }

            if (order == null && orderNumber.HasValue)
            {
                var cart = _orderRepository.Load<ICart>(orderNumber.Value);
                if (cart != null)
                {
                    var swedbankPayOrderId = cart.Properties[Constants.SwedbankPayOrderIdField];
                    order = _checkoutService.GetOrCreatePurchaseOrder(orderNumber.Value, swedbankPayOrderId.ToString());
                }
                else
                {
                    order = _swedbankPayCheckoutService.GetByPayeeReference(payeeReference);
                }
            }

            if (order != null && order.CustomerId == CustomerContext.CurrentContactId)
            {
                var viewModel = CreateViewModel(currentPage, order);
                viewModel.NotificationMessage = notificationMessage;

                return View(viewModel);
            }


            return Redirect(Url.ContentUrl(ContentReference.StartPage));
        }
    }
}