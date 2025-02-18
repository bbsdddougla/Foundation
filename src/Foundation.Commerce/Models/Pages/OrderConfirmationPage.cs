using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Foundation.Cms.Pages;
using System.ComponentModel.DataAnnotations;

namespace Foundation.Commerce.Models.Pages
{
    [ContentType(DisplayName = "Order Confirmation Page",
        GUID = "04285260-47be-4ecf-9118-558d6c88d3c0",
        Description = "Page to show when succesful checkout",
        GroupName = CommerceGroupNames.Commerce,
        AvailableInEditMode = false)]
    [AvailableContentTypes(Availability = Availability.None)]
    [ImageUrl("~/assets/icons/cms/pages/cms-icon-page-08.png")]
    public class OrderConfirmationPage : FoundationPageData
    {
        [CultureSpecific]
        [Display(
            Name = "Title",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string Title { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Body text",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        public virtual XhtmlString Body { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Registration area",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        public virtual ContentArea RegistrationArea { get; set; }
    }
}