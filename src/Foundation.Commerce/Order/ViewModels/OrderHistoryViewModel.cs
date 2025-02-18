using Foundation.Cms.ViewModels;
using Foundation.Commerce.Customer;
using Foundation.Commerce.Models.Blocks;
using Foundation.Commerce.Models.Pages;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Foundation.Commerce.Order.ViewModels
{
    public class OrderHistoryViewModel : ContentViewModel<OrderHistoryPage>
    {
        public List<OrderViewModel> Orders { get; set; }
        public string OrderDetailsPageUrl { get; set; }
        public FoundationContact CurrentCustomer { get; set; }

        public int CycleMode { get; set; }
        public int CycleLength { get; set; }
        public OrderHistoryBlock CurrentBlock { get; set; }

        public List<SelectListItem> Modes => new List<SelectListItem>
        {
            new SelectListItem { Text = "Every x Days", Value="1"},
            new SelectListItem { Text = "Every x Weeks", Value="2"},
            new SelectListItem { Text = "Every X Months", Value="3"},
            new SelectListItem { Text = "Every X Years", Value="4"}
        };

        public OrderHistoryViewModel() : base() { }
        public OrderHistoryViewModel(OrderHistoryPage currentContent) : base(currentContent) { } // currentContent must be OrderHistoryPage or OrderHistoryBlock
    }
}