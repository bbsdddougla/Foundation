﻿using EPiServer;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.Security;
using EPiServer.Web.Routing;
using Foundation.Cms.Identity;
using Foundation.Commerce.Customer.Services;
using Foundation.Commerce.Customer.ViewModels;
using Foundation.Commerce.Models.Pages;
using Foundation.Commerce.Order.Services;
using Foundation.Commerce.Order.ViewModels;
using Mediachase.Commerce.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Foundation.Features.MyAccount.ProfilePage
{
    [Authorize]
    public class ProfilePageController : IdentityControllerBase<Social.Models.Pages.ProfilePage>
    {
        private readonly IAddressBookService _addressBookService;
        private readonly IOrderRepository _orderRepository;
        private readonly IContentLoader _contentLoader;
        private readonly ICartService _cartService;

        public ProfilePageController(IAddressBookService addressBookService,
            IOrderRepository orderRepository,
            ApplicationSignInManager<SiteUser> signinManager,
            ApplicationUserManager<SiteUser> userManager,
            ICartService cartService,
            IContentLoader contentLoader,
            ICustomerService customerService) : base(signinManager, userManager, customerService)
        {
            _addressBookService = addressBookService;
            _orderRepository = orderRepository;
            _contentLoader = contentLoader;
            _cartService = cartService;
        }

        public ActionResult Index(Social.Models.Pages.ProfilePage currentPage)
        {
            var viewModel = new SocialCommerceProfilePageViewModel(currentPage)
            {
                Orders = GetOrderHistoryViewModels(),
                Addresses = GetAddressViewModels(),
                SiteUser = CustomerService.GetSiteUser(User.Identity.Name),
                CustomerContact = new Commerce.Customer.FoundationContact(CustomerService.GetCurrentContact().Contact)
            };

            viewModel.OrderDetailsPageUrl = UrlResolver.Current.GetUrl(_contentLoader.Get<CommerceHomePage>(ContentReference.StartPage).OrderDetailsPage);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Save(Social.Models.Pages.ProfilePage currentPage, AccountInformationViewModel viewModel)
        {
            var user = CustomerService.GetSiteUser(User.Identity.Name);
            var contact = CustomerService.GetCurrentContact();
            user.FirstName = contact.FirstName = viewModel.FirstName;
            user.LastName = contact.LastName = viewModel.LastName;
            contact.Contact.BirthDate = viewModel.DateOfBirth;
            user.NewsLetter = viewModel.SubscribesToNewsletter;

            UserManager.UpdateAsync(user)
                .GetAwaiter()
                .GetResult();

            contact.SaveChanges();

            return Json(new { FirstName = contact.FirstName, LastName = contact.LastName });
        }

        private IList<AddressModel> GetAddressViewModels() => _addressBookService.List();

        private List<OrderViewModel> GetOrderHistoryViewModels()
        {
            var purchaseOrders = _orderRepository.Load<IPurchaseOrder>(PrincipalInfo.CurrentPrincipal.GetContactId(), _cartService.DefaultCartName)
                .OrderByDescending(x => x.Created).ToList();

            if (purchaseOrders.Count > 3)
            {
                purchaseOrders = purchaseOrders.Take(3).ToList();
            }


            var viewModel = new List<OrderViewModel>();

            foreach (var purchaseOrder in purchaseOrders)
            {
                // Assume there is only one form per purchase.
                var form = purchaseOrder.GetFirstForm();
                var billingAddress = new AddressModel();
                var payment = form.Payments.FirstOrDefault();
                if (payment != null)
                {
                    billingAddress = _addressBookService.ConvertToModel(payment.BillingAddress);
                }
                var orderViewModel = new OrderViewModel
                {
                    PurchaseOrder = purchaseOrder,
                    Items = form.GetAllLineItems().Select(lineItem => new OrderHistoryItemViewModel
                    {
                        LineItem = lineItem,
                    }).GroupBy(x => x.LineItem.Code).Select(group => group.First()),
                    BillingAddress = billingAddress,
                    ShippingAddresses = new List<AddressModel>()
                };

                foreach (var orderAddress in purchaseOrder.Forms.SelectMany(x => x.Shipments).Select(s => s.ShippingAddress))
                {
                    var shippingAddress = _addressBookService.ConvertToModel(orderAddress);
                    orderViewModel.ShippingAddresses.Add(shippingAddress);
                }

                viewModel.Add(orderViewModel);
            }

            return viewModel;
        }
    }
}